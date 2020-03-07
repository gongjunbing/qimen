using CommonUtil;
using Microsoft.AspNetCore.Mvc;

namespace QiMen.Api.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    //[Produces(TopConstants.CONTENT_TYPE_APPLICATION_JSON)]
    public class BaseController : ControllerBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseController()
        {
        }

        #region 统一返回方法，勿删勿改

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="response">返回数据</param>
        /// <returns></returns>
        [NonAction]
        protected IActionResult Success<T>(T response)
        {
            return Ok(TopResult.GetTopResultDTO(response, true, (int)TopResultEnum.OK, TopResultEnum.OK.GetDescription()));
        }

        /// <summary>
        /// 分页成功返回
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="response">返回数据</param>
        /// <param name="currentPage">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        [NonAction]
        protected IActionResult Success<T>(T response, int currentPage, int pageSize, int totalCount)
        {
            return Ok(TopResult.GetTopPageResultDTO(response, true, (int)TopResultEnum.OK, TopResultEnum.OK.ToString(), currentPage, pageSize, totalCount));
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        [NonAction]
        protected IActionResult Error(string errMsg)
        {
            return Ok(TopResult.GetTopResultDTO(errMsg, false, (int)TopResultEnum.ErrorGeneral, errMsg));
        }

        /// <summary>
        /// 没有登录
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected IActionResult UnAuthorized()
        {
            return Ok(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.Unauthorized, TopResultEnum.Unauthorized.GetDescription()));
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected IActionResult Forbidden()
        {
            return Ok(TopResult.GetTopResultDTO<string>(false, (int)TopResultEnum.Forbidden, TopResultEnum.Forbidden.GetDescription()));
        }

        #endregion
    }
}
