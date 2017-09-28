using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLog
{
    public static class EventLogs
    {
        static private EventLog elog;
        static private string _eventSourceName = "SRVLog";
        static private string _logName = "SRVLogFile";
        static private bool initLog = false;
        private static bool _blog = false;

        static EventLogs()
        {
            FileLogs.InitLogger();
        }

        public static void InitLog(bool blog)
        {
            _blog = blog;
        }
        /// <summary>
        /// Инициализировать объект
        /// </summary>
        /// <param name="eventSourceName"></param>
        /// <param name="logName"></param>
        /// <returns></returns>
        static public  bool InitEventLogs(string eventSourceName, string logName) {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
                }
                elog = new System.Diagnostics.EventLog();
                elog.Source = eventSourceName; elog.Log = logName;
                _eventSourceName = eventSourceName;
                _logName = logName;
                initLog = true;
                return true;
            }
            catch (Exception e)
            {
                elog = null;
                e.SaveErrorMethod(String.Format("InitEventLogs(eventSourceName={0}, logName={1})", eventSourceName, logName), _blog);
                initLog = false;
                return false;
            }

        }
        /// <summary>
        /// Инициализировать объект
        /// </summary>
        /// <returns></returns>
        static public bool InitEventLogs() {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(_eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(_eventSourceName, _logName);
                }
                elog = new System.Diagnostics.EventLog();
                elog.Source = _eventSourceName; elog.Log = _logName;
                initLog = true;
                return true;
            }
            catch (Exception e)
            {
                elog = null;
                e.SaveErrorMethod(String.Format("InitEventLogs()"), _blog);
                initLog = false;
                return false;
            }

        }

        #region Logs

            #region Information
            /// <summary>
            /// Сохранить лог Information
            /// </summary>
            /// <param name="log"></param>
            /// <returns></returns>
            static public void SaveInformation(this string log)
            {
                if (initLog) elog.WriteEntry(log, EventLogEntryType.Information);
            }
            /// <summary>
            /// Сохранить лог Information
            /// </summary>
            /// <param name="log"></param>
            /// <param name="services"></param>
            /// <param name="eventID"></param>
            /// <returns></returns>
            static public void SaveInformation(this string log, int? id_services, int? id_eventID)
            {
                if (initLog)
                {
                    if (id_services != null) elog.WriteEntry(log, EventLogEntryType.Information, (id_eventID != null ? (int)id_eventID : 0), (short)id_services);
                    else elog.WriteEntry(log, EventLogEntryType.Information, (id_eventID != null ? (int)id_eventID : 0));
                }
            }
            #endregion

            #region Warning
            /// <summary>
            /// Сохранить лог Warning
            /// </summary>
            /// <param name="log"></param>
            /// <returns></returns>
            static public void SaveWarning(this string log)
            {
                if (initLog) elog.WriteEntry(log, EventLogEntryType.Warning);
            }
            /// <summary>
            /// Сохранить лог Warning
            /// </summary>
            /// <param name="log"></param>
            /// <param name="services"></param>
            /// <param name="eventID"></param>
            /// <returns></returns>
            static public void SaveWarning(this string log, int? id_services, int? id_eventID)
            {
                if (initLog)
                {
                    if (id_services != null) elog.WriteEntry(log, EventLogEntryType.Warning, (id_eventID != null ? (int)id_eventID : 0), (short)id_services);
                    else elog.WriteEntry(log, EventLogEntryType.Warning, (id_eventID != null ? (int)id_eventID : 0));
                }
            }
            #endregion

            #region Error
            /// <summary>
            /// Сохранить лог Error
            /// </summary>
            /// <param name="log"></param>
            /// <returns></returns>
            static public void SaveError(this string log)
            {
                if (initLog) elog.WriteEntry(log, EventLogEntryType.Error);
            }
            /// <summary>
            /// Сохранить лог Error
            /// </summary>
            /// <param name="log"></param>
            /// <param name="services"></param>
            /// <param name="eventID"></param>
            /// <returns></returns>
            static public void SaveError(this string log, int? id_services, int? id_eventID)
            {
                if (initLog)
                {
                    if (id_services != null) elog.WriteEntry(log, EventLogEntryType.Error, (id_eventID != null ? (int)id_eventID : 0), (short)id_services);
                    else elog.WriteEntry(log, EventLogEntryType.Error, (id_eventID != null ? (int)id_eventID : 0));
                }
            }
            #endregion

            //TODO: По необходимости описать логирование событий SuccessAudit = 8, FailureAudit = 16

        #endregion

            #region LogErrors
            /// <summary>
            /// Формирование текста сообщения об ошибке
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="user_message"></param>
            /// <returns></returns>
            public static string GetErrorMessage(Exception ex, string user_message) {
                if (!String.IsNullOrWhiteSpace(user_message))
                {
                    return String.Format("UserMesage: {0}, Source: {1}, HResult {2}, Message:  {3}, InnerException: {4}", user_message, ex.Source, ex.HResult, ex.Message, ex.InnerException != null ? ex.InnerException.Message : null);
                }
                else {
                    return String.Format("Source: {0}, HResult {1}, Message:  {2}, InnerException: {3}", ex.Source, ex.HResult, ex.Message, ex.InnerException != null ? ex.InnerException.Message : null);
                }
            }
            /// <summary>
            /// Формирование текста сообщения об ошибке
            /// </summary>
            /// <param name="ex"></param>
            /// <returns></returns>
            public static string GetErrorMessage(Exception ex)
            {
                return GetErrorMessage(ex, null);
            }
            /// <summary>
            /// Сохранить лог Exception
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="user_message"></param>
            /// <param name="id_services"></param>
            /// <param name="id_eventID"></param>
            /// <returns></returns>
            public static void SaveError(this Exception ex, string user_message, int? id_services, int? id_eventID)
            {
                if (ex.InnerException != null) SaveError(ex.InnerException, user_message, id_services, id_eventID);
                EventLogs.SaveError(GetErrorMessage(ex, user_message), id_services, id_eventID);
            }
            /// <summary>
            /// Сохранить лог Exception
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="id_services"></param>
            /// <param name="id_eventID"></param>
            /// <returns></returns>
            public static void SaveError(this Exception ex, int? id_services, int? id_eventID)
            {
                if (ex.InnerException != null) SaveError(ex.InnerException, id_services, id_eventID);                
                EventLogs.SaveError(GetErrorMessage(ex), id_services, id_eventID);
            }
            /// <summary>
            /// Сохранить лог Exception
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="user_message"></param>
            /// <returns></returns>
            public static void SaveError(this Exception ex, string user_message)
            {
                if (ex.InnerException != null) SaveError(ex.InnerException, user_message);                  
                EventLogs.SaveError(GetErrorMessage(ex, user_message));
            }
            /// <summary>
            /// Сохранить лог Exception
            /// </summary>
            /// <param name="ex"></param>
            /// <returns></returns>
            public static void SaveError(this Exception ex)
            {
                if (ex.InnerException != null) SaveError(ex.InnerException); 
                EventLogs.SaveError(GetErrorMessage(ex));
            }

            #endregion

    }
}
