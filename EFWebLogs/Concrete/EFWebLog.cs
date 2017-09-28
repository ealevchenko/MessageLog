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



        //#region Logs


        //    /// <summary>
        //    /// Записать сообщение в БД
        //    /// </summary>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <param name="level"></param>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long Write(int? id_services, int? id_eventID, Level level, string log)
        //    {
        //        return SaveLogs(new Logs()
        //        {
        //            ID = 0,
        //            DateTime = DateTime.Now,
        //            UserName = System.Environment.UserDomainName + @"\" + System.Environment.UserName,
        //            UserHostName = System.Environment.MachineName,
        //            UserHostAddress = GetIP(),
        //            PhysicalPath = System.Environment.CommandLine,
        //            Service = id_services,
        //            EventID = id_eventID,
        //            Level = (int)level,
        //            Log = log
        //        });
        //    }
        //    #endregion

        //    #region Information
        //    /// <summary>
        //    /// Сохранить лог типа информация в бд
        //    /// </summary>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveInformation(string log)
        //    {
        //        return Write(null, null, Level.Information, log);
        //    }
        //    /// <summary>
        //    /// Сохранить лог типа информация в бд
        //    /// </summary>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveInformation(int? id_services, int? id_eventID, string log)
        //    {
        //        return Write(id_services, id_eventID, Level.Information, log);
        //    }
        //    #endregion

        //    #region Warning
        //    /// <summary>
        //    /// Сохранить лог типа Warning в бд
        //    /// </summary>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveWarning(string log)
        //    {
        //        return Write(null, null, Level.Warning, log);
        //    }
        //    /// <summary>
        //    /// Сохранить лог типа Warning в бд
        //    /// </summary>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveWarning(int? id_services, int? id_eventID, string log)
        //    {
        //        return Write(id_services, id_eventID, Level.Warning, log);
        //    }
        //    #endregion

        //    #region Error
        //    /// <summary>
        //    /// Сохранить лог типа Error в бд
        //    /// </summary>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveError(string log)
        //    {
        //        return Write(null, null, Level.Error, log);
        //    }
        //    /// <summary>
        //    /// Сохранить лог типа Error в бд
        //    /// </summary>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveError(int? id_services, int? id_eventID, string log)
        //    {
        //        return Write(id_services, id_eventID, Level.Error, log);
        //    }
        //    #endregion

        //    #region Critical
        //    /// <summary>
        //    /// Сохранить лог типа Critical в бд
        //    /// </summary>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveCritical(string log)
        //    {
        //        return Write(null, null, Level.Critical, log);
        //    }
        //    /// <summary>
        //    /// Сохранить лог типа Critical в бд
        //    /// </summary>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <param name="log"></param>
        //    /// <returns></returns>
        //    public long SaveCritical(int? id_services, int? id_eventID, string log)
        //    {
        //        return Write(id_services, id_eventID, Level.Critical, log);
        //    }
        //    #endregion

        //#endregion

        //#region LogErrors

        //    #region Общие
        //    /// <summary>
        ///// Прочесть
        ///// </summary>
        //    public IQueryable<LogErrors> LogErrors
        //    {
        //        get { return context.LogErrors; }
        //    }

        //    public IQueryable<LogErrors> GetLogErrors()
        //    {
        //        try
        //        {
        //            return LogErrors;
        //        }
        //        catch (Exception e)
        //        {
        //            e.SaveErrorMethod(String.Format("GetLogErrors()"), blog);
        //            return null;
        //        }
        //    }

        //    public LogErrors GetLogErrors(long id)
        //    {
        //        try
        //        {
        //            return GetLogErrors().Where(l => l.ID == id).FirstOrDefault();
        //        }
        //        catch (Exception e)
        //        {
        //            e.SaveErrorMethod(String.Format("GetLogErrors(id={0})", id), blog);
        //            return null;
        //        }
        //    }
        //    /// <summary>
        //    /// Добавить сохранить
        //    /// </summary>
        //    /// <param name="LogErrors"></param>
        //    /// <returns></returns>
        //    public long SaveLogErrors(LogErrors LogErrors)
        //    {
        //        LogErrors dbEntry;
        //        try
        //        {
        //            if (LogErrors.ID == 0)
        //            {
        //                dbEntry = new LogErrors()
        //                {
        //                    ID = 0,
        //                    DateTime = LogErrors.DateTime,
        //                    UserName = LogErrors.UserName,
        //                    UserHostName = LogErrors.UserHostName,
        //                    UserHostAddress = LogErrors.UserHostAddress,
        //                    PhysicalPath = LogErrors.PhysicalPath,
        //                    UserMessage = LogErrors.UserMessage,
        //                    Service = LogErrors.Service,
        //                    EventID = LogErrors.EventID,
        //                    HResult = LogErrors.HResult,
        //                    InnerException = LogErrors.InnerException,
        //                    Message = LogErrors.Message,
        //                    Source = LogErrors.Source,
        //                    StackTrace = LogErrors.StackTrace
        //                };
        //                context.LogErrors.Add(dbEntry);
        //            }
        //            else
        //            {
        //                dbEntry = context.LogErrors.Find(LogErrors.ID);
        //                if (dbEntry != null)
        //                {
        //                    dbEntry.DateTime = LogErrors.DateTime;
        //                    dbEntry.UserName = LogErrors.UserName;
        //                    dbEntry.UserHostName = LogErrors.UserHostName;
        //                    dbEntry.UserHostAddress = LogErrors.UserHostAddress;
        //                    dbEntry.PhysicalPath = LogErrors.PhysicalPath;
        //                    dbEntry.UserMessage = LogErrors.UserMessage;
        //                    dbEntry.Service = LogErrors.Service;
        //                    dbEntry.EventID = LogErrors.EventID;
        //                    dbEntry.HResult = LogErrors.HResult;
        //                    dbEntry.InnerException = LogErrors.InnerException;
        //                    dbEntry.Message = LogErrors.Message;
        //                    dbEntry.Source = LogErrors.Source;
        //                    dbEntry.StackTrace = LogErrors.StackTrace;
        //                }
        //            }

        //            context.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            e.SaveErrorMethod(String.Format("SaveLogErrors(LogErrors={0})", LogErrors.GetFieldsAndValue()), blog);
        //            return -1;
        //        }
        //        return dbEntry.ID;
        //    }
        //    /// <summary>
        //    /// Удалить
        //    /// </summary>
        //    /// <param name="ID"></param>
        //    /// <returns></returns>
        //    public LogErrors DeleteLogErrors(long ID)
        //    {
        //        LogErrors dbEntry = context.LogErrors.Find(ID);
        //        if (dbEntry != null)
        //        {
        //            try
        //            {
        //                context.LogErrors.Remove(dbEntry);                        
        //                context.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                e.SaveErrorMethod(String.Format("DeleteLogErrors(ID={0})", ID), blog);
        //                return null;
        //            }
        //        }
        //        return dbEntry;
        //    }
        //    #endregion

        //    #region Exception

        //    /// <summary>
        //    /// Сохранить ошибку сервиса
        //    /// </summary>
        //    /// <param name="e"></param>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <returns></returns>
        //    public long SaveError(Exception e, string user_message, int? id_services, int? id_eventID)
        //    {
        //        if (e.InnerException != null)
        //        {
        //            SaveError(e.InnerException, user_message, id_services, id_eventID);
        //        }
        //        return SaveLogErrors(new LogErrors()
        //        {
        //            ID = 0,
        //            DateTime = DateTime.Now,
        //            UserName = System.Environment.UserDomainName + @"\" + System.Environment.UserName,
        //            UserHostName = System.Environment.MachineName,
        //            UserHostAddress = GetIP(),
        //            PhysicalPath = System.Environment.CommandLine,
        //            UserMessage = user_message,
        //            Service = id_services,
        //            EventID = id_eventID,
        //            HResult = e.HResult,
        //            InnerException = e.InnerException != null ? e.InnerException.Message : null,
        //            Message = e.Message,
        //            Source = e.Source,
        //            StackTrace = e.StackTrace
        //        });
        //    }
        //    /// <summary>
        //    /// Сохранить ошибку сервиса
        //    /// </summary>
        //    /// <param name="e"></param>
        //    /// <param name="id_services"></param>
        //    /// <param name="id_eventID"></param>
        //    /// <returns></returns>
        //    public long SaveError(Exception e, int? id_services, int? id_eventID)
        //    {
        //        return SaveError(e, null, id_services, id_eventID);
        //    }
        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    /// <param name="e"></param>
        //    /// <param name="user_message"></param>
        //    /// <returns></returns>
        //    public long SaveError(Exception e, string user_message)
        //    {
        //        return SaveError(e, user_message, null, null);
        //    }
        //    /// <summary>
        //    /// Сохранить ошибку сервиса
        //    /// </summary>
        //    /// <param name="e"></param>
        //    /// <returns></returns>
        //    public long SaveError(Exception e)
        //    {
        //        return SaveError(e, null, null);
        //    }

        //    #endregion

        //#endregion

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
    }
}
