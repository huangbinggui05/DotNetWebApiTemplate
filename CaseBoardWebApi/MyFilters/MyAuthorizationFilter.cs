using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyHelper;
using CaseBoardWebApi.Models.Authorization;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace CaseBoardWebApi.MyFilters
{
    /// <summary>
    /// 授权筛选器
    /// </summary>
    public class MyAuthorizationFilter : AuthorizationFilterAttribute
    {

        public async override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method == HttpMethod.Post)
            {
                #region 客户端访问凭据验证
                //请求参数
                byte[] byteArray = await actionContext.Request.Content.ReadAsByteArrayAsync();
                string reqContent = System.Text.Encoding.UTF8.GetString(byteArray);
                Credential credential = reqContent.JsonToObj<Credential>();

                if (credential != null)
                {
                    if (credential.calltoken.Equals(ConfigurationManager.AppSettings["calltoken"], StringComparison.CurrentCulture)
                        && credential.password.Equals(ConfigurationManager.AppSettings["password"], StringComparison.CurrentCulture))
                    {
                        //允许客户端访问WebApi
                        return;
                    }
                }
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "客户端访问凭据不正确");
                #endregion
                
                #region 用户权限验证

                #endregion
            }
        }
    }
}
