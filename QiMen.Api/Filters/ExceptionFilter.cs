using CommonUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QiMen.Api.Filters
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExceptionFilter()
        {
        }

        /// <summary>
        /// 触发异常
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            LogUtil.Error(context.Exception);

            context.ExceptionHandled = true;

            //判断是否为可控异常
            if (context.Exception.GetType() == typeof(TopException))
            {
                int exceptionCode = ((TopException)context.Exception).Code;

                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, exceptionCode, context.Exception.Message));
            }
            else
            {
                context.Result = new OkObjectResult(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.ErrorException, context.Exception.Message));
            }
        }
    }
}
