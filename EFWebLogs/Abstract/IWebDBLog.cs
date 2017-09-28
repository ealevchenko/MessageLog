
using EFWebLogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWebLogs.Abstract
{
    public interface IWebDBLog
    {
        #region LogWebErrors

        IQueryable<LogWebErrors> LogWebErrors { get; }
        IQueryable<LogWebErrors> GetLogWebErrors();
        LogWebErrors GetLogWebErrors(long id);
        long SaveLogWebErrors(LogWebErrors LogWebErrors);
        LogWebErrors DeleteLogWebErrors(long ID);

        //long Write(int? id_services, int? id_eventID, Level level, string log);

        //long SaveInformation(string log);
        //long SaveInformation(int? id_services, int? id_eventID, string log);

        //long SaveWarning(string log);
        //long SaveWarning(int? id_services, int? id_eventID, string log);

        //long SaveError(string log);
        //long SaveError(int? id_services, int? id_eventID, string log);

        //long SaveCritical(string log);
        //long SaveCritical(int? id_services, int? id_eventID, string log);

        #endregion

        #region LogWebVisit

        IQueryable<LogWebVisit> LogWebVisit { get; }
        IQueryable<LogWebVisit> GetLogWebVisit();
        LogWebVisit GetLogWebVisit(long id);
        long SaveLogWebVisit(LogWebVisit LogWebVisit);
        LogWebVisit DeleteLogWebVisit(long ID);

        //long SaveError(Exception e, string user_message, int? id_services, int? id_eventID);
        //long SaveError(Exception e, int? id_services, int? id_eventID);
        //long SaveError(Exception e, string user_message);
        //long SaveError(Exception e);

        #endregion
    }
}
