using EFServicesLogs.Abstract;
using EFServicesLogs.Entities;
using MessageLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libClass;

namespace EFServicesLogs.Concrete
{
    public class EFServicesLog : IServicesLogs
    {
        protected EFDbContext context = new EFDbContext();
        private bool blog = false;


        public EFServicesLog()
        {
            FileLogs.InitLogger();
        }

        public EFServicesLog(bool blog)
        {
            FileLogs.InitLogger();
            this.blog = blog;
        }

        #region LogServices
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<LogServices> LogServices
        {
            get { return context.LogServices; }
        }

        public IQueryable<LogServices> GetLogServices()
        {
            try
            {
                return LogServices;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogServices()"), blog);
                return null;
            }
        }
        /// <summary>
        /// Править добавить
        /// </summary>
        /// <param name="LogServices"></param>
        /// <returns></returns>
        public long SaveLogServices(LogServices LogServices)
        {
            LogServices dbEntry;
            try
            {
                if (LogServices.id == 0)
                {
                    dbEntry = new LogServices()
                    {
                        id = LogServices.id,
                        service = LogServices.service,
                        start = LogServices.start,
                        duration = LogServices.duration,
                        code_return = LogServices.code_return
                    };
                    context.LogServices.Add(dbEntry);
                }
                else
                {
                    dbEntry = context.LogServices.Find(LogServices.id);
                    if (dbEntry != null)
                    {
                        dbEntry.service = LogServices.service;
                        dbEntry.start = LogServices.start;
                        dbEntry.duration = LogServices.duration;
                        dbEntry.code_return = LogServices.code_return;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogServices(LogServices={0})", LogServices.GetFieldsAndValue()), blog);
                return -1;
            }
            return dbEntry.id;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDLogService"></param>
        /// <returns></returns>
        public LogServices DeleteLogServices(long id)
        {
            LogServices dbEntry = context.LogServices.Find(id);
            if (dbEntry != null)
            {

                try
                {
                    context.LogServices.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogServices(id={0})", id), blog);
                    return null;
                }
            }
            return dbEntry;
        }
        /// <summary>
        /// Вернуть лог сервисов
        /// </summary>
        /// <param name="id_service"></param>
        /// <returns></returns>
        public IQueryable<LogServices> GetLogServices(int id_service)
        {
            try
            {
                return GetLogServices().Where(l => l.service == id_service).OrderBy(l => l.start);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogServices(id_service={0})", id_service), blog);
                return null;
            }
        }
        /// <summary>
        /// Вернуть лог сервиса за период
        /// </summary>
        /// <param name="id_service"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<LogServices> GetLogServices(int id_service, DateTime start, DateTime stop)
        {
            try
            {
                return GetLogServices(id_service).Where(l => l.start >= start & l.start < stop);
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogServices(id_service={0}, start={1}, stop={2})", id_service,start,stop), blog);
                return null;
            }
        }

        #endregion

        #region StatusService
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<LogStatusServices> LogStatusServices
        {
            get { return context.LogStatusServices; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<LogStatusServices> GetLogStatusServices()
        {
            try
            {
                return LogStatusServices;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetLogStatusServices()"), blog);
                return null;
            }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="LogStatusServices"></param>
        /// <returns></returns>
        public int SaveLogStatusServices(LogStatusServices LogStatusServices)
        {
            LogStatusServices dbEntry;
            try
            {
                LogStatusServices lss = context.LogStatusServices.Where(s => s.service == LogStatusServices.service).FirstOrDefault();
                if (lss == null)
                {
                    dbEntry = new LogStatusServices()
                    {
                        id = LogStatusServices.id,
                        service = LogStatusServices.service,
                        start = LogStatusServices.start,
                        execution = LogStatusServices.execution,
                        current = LogStatusServices.current,
                        max = LogStatusServices.max,
                        min = LogStatusServices.min
                    };
                    context.LogStatusServices.Add(dbEntry);
                }
                else
                {
                    int id = lss.id;
                    dbEntry = context.LogStatusServices.Find(id);
                    if (dbEntry != null)
                    {
                        dbEntry.service = LogStatusServices.service;
                        dbEntry.start = LogStatusServices.start;
                        dbEntry.execution = LogStatusServices.execution;
                        dbEntry.current = LogStatusServices.current;
                        dbEntry.max = LogStatusServices.max;
                        dbEntry.min = LogStatusServices.min;
                    }
                }
                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogStatusServices(LogStatusServices={0})", LogStatusServices.GetFieldsAndValue()), blog);
                return -1;
            }
            return dbEntry.id;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogStatusServices DeleteLogStatusServices(int id)
        {
            LogStatusServices dbEntry = context.LogStatusServices.Find(id);
            if (dbEntry != null)
            {
                try
                {
                    context.LogStatusServices.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.SaveErrorMethod(String.Format("DeleteLogStatusServices(id={0})", id), blog);
                    return null;
                }

            }
            return dbEntry;
        }
        /// <summary>
        /// Получить строку статуса для указанного сервиса
        /// </summary>
        /// <param name="id_service"></param>
        /// <returns></returns>
        public LogStatusServices GetLogStatusServices(int id_service)
        {
            try
            {
                return GetLogStatusServices().Where(s => s.service == id_service).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetStatusService(id_service={0})", id_service), blog);
                return null;
            }
        }
        #endregion

    }
}
