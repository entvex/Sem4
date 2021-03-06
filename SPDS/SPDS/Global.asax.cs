﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Http;
using MSSQLModel;
using SPDS.Models.DbModels;

namespace SPDS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Extract user role and create corresponding Principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
           

                        //retrieve user with email 'username' and extrack role as string

                        IDalUserManagement dal = new MSSQLModelDAL();
                        List<User> users = dal.GetUsers(new ParametersForUsers()
                        {
                            Email = username
                        });
                        if(users.Any())
                        {
                            roles = users.First().Permission.Description;
                        }

                            //Set principal 

                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                        new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
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
