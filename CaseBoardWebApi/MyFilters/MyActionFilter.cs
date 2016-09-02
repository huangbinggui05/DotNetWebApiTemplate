using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CaseBoardWebApi.MyFilters
{
    /// <summary>
    /// 操作筛选器
    /// </summary>
    public class MyActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 操作方法调用前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            return;
        }
        /// <summary>
        /// 操作方法调用后
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            return;
        }
    }
}
