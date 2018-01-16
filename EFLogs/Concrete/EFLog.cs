using EFLogs.Abstract;
using EFLogs.Entities;
using libClass;
using MessageLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLogs.Concrete
{
    public enum Level : int
    {
        Information = 0, Warning = 1, Error = 2, Critical = 3
    }

    public class EFLog : IDBLog
    {
        protected EFDbContext context = new EFDbContext();
        private bool blog = false;


        public EFLog()
        {
            FileLogs.InitLogger();
        }

        public EFLog(bool blog)
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

        #region Logs

        #region Общие
        public IQueryable<Logs> Logs
        {
            get { return context.Logs; }
        }

        public IQueryable<Logs> GetLogs()
        {
            try
            {
                return Logs;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogs()"), blog);
                return null;
            }
        }

        public Logs GetLogs(long id)
        {
            try
            {
                return GetLogs().Where(l => l.ID == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogs(id={0})", id), blog);
                return null;
            }
        }

        public long SaveLogs(Logs Logs)
        {
            Logs dbEntry;
            try
            {
                if (Logs.ID == 0)
                {
                    dbEntry = new Logs()
                    {
                        ID = 0,
                        DateTime = Logs.DateTime,
                        UserName = Logs.UserName,
                        UserHostName = Logs.UserHostName,
                        UserHostAddress = Logs.UserHostAddress,
                        PhysicalPath = Logs.PhysicalPath,
                        Service = Logs.Service,
                        EventID = Logs.EventID,
                        Level = Logs.Level,
                        Log = Logs.Log
                    };
                    context.Logs.Add(dbEntry);
                }
                else
                {
                    dbEntry = context.Logs.Find(Logs.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.DateTime = Logs.DateTime;
                        dbEntry.UserName = Logs.UserName;
                        dbEntry.UserHostName = Logs.UserHostName;
                        dbEntry.UserHostAddress = Logs.UserHostAddress;
                        dbEntry.PhysicalPath = Logs.PhysicalPath;
                        dbEntry.Service = Logs.Service;
                        dbEntry.EventID = Logs.EventID;
                        dbEntry.Level = Logs.Level;
                        dbEntry.Log = Logs.Log;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogs(Logs={0})", Logs.GetFieldsAndValue()), blog);

                return -1;
            }
            return dbEntry.ID;
        }

        public Logs DeleteLogs(long ID)
        {
            Logs dbEntry = context.Logs.Find(ID);
            if (dbEntry != null)
            {
                try
                {
                    context.Logs.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogs(ID={0})", ID), blog);
                    return null;
                }
            }
            return dbEntry;
        }
        /// <summary>
        /// Записать сообщение в БД
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="level"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public long Write(int? id_services, int? id_eventID, Level level, string log)
        {
            return SaveLogs(new Logs()
            {
                ID = 0,
                DateTime = DateTime.Now,
                UserName = System.Environment.UserDomainName + @"\" + System.Environment.UserName,
                UserHostName = System.Environment.MachineName,
                UserHostAddress = GetIP(),
                PhysicalPath = System.Environment.CommandLine,
                Service = id_services,
                EventID = id_eventID,
                Level = (int)level,
                Log = log
            });
        }
        #endregion

        #region Information
        /// <summary>
        /// Сохранить лог типа информация в бд
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveInformation(string log)
        {
            return Write(null, null, Level.Information, log);
        }
        /// <summary>
        /// Сохранить лог типа информация в бд
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveInformation(int? id_services, int? id_eventID, string log)
        {
            return Write(id_services, id_eventID, Level.Information, log);
        }
        #endregion

        #region Warning
        /// <summary>
        /// Сохранить лог типа Warning в бд
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveWarning(string log)
        {
            return Write(null, null, Level.Warning, log);
        }
        /// <summary>
        /// Сохранить лог типа Warning в бд
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveWarning(int? id_services, int? id_eventID, string log)
        {
            return Write(id_services, id_eventID, Level.Warning, log);
        }
        #endregion

        #region Error
        /// <summary>
        /// Сохранить лог типа Error в бд
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveError(string log)
        {
            return Write(null, null, Level.Error, log);
        }
        /// <summary>
        /// Сохранить лог типа Error в бд
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveError(int? id_services, int? id_eventID, string log)
        {
            return Write(id_services, id_eventID, Level.Error, log);
        }
        #endregion

        #region Critical
        /// <summary>
        /// Сохранить лог типа Critical в бд
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveCritical(string log)
        {
            return Write(null, null, Level.Critical, log);
        }
        /// <summary>
        /// Сохранить лог типа Critical в бд
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public long SaveCritical(int? id_services, int? id_eventID, string log)
        {
            return Write(id_services, id_eventID, Level.Critical, log);
        }
        #endregion

        #endregion

        #region LogErrors

        #region Общие
        /// <summary>
        /// Прочесть
        /// </summary>
        public IQueryable<LogErrors> LogErrors
        {
            get { return context.LogErrors; }
        }

        public IQueryable<LogErrors> GetLogErrors()
        {
            try
            {
                return LogErrors;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogErrors()"), blog);
                return null;
            }
        }

        public LogErrors GetLogErrors(long id)
        {
            try
            {
                return GetLogErrors().Where(l => l.ID == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogErrors(id={0})", id), blog);
                return null;
            }
        }
        /// <summary>
        /// Добавить сохранить
        /// </summary>
        /// <param name="LogErrors"></param>
        /// <returns></returns>
        public long SaveLogErrors(LogErrors LogErrors)
        {
            LogErrors dbEntry;
            try
            {
                if (LogErrors.ID == 0)
                {
                    dbEntry = new LogErrors()
                    {
                        ID = 0,
                        DateTime = LogErrors.DateTime,
                        UserName = LogErrors.UserName,
                        UserHostName = LogErrors.UserHostName,
                        UserHostAddress = LogErrors.UserHostAddress,
                        PhysicalPath = LogErrors.PhysicalPath,
                        UserMessage = LogErrors.UserMessage,
                        Service = LogErrors.Service,
                        EventID = LogErrors.EventID,
                        HResult = LogErrors.HResult,
                        InnerException = LogErrors.InnerException,
                        Message = LogErrors.Message,
                        Source = LogErrors.Source,
                        StackTrace = LogErrors.StackTrace
                    };
                    context.LogErrors.Add(dbEntry);
                }
                else
                {
                    dbEntry = context.LogErrors.Find(LogErrors.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.DateTime = LogErrors.DateTime;
                        dbEntry.UserName = LogErrors.UserName;
                        dbEntry.UserHostName = LogErrors.UserHostName;
                        dbEntry.UserHostAddress = LogErrors.UserHostAddress;
                        dbEntry.PhysicalPath = LogErrors.PhysicalPath;
                        dbEntry.UserMessage = LogErrors.UserMessage;
                        dbEntry.Service = LogErrors.Service;
                        dbEntry.EventID = LogErrors.EventID;
                        dbEntry.HResult = LogErrors.HResult;
                        dbEntry.InnerException = LogErrors.InnerException;
                        dbEntry.Message = LogErrors.Message;
                        dbEntry.Source = LogErrors.Source;
                        dbEntry.StackTrace = LogErrors.StackTrace;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogErrors(LogErrors={0})", LogErrors.GetFieldsAndValue()), blog);
                return -1;
            }
            return dbEntry.ID;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LogErrors DeleteLogErrors(long ID)
        {
            LogErrors dbEntry = context.LogErrors.Find(ID);
            if (dbEntry != null)
            {
                try
                {
                    context.LogErrors.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogErrors(ID={0})", ID), blog);
                    return null;
                }
            }
            return dbEntry;
        }

        public IQueryable<LogErrors> GetLogErrorsOfService(int service) { 
            try
            {
                return GetLogErrors().Where(l => l.Service == service);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogErrorsOfService(service={0})", service), blog);
                return null;
            }        
        }

        public IQueryable<LogErrors> GetLogErrors(int service, DateTime start, DateTime stop)
        {
            try
            {
                return GetLogErrorsOfService(service).Where(l => l.DateTime >= start & l.DateTime < stop);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogErrors(id_service={0}, start={1}, stop={2})", service, start, stop), blog);
                return null;
            }
        }

        public IQueryable<LogErrors> GetLogErrors(DateTime start, DateTime stop)
        {
            try
            {
                return GetLogErrors().Where(l => l.DateTime >= start & l.DateTime < stop);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogErrors(start={0}, stop={1})", start, stop), blog);
                return null;
            }
        }


        #endregion

        #region Exception

        /// <summary>
        /// Сохранить ошибку сервиса
        /// </summary>
        /// <param name="e"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public long SaveError(Exception e, string user_message, int? id_services, int? id_eventID)
        {
            if (e.InnerException != null)
            {
                SaveError(e.InnerException, user_message, id_services, id_eventID);
            }
            return SaveLogErrors(new LogErrors()
            {
                ID = 0,
                DateTime = DateTime.Now,
                UserName = System.Environment.UserDomainName + @"\" + System.Environment.UserName,
                UserHostName = System.Environment.MachineName,
                UserHostAddress = GetIP(),
                PhysicalPath = System.Environment.CommandLine,
                UserMessage = user_message,
                Service = id_services,
                EventID = id_eventID,
                HResult = e.HResult,
                InnerException = e.InnerException != null ? e.InnerException.Message : null,
                Message = e.Message,
                Source = e.Source,
                StackTrace = e.StackTrace
            });
        }
        /// <summary>
        /// Сохранить ошибку сервиса
        /// </summary>
        /// <param name="e"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public long SaveError(Exception e, int? id_services, int? id_eventID)
        {
            return SaveError(e, null, id_services, id_eventID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="user_message"></param>
        /// <returns></returns>
        public long SaveError(Exception e, string user_message)
        {
            return SaveError(e, user_message, null, null);
        }
        /// <summary>
        /// Сохранить ошибку сервиса
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public long SaveError(Exception e)
        {
            return SaveError(e, null, null);
        }

        #endregion

        #endregion

        #region LogEvents

        #region Общие
        public IQueryable<LogEvents> LogEvents
        {
            get { return context.LogEvents; }
        }

        public IQueryable<LogEvents> GetLogEvents()
        {
            try
            {
                return LogEvents;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEvents()"), blog);
                return null;
            }
        }

        public LogEvents GetLogEvent(int id)
        {
            try
            {
                return GetLogEvents().Where(l => l.ID == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEvent(id={0})", id), blog);
                return null;
            }
        }

        public IQueryable<LogEvents> GetLogEvents(DateTime start, DateTime stop)
        {
            try
            {
                return LogEvents.Where(l => l.DateTime >= start & l.DateTime <= stop);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEvents(start={0}, stop={1})", start, stop), blog);
                return null;
            }
        }

        public IQueryable<LogEvents> GetLogEventsOfServices(DateTime start, DateTime stop, int? id_service)
        {
            try
            {
                return GetLogEvents(start, stop).Where(l => l.Service == id_service | l.Service == (id_service == -1 ? null : id_service));
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEventsOfServices(start={0}, stop={1}, id_service={2})", start, stop, id_service), blog);
                return null;
            }
        }

        public IQueryable<LogEvents> GetLogEventsOfEventID(DateTime start, DateTime stop, int? event_id)
        {
            try
            {
                return GetLogEvents(start, stop).Where(l => l.EventID == event_id | l.EventID == (event_id == -1 ? null : event_id));
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEventsOfEventID(start={0}, stop={1}, event_id={2})", start, stop, event_id), blog);
                return null;
            }
        }

        public IQueryable<LogEvents> GetLogEvents(DateTime start, DateTime stop, int? id_service, int? event_id)
        {
            try
            {
                return GetLogEvents(start, stop).Where(l => (l.Service == id_service | l.Service == (id_service == -1 ? null : id_service)) & (l.EventID == event_id | l.EventID == (event_id == -1 ? null : event_id)));
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogEvents(start={0}, stop={1}, id_service={2}, event_id={3})", start, stop, id_service, event_id), blog);
                return null;
            }
        }

        /// <summary>
        /// Сохранить, добавить
        /// </summary>
        /// <param name="LogEvents"></param>
        /// <returns></returns>
        public long SaveLogEvents(LogEvents LogEvents)
        {
            LogEvents dbEntry;
            try
            {
                if (LogEvents.ID == 0)
                {
                    dbEntry = new LogEvents()
                    {
                        ID = 0,
                        DateTime = LogEvents.DateTime,
                        UserName = LogEvents.UserName,
                        UserHostName = LogEvents.UserHostName,
                        UserHostAddress = LogEvents.UserHostAddress,
                        PhysicalPath = LogEvents.PhysicalPath,
                        Service = LogEvents.Service,
                        EventID = LogEvents.EventID,
                        Event = LogEvents.Event,
                        Status = LogEvents.Status
                    };
                    context.LogEvents.Add(dbEntry);
                }
                else
                {
                    dbEntry = context.LogEvents.Find(LogEvents.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.DateTime = LogEvents.DateTime;
                        dbEntry.UserName = LogEvents.UserName;
                        dbEntry.UserHostName = LogEvents.UserHostName;
                        dbEntry.UserHostAddress = LogEvents.UserHostAddress;
                        dbEntry.PhysicalPath = LogEvents.PhysicalPath;
                        dbEntry.Service = LogEvents.Service;
                        dbEntry.EventID = LogEvents.EventID;
                        dbEntry.Event = LogEvents.Event;
                        dbEntry.Status = LogEvents.Status;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogEvents(LogEvents={0})", LogEvents.GetFieldsAndValue()), blog);
                return -1;
            }
            return dbEntry.ID;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LogEvents DeleteLogEvents(long ID)
        {
            LogEvents dbEntry = context.LogEvents.Find(ID);
            if (dbEntry != null)
            {
                try
                {
                    context.LogEvents.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogEvents(ID={0})", ID), blog);
                    return null;
                }
            }
            return dbEntry;
        }
        #endregion

        #region Events

        /// <summary>
        /// Добавить событие
        /// </summary>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public long SaveLogEvents(int? id_services, int? id_eventID, string events, string status)
        {
            return SaveLogEvents(new LogEvents()
            {
                ID = 0,
                DateTime = DateTime.Now,
                UserName = System.Environment.UserDomainName + @"\" + System.Environment.UserName,
                UserHostName = System.Environment.MachineName,
                UserHostAddress = GetIP(),
                PhysicalPath = System.Environment.CommandLine,
                Service = id_services,
                EventID = id_eventID,
                Event = events,
                Status = status,
            });
        }
        #endregion

        #endregion
    }
}
