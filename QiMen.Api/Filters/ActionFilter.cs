using CommonUtil;
using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using QiMen.Api.Attributes;
using QiMen.Common;
using System;
using System.Linq;
using System.Text;

namespace QiMen.Api.Filters
{
    /// <summary>
    /// Action过滤器
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        /// <summary>
        /// 缓存容器
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ActionFilter(IEasyCachingProvider easyCachingProvider)
        {
            this._provider = easyCachingProvider;
        }

        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add(TopConstants.LOG_KEY, DateTime.Now.ToMillisecondString());
            var controllerTypeInfo = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerTypeInfo;

            var actionMetadata = new EndpointMetadataCollection(context.ActionDescriptor.EndpointMetadata);
            var actionAttributes = actionMetadata.GetMetadata<ProducesDefaultResponseTypeAttribute>();

            //未标识返回类型
            if (actionAttributes.IsNull())
            {
                LogUtil.Trace($"------------未标识返回类型----------------");
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.UndefinedResponseType, TopResultEnum.UndefinedResponseType.GetDescription()));
                return;
            }
            LogUtil.Trace($"------------校验标识返回类型：true----------------");

            //未继承基类返回
            if (controllerTypeInfo.BaseType != typeof(Controllers.BaseController))
            {
                LogUtil.Trace($"------------未继承基类返回----------------");
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.ErrorBaseType, TopResultEnum.ErrorBaseType.GetDescription()));
                return;
            }
            LogUtil.Trace($"------------校验继承基类返回：true----------------");

            //未使用统一命名空间
            string nameSpace = controllerTypeInfo.Namespace;
            if (nameSpace != Constants.API_LIBRARY_NAMESPACE)
            {
                LogUtil.Trace($"------------未使用统一命名空间----------------");
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.ErrorNameSpace, TopResultEnum.ErrorNameSpace.GetDescription()));
                return;
            }
            LogUtil.Trace($"------------校验使用统一命名空间：true----------------");

            var req = context.HttpContext.Request;

            string method = req.Method.ToUpper();
            //非法的请求类型
            if (method != TopConstants.HTTP_GET && method != TopConstants.HTTP_POST)
            {
                LogUtil.Trace($"------------非法的请求类型----------------");
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.HttpMehtodError, TopResultEnum.HttpMehtodError.GetDescription()));
                return;
            }
            LogUtil.Trace($"------------校验请求类型：true----------------");

            //参数校验
            if (!context.ModelState.IsValid)
            {
                LogUtil.Trace($"------------参数校验不通过----------------");
                string errorMsg = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.ErrorGeneral, errorMsg));
                return;
            }
            LogUtil.Trace($"------------校验参数：true----------------");
        }


        /// <summary>
        /// Action执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var actionMetadata = new EndpointMetadataCollection(context.ActionDescriptor.EndpointMetadata);
                var flushCacheAttributes = actionMetadata.GetMetadata<FlushCacheAttribute>();
                if (!flushCacheAttributes.IsNull() && flushCacheAttributes.IsFlush)
                {
                    _provider.Flush();
                }
                var request = context.HttpContext.Request;
                string traceId = context.HttpContext.TraceIdentifier;

                //入参
                request.EnableRewind();
                request.Body.Position = 0;
                string requestValues = TopUtil.Stream2String(request.Body);

                //出参
                string responseMsg = string.Empty;
                if (context.Result != null)
                {
                    responseMsg = JsonConvert.SerializeObject(((ObjectResult)context.Result).Value);//context.Result可能为空
                }

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"请求ID：{traceId}");
                sb.AppendLine($"请求URL：{request.Path}");
                sb.AppendLine($"请求是否HTTPS：{request.IsHttps}");
                sb.AppendLine($"请求Header明细：{JsonConvert.SerializeObject(request.Headers)}");

                if (request.Headers.ContainsKey("Content-Type"))
                    sb.AppendLine($"请求报文类型Content-Type：{request.Headers["Content-Type"]}");
                if (request.Headers.ContainsKey("Accept"))
                    sb.AppendLine($"请求接收报文类型Accept：{request.Headers["Accept"]}");
                if (request.Headers.ContainsKey("Cookie"))
                    sb.AppendLine($"请求携带Cookie：{request.Headers["Cookie"]}");
                if (request.Headers.ContainsKey("Host"))
                    sb.AppendLine($"请求Host：{request.Headers["Host"]}");
                if (request.Headers.ContainsKey("Referer"))
                    sb.AppendLine($"请求Host：{request.Headers["Referer"]}");
                if (request.Headers.ContainsKey("User-Agent"))
                    sb.AppendLine($"请求User-Agent：{request.Headers["User-Agent"]}");
                if (request.Headers.ContainsKey("Origin"))
                    sb.AppendLine($"请求Origin：{request.Headers["Origin"]}");
                if (request.Headers.ContainsKey("Content-Length"))
                    sb.AppendLine($"请求入参报文长度Content-Length：{request.Headers["Content-Length"]}");

                sb.AppendLine($"请求入参：{requestValues}");
                sb.AppendLine($"请求入参是否通过验证：{context.ModelState.IsValid}");
                sb.AppendLine($"响应HTTP状态码：{context.HttpContext.Response.StatusCode}");
                sb.AppendLine($"响应出参：{responseMsg}");

                if (context.HttpContext.Items.ContainsKey(TopConstants.LOG_KEY))
                    sb.AppendLine($"请求开始时间：{context.HttpContext.Items[TopConstants.LOG_KEY]}");

                sb.AppendLine($"响应结束时间：{DateTime.Now.ToMillisecondString()}");

                LogUtil.Info(sb.ToString(), "访问日志");
            }
            catch (Exception e)
            {
                LogUtil.Error(e, "访问日志");
            }
        }
    }
}
