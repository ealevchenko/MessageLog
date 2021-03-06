﻿using EFWebLogs.Concrete;
using EFWebLogs.Entities;
using Moveax.Mvc.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MessageLog
{
    public static class WebLogs
    {
        private static bool _blog = false;

        static WebLogs()
        {
            FileLogs.InitLogger();
        }

        /// <summary>
        /// Инициализация логирования ошибок
        /// </summary>
        /// <param name="blog"></param>
        public static void InitLog(bool blog)
        {
            _blog = blog;
        }

        #region Visit
        /// <summary>
        /// Сохранить лог визита
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="RolesAccess"></param>
        /// <param name="Access"></param>
        /// <returns></returns>
        public static long WriteVisit(this ActionExecutingContext filterContext, string RolesAccess, bool? Access)
        {
            try
            {
                EFWebLog efwl = new EFWebLog(_blog);
                return efwl.SaveLogWebVisit(new LogWebVisit()
                {
                    ID = 0,
                    DateTime = DateTime.Now,
                    UserName = filterContext.HttpContext.User.Identity.Name,
                    Authentication = filterContext.HttpContext.User.Identity.IsAuthenticated,
                    AuthenticationType = filterContext.HttpContext.User.Identity.AuthenticationType,
                    MachineName = filterContext.HttpContext.Request.UserHostName,
                    MachineIP = filterContext.HttpContext.Request.UserHostAddress,
                    url = filterContext.HttpContext.Request.Url.AbsoluteUri,
                    PhysicalPath = filterContext.HttpContext.Request.PhysicalPath,
                    ActionName = filterContext.ActionDescriptor.ActionName,
                    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    RolesAccess = RolesAccess,
                    Access = Access
                });
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteVisit(ActionExecutingContext={0}, RolesAccess={1}, Access={2})", filterContext, RolesAccess, Access), _blog);
                return -1;
            }
        }
        /// <summary>
        /// Сохранить лог визита
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="RolesAccess"></param>
        /// <param name="Access"></param>
        /// <returns></returns>
        public static long WriteVisit(this ActionExecutedContext filterContext, string RolesAccess, bool? Access)
        {
            try
            {
                EFWebLog efwl = new EFWebLog(_blog);
                return efwl.SaveLogWebVisit(new LogWebVisit()
                {
                    ID = 0,
                    DateTime = DateTime.Now,
                    UserName = filterContext.HttpContext.User.Identity.Name,
                    Authentication = filterContext.HttpContext.User.Identity.IsAuthenticated,
                    AuthenticationType = filterContext.HttpContext.User.Identity.AuthenticationType,
                    MachineName = filterContext.HttpContext.Request.UserHostName,
                    MachineIP = filterContext.HttpContext.Request.UserHostAddress,
                    url = filterContext.HttpContext.Request.Url.AbsoluteUri,
                    PhysicalPath = filterContext.HttpContext.Request.PhysicalPath,
                    ActionName = filterContext.ActionDescriptor.ActionName,
                    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    RolesAccess = RolesAccess,
                    Access = Access
                });
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteVisit(ActionExecutedContext={0}, RolesAccess={1}, Access={2})", filterContext, RolesAccess, Access), _blog);
                return -1;
            }
        }
        #endregion

        #region Error
        /// <summary>
        /// Сохранить ошибку
        /// </summary>
        /// <param name="Exception"></param>
        /// <param name="HttpCode"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static long WriteError(this Exception Exception, int? HttpCode, HttpRequest Request)
        {
            try
            {
                EFWebLog efwl = new EFWebLog(_blog);
                if (Exception.InnerException != null)
                {
                    Exception.InnerException.WriteError(null, Request);
                }
                return efwl.SaveLogWebErrors(new LogWebErrors()
                {
                    ID = 0,
                    DateTime = DateTime.Now,
                    UserName = Request.LogonUserIdentity.Name,
                    Authentication = Request.IsAuthenticated,
                    AuthenticationType = Request.LogonUserIdentity.AuthenticationType,
                    UserHostName = Request.UserHostName,
                    UserHostAddress = Request.UserHostAddress,
                    url = Request.Url.AbsolutePath,
                    PhysicalPath = Request.PhysicalPath,
                    UserAgent = Request.UserAgent,
                    RequestType = Request.RequestType,
                    HttpCode = HttpCode,
                    HResult = Exception.HResult,
                    InnerException = Exception.InnerException != null ? Exception.InnerException.Message : null,
                    Message = Exception.Message,
                    Source = Exception.Source,
                    StackTrace = Exception.StackTrace
                });
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteError(Exception={0}, HttpCode={1}, Request={2})", Exception, HttpCode, Request), _blog);
                return -1;
            }
        }
        ///<summary>
        /// Сохранить цепочку ошибок
        ///</summary>
        ///<param name="errorDescription"></param>
        ///<returns></returns>
        public static long WriteError(this ErrorDescription errorDescription)
        {
            return errorDescription.Exception.WriteError(errorDescription.HttpCode, errorDescription.Request);
        }
        #endregion
    }
}
