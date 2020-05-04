using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TourismService.Authentification
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Si l'utilisateur n'utilise pas le https protocole
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                //creer une reponse avec message
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p> Use Https instead http</p>");

                //creer l'URL https avec UriBuilder object
                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
                uriBuilder.Scheme = Uri.UriSchemeHttps;
                uriBuilder.Port = 44348;

                actionContext.Response.Headers.Location = uriBuilder.Uri;

            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}