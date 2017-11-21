using EFLogs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLog
{
    public enum EventStatus : int
    {
        Error = -1,
        No_Actions = 0,
        Ok = 1,
    }
    
    public static class DBLogs
    {
        private static bool _blog = false;

        static DBLogs() {
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

        #region Logs

        /// <summary>
        /// Сохранить лог Information
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static long SaveInformation(this string log)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveInformation(log);
        }
        /// <summary>
        /// Сохранить лог Information
        /// </summary>
        /// <param name="log"></param>
        /// <param name="services"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static long SaveInformation(this string log, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveInformation(id_services, id_eventID, log);
        }
        /// <summary>
        /// Сохранить лог Warning
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static long SaveWarning(this string log)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveWarning(log);
        }
        /// <summary>
        /// Сохранить лог Warning
        /// </summary>
        /// <param name="log"></param>
        /// <param name="services"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static long SaveWarning(this string log, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveWarning(id_services, id_eventID, log);
        }
        /// <summary>
        /// Сохранить лог Error
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static long SaveError(this string log)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(log);
        }
        /// <summary>
        /// Сохранить лог Error
        /// </summary>
        /// <param name="log"></param>
        /// <param name="services"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static long SaveError(this string log, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(id_services, id_eventID, log);
        }
        /// <summary>
        /// Сохранить лог Critical
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static long SaveCritical(this string log)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveCritical(log);
        }
        /// <summary>
        /// Сохранить лог Critical
        /// </summary>
        /// <param name="log"></param>
        /// <param name="services"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static long SaveCritical(this string log, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveCritical(id_services, id_eventID, log);
        }

        #endregion

        #region LogErrors
        /// <summary>
        /// Сохранить лог Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="user_message"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveError(this Exception ex, string user_message, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(ex, user_message, id_services, id_eventID);
        }
        /// <summary>
        /// Сохранить лог Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveError(this Exception ex, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(ex, id_services, id_eventID);
        }
        /// <summary>
        /// Сохранить лог Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="user_message"></param>
        /// <returns></returns>
        public static long SaveError(this Exception ex, string user_message)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(ex, user_message);
        }
        /// <summary>
        /// Сохранить лог Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static long SaveError(this Exception ex)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveError(ex);
        }

        #endregion

        #region LogEvent
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveEvents(this string events, string status, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(id_services, id_eventID, events, status);
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveEventsOfeventID(this string events, string status, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(null, id_eventID, events, status);
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_services"></param>
        /// <returns></returns>
        public static long SaveEventsOfServices(this string events, string status, int? id_services)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(id_services, null, events, status);
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static long SaveEvents(this string events, string status)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(null, null, events, status);
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_services"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveEvents(this string events, EventStatus status, int? id_services, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(id_services, id_eventID, events, status.ToString());
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_eventID"></param>
        /// <returns></returns>
        public static long SaveEventsOfeventID(this string events, EventStatus status, int? id_eventID)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(null, id_eventID, events, status.ToString());
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <param name="id_services"></param>
        /// <returns></returns>
        public static long SaveEventsOfServices(this string events, EventStatus status, int? id_services)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(id_services, null, events, status.ToString());
        }
        /// <summary>
        /// Сохранить событие 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static long SaveEvents(this string events, EventStatus status)
        {
            EFLog eflog = new EFLog(_blog);
            return eflog.SaveLogEvents(null, null, events, status.ToString());
        }
        #endregion


    }
}
