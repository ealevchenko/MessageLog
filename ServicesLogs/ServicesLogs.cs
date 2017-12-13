using EFServicesLogs.Concrete;
using EFServicesLogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLog
{
    public static class ServicesLogs
    {
        private static bool _blog = false;

        static ServicesLogs()
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
        /// <summary>
        /// Записать лог сервиса
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static long WriteLogServices(this int id_service, DateTime start, DateTime stop, int code)
        {
            try
            {
                EFServicesLog efsl = new EFServicesLog(_blog);
                TimeSpan ts = stop - start;
                int cur_ms = (int)ts.TotalMilliseconds;
                return efsl.SaveLogServices(new LogServices()
                {
                    id = 0,
                    service = id_service,
                    start = start,
                    duration = cur_ms,
                    code_return = code
                });
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteLogServices(id_service={0}, start={1}, stop={2}, code={3})", id_service, start, stop, code), _blog);
                return -1;
            }
        }
        /// <summary>
        /// Записать статус сервиса после выполнения
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static long WriteLogStatusServices(this int id_service, DateTime start, DateTime stop) {
            try
            {
                TimeSpan ts = stop - start;
                int cur_ms = (int)ts.TotalMilliseconds;
                EFServicesLog efsl = new EFServicesLog(_blog);
                LogStatusServices status = efsl.GetLogStatusServices(id_service);
                if (status == null)
                {
                    status = new LogStatusServices()
                    {
                         id = 0,
                         service = id_service,
                         start = start,
                         execution = start,
                         min = cur_ms,
                        max = cur_ms
                    };
                }
                status.execution = start;
                status.current  = cur_ms;
                status.min = status.min == null || cur_ms < status.min ? cur_ms : status.min;
                status.max = status.max == null || cur_ms > status.max ? cur_ms : status.max;
                return efsl.SaveLogStatusServices(status);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteLogStatusServices(id_service={0}, start={1}, stop={2})", id_service, start, stop), _blog);
                return -1;
            }
        }
        /// <summary>
        /// Записать статус сервиса при запуске
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static long WriteLogStatusServices(this int id_service, DateTime start) {
            try
            {

                EFServicesLog efsl = new EFServicesLog(_blog);
                LogStatusServices status = efsl.GetLogStatusServices(id_service);
                if (status == null)
                {
                    status = new LogStatusServices()
                    {
                        id = 0,
                        service = id_service,
                    };
                }
                status.start = start;
                status.execution = null;
                status.current = null;
                return efsl.SaveLogStatusServices(status);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("WriteLogStatusServices(id_service={0}, start={1})", id_service, start), _blog);
                return -1;
            }
        }
        /// <summary>
        /// Проверить выполнение службы за указанный период
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static bool IsRunServices(this int id_service, int period) {
            try
            {
                EFServicesLog efsl = new EFServicesLog(_blog);
                return efsl.IsRunServices(id_service, period);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("IsRunServices(id_service={0}, period={1})", id_service, period), _blog);
                return false;
            }
        }
        /// <summary>
        /// Вернуть код последнего выполнения службы.
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static int? GetCodeReturnServices(this int id_service, int period)
        {
            try
            {
                EFServicesLog efsl = new EFServicesLog(_blog);
                return efsl.GetCodeReturnServices(id_service, period);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetCodeReturnServices(id_service={0}, period={1})", id_service, period), _blog);
                return null;
            }
        }
    }
}
