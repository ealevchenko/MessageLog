using EFServicesLogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFServicesLogs.Abstract
{
    public interface IServicesLogs
    {
        #region LogServices
        IQueryable<LogServices> LogServices {get;}
        IQueryable<LogServices> GetLogServices();
        long SaveLogServices(LogServices LogServices);
        LogServices DeleteLogServices(long id);
        IQueryable<LogServices> GetLogServices(int id_service);
        IQueryable<LogServices> GetLogServices(int id_service, DateTime start, DateTime stop);

        #endregion

        #region StatusService
        IQueryable<LogStatusServices> LogStatusServices {get;}
        IQueryable<LogStatusServices> GetLogStatusServices();
        int SaveLogStatusServices(LogStatusServices LogStatusServices);
        LogStatusServices DeleteLogStatusServices(int id);
        LogStatusServices GetLogStatusServices(int id_service);
        #endregion
    }
}
