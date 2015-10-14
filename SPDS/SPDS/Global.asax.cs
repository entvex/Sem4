using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SPDS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if(Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //retrieve username

                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        //extract role from DBcontext
                        //TO be implemented
                        //remove hardcoded value before production

                        roles = "admin";

                        //Set principal 

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                            new System.Security.Principal.GenericIdentity(username,"Forms"),roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        {
                        }
                        throw;
                    }
                }
            }
        }
    }
}
