using EFLogs.Concrete;
using EFLogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLogs.Abstract
{
    public interface IDBLog
    {
        #region Logs

        IQueryable<Logs> Logs { get; }
        IQueryable<Logs> GetLogs();
        Logs GetLogs(long id);
        long SaveLogs(Logs Logs);
        Logs DeleteLogs(long ID);

        long Write(int? id_services, int? id_eventID, Level level, string log);

        long SaveInformation(string log);
        long SaveInformation(int? id_services, int? id_eventID, string log);

        long SaveWarning(string log);
        long SaveWarning(int? id_services, int? id_eventID, string log);

        long SaveError(string log);
        long SaveError(int? id_services, int? id_eventID, string log);

        long SaveCritical(string log);
        long SaveCritical(int? id_services, int? id_eventID, string log);

        #endregion

        #region LogErrors

        IQueryable<LogErrors> LogErrors { get; }
        IQueryable<LogErrors> GetLogErrors();
        LogErrors GetLogErrors(long id);
        long SaveLogErrors(LogErrors LogErrors);
        LogErrors DeleteLogErrors(long ID);

        long SaveError(Exception e, string user_message, int? id_services, int? id_eventID);
        long SaveError(Exception e, int? id_services, int? id_eventID);
        long SaveError(Exception e, string user_message);
        long SaveError(Exception e);

        #endregion
    }
}
