using EFLogs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLog
{
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
    }
}
