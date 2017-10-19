using EFWebLogs.Abstract;
using EFWebLogs.Entities;
using libClass;
using MessageLog;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWebLogs.Concrete
{
    public enum Level : int
    {
        Information = 0, Warning = 1, Error = 2, Critical = 3
    }
    
    public class EFWebLog : IWebDBLog
    {
        protected EFDbContext context = new EFDbContext();
        private bool blog = false;


        public EFWebLog() {
            FileLogs.InitLogger();
        }

        public EFWebLog(bool blog)
        {
            FileLogs.InitLogger();
            this.blog = blog;
        }

        #region Вспомогательные
        /// <summary>
        /// Получить ip-адрес
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            string ips = "";
            foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList)
            {
                ips += ip.ToString() + "; ";
            }
            return ips.Trim();
        }
        #endregion

        #region LogWebErrors

        public IQueryable<LogWebErrors> LogWebErrors
        {
            get { return context.LogWebErrors; }
        }

        public IQueryable<LogWebErrors> GetLogWebErrors()
        {
            try
            {
                return LogWebErrors;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogWebErrors()"), blog);
                return null;
            }
        }

        public LogWebErrors GetLogWebErrors(long id)
        {
            try
            {
                return GetLogWebErrors().Where(l => l.ID == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogWebErrors(id={0})", id), blog);
                return null;
            }
        }

        public long SaveLogWebErrors(LogWebErrors LogWebErrors)
        {
            LogWebErrors dbEntry;
            try
            {
                if (LogWebErrors.ID == 0)
                {
                    dbEntry = new LogWebErrors()
                    {
                        ID = 0,
                        DateTime = LogWebErrors.DateTime,
                        UserName = LogWebErrors.UserName,
                        Authentication = LogWebErrors.Authentication,
                        AuthenticationType = LogWebErrors.AuthenticationType,
                        UserHostName = LogWebErrors.UserHostName,
                        UserHostAddress = LogWebErrors.UserHostAddress,
                        url = LogWebErrors.url,
                        PhysicalPath = LogWebErrors.PhysicalPath,
                        UserAgent = LogWebErrors.UserAgent,
                        RequestType = LogWebErrors.RequestType,
                        HttpCode = LogWebErrors.HttpCode,
                        HResult = LogWebErrors.HResult,
                        InnerException = LogWebErrors.InnerException,
                        Message = LogWebErrors.Message,
                        Source = LogWebErrors.Source,
                        StackTrace = LogWebErrors.StackTrace
                    };
                    context.LogWebErrors.Add(dbEntry);
                }
                else
                {
                    dbEntry = context.LogWebErrors.Find(LogWebErrors.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.DateTime = LogWebErrors.DateTime;
                        dbEntry.UserName = LogWebErrors.UserName;
                        dbEntry.Authentication = LogWebErrors.Authentication;
                        dbEntry.AuthenticationType = LogWebErrors.AuthenticationType;
                        dbEntry.UserHostName = LogWebErrors.UserHostName;
                        dbEntry.UserHostAddress = LogWebErrors.UserHostAddress;
                        dbEntry.url = LogWebErrors.url;
                        dbEntry.PhysicalPath = LogWebErrors.PhysicalPath;
                        dbEntry.UserAgent = LogWebErrors.UserAgent;
                        dbEntry.RequestType = LogWebErrors.RequestType;
                        dbEntry.HttpCode = LogWebErrors.HttpCode;
                        dbEntry.HResult = LogWebErrors.HResult;
                        dbEntry.InnerException = LogWebErrors.InnerException;
                        dbEntry.Message = LogWebErrors.Message;
                        dbEntry.Source = LogWebErrors.Source;
                        dbEntry.StackTrace = LogWebErrors.StackTrace;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogWebErrors(LogWebErrors={0})", LogWebErrors.GetFieldsAndValue()), blog);

                return -1;
            }
            return dbEntry.ID;
        }

        public LogWebErrors DeleteLogWebErrors(long ID)
        {
            LogWebErrors dbEntry = context.LogWebErrors.Find(ID);
            if (dbEntry != null)
            {
                try
                {
                    context.LogWebErrors.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogWebErrors(ID={0})", ID), blog);
                    return null;
                }
            }
            return dbEntry;
        }
        #endregion

        #region LogWebVisit
            #region Общие

            public IQueryable<LogWebVisit> LogWebVisit
            {
                get { return context.LogWebVisit; }
            }

            public IQueryable<LogWebVisit> GetLogWebVisit()
            {
                try
                {
                    return LogWebVisit;
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("GetLogWebVisit()"), blog);
                    return null;
                }
            }

            public LogWebVisit GetLogWebVisit(long id)
            {
                try
                {
                    return GetLogWebVisit().Where(l => l.ID == id).FirstOrDefault();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("GetLogWebVisit(id={0})", id), blog);
                    return null;
                }
            }

            public long SaveLogWebVisit(LogWebVisit LogWebVisit)
            {
                LogWebVisit dbEntry;
                try
                {
                    if (LogWebVisit.ID == 0)
                    {
                        dbEntry = new LogWebVisit()
                        {
                            ID = 0,
                            DateTime = LogWebVisit.DateTime,
                            UserName = LogWebVisit.UserName,
                            Authentication = LogWebVisit.Authentication,
                            AuthenticationType = LogWebVisit.AuthenticationType,
                            MachineName = LogWebVisit.MachineName,
                            MachineIP = LogWebVisit.MachineIP,
                            url = LogWebVisit.url,
                            PhysicalPath = LogWebVisit.PhysicalPath,
                            ActionName = LogWebVisit.ActionName,
                            ControllerName = LogWebVisit.ControllerName,
                            RolesAccess = LogWebVisit.RolesAccess,
                            Access = LogWebVisit.Access
                        };
                        context.LogWebVisit.Add(dbEntry);
                    }
                    else
                    {
                        dbEntry = context.LogWebVisit.Find(LogWebVisit.ID);
                        if (dbEntry != null)
                        {
                            dbEntry.DateTime = LogWebVisit.DateTime;
                            dbEntry.UserName = LogWebVisit.UserName;
                            dbEntry.Authentication = LogWebVisit.Authentication;
                            dbEntry.AuthenticationType = LogWebVisit.AuthenticationType;
                            dbEntry.MachineName = LogWebVisit.MachineName;
                            dbEntry.MachineName = LogWebVisit.MachineName;
                            dbEntry.MachineIP = LogWebVisit.MachineIP;
                            dbEntry.PhysicalPath = LogWebVisit.PhysicalPath;
                            dbEntry.ActionName = LogWebVisit.ActionName;
                            dbEntry.ControllerName = LogWebVisit.ControllerName;
                            dbEntry.RolesAccess = LogWebVisit.RolesAccess;
                            dbEntry.Access = LogWebVisit.Access;
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("SaveLogWebVisit(LogWebVisit={0})", LogWebVisit.GetFieldsAndValue()), blog);

                    return -1;
                }
                return dbEntry.ID;
            }

            public LogWebVisit DeleteLogWebVisit(long ID)
            {
                LogWebVisit dbEntry = context.LogWebVisit.Find(ID);
                if (dbEntry != null)
                {
                    try
                    {
                        context.LogWebVisit.Remove(dbEntry);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        e.SaveErrorMethod(String.Format("DeleteLogWebVisit(ID={0})", ID), blog);
                        return null;
                    }
                }
                return dbEntry;
            }
            #endregion
        #endregion

    }
}
