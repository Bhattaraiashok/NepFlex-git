using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public static class Utility
    {
        public static ResponseStatus CheckAndAssignStatus(string code)
        {
            ResponseStatus _requestStatus = new ResponseStatus
            {
                StrMessage = new List<string>()
            };

            Messages msg = new Messages();
            //FILTERS FROM ResponseMessageList
            var responseMessage = msg.ResponseMessageList.Find(x => x.Code == code);

            if (responseMessage != null)
            {
                _requestStatus.IsSuccess = responseMessage.Returned;
                _requestStatus.StrMessage.Add(responseMessage.Message);
            }

            return _requestStatus;
        }

        public static int LogTransaction(LogTransactionDetail _logDetail, out string _transId, out List<string> diagnostics)
        {
            // DB call to log Transaction
            string transId = "0";
            List<string> _diagnostics = new List<string>();

            _transId = transId;
            diagnostics = _diagnostics;

            return 0;
        }

        public enum TransactionStatus
        {
            COMMITTED = 0,
            SUCCEEDED = 2,
            IN_PROGRESS = 3,
            FAILED = 4

        }

        //TODOs: 
        // complete DB LogTransaction
        // determine Logged in User Type like -internal or extrenal

    }

    public class LogTransactionDetail
    {
        public string UI;
        public string TranArg;
        public Utility.TransactionStatus TranStatus;
        public string UserLocation;
        public string TranTitle;
        public string TranDetail;
        
        public string TranId { get; set; }
    }
    public class CONSTResponse
    {
        public readonly static string CONST_SUCCESS = "SUCCESS";
        public readonly static string CONST_FAILURE = "FAILURE";
    }

    public class ConstList
    {
        //general
        public readonly static string REQ_OBJ_CONST_SUCCESS = "REQ_OBJ_CONST_SUCCESS";
        public readonly static string REQ_OBJ_CONST_FAILURE = "REQ_OBJ_CONST_FAILURE";

        public readonly static string RES_OBJ_CONST_SUCCESS = "RES_OBJ_CONST_SUCCESS";
        public readonly static string RES_OBJ_CONST_FAILURE = "RES_OBJ_CONST_FAILURE";
        public readonly static string RES_OBJ_CONST_NULL = "RES_OBJ_CONST_NULL";

        public readonly static string SYS_OBJ_CONST_FAILURE = "RES_OBJ_CONST_FAILURE";

        //SPECIFIC-REQ && RES
        public readonly static string USER_LOGIN_CONST_SUCCESS = "USER_LOGIN_CONST_SUCCESS";
        public readonly static string USER_LOGIN_CONST_FAILURE = "USER_LOGIN_CONST_FAILURE";

        public readonly static string COMPANY_LOGIN_CONST_SUCCESS = "COMPANY_LOGIN_CONST_SUCCESS";
        public readonly static string COMPANY_LOGIN_CONST_FAILURE = "COMPANY_LOGIN_CONST_FAILURE";

        public readonly static string USER_REGISTER_CONST_SUCCESS = "USER_REGISTER_CONST_SUCCESS";
        public readonly static string USER_REGISTER_CONST_FAILURE = "USER_REGISTER_CONST_FAILURE";

        public readonly static string COMPANY_REGISTER_CONST_SUCCESS = "COMPANY_REGISTER_CONST_SUCCESS";
        public readonly static string COMPANY_REGISTER_CONST_FAILURE = "COMPANY_REGISTER_CONST_FAILURE";

        public readonly static string USERANDCOMPANY_REGISTER_CONST_SUCCESS = "USERANDCOMPANY_REGISTER_CONST_SUCCESS";
        public readonly static string USERANDCOMPANY_REGISTER_CONST_FAILURE = "USERANDCOMPANY_REGISTER_CONST_FAILURE";

        //PAGESstatic
        public readonly static string PAGE_SEARCH_CONST = "PAGE_SEARCH_CONST";
        public readonly static string PAGE_DETAIL_CONST = "PAGE_DETAIL_CONST";
        public readonly static string PAGE_LIST_CONST = "PAGE_LIST_CONST";
        public readonly static string PAGE_REPORT_CONST = "PAGE_REPORT_CONST";
        public readonly static string PAGE_FAQ_CONST = "PAGE_FAQ_CONST";
        public readonly static string PAGE_ABOUTUS_CONST = "PAGE_ABOUTUS_CONST";

        //USER-ROLE-AUTH
        public readonly static string ROLE_USER_REQ_CONST = "ROLE_USER_REQ_CONST";
        public readonly static string ROLE_USER_RES_CONST_SUCCESS = "ROLE_USER_RES_CONST_SUCCESS";
        public readonly static string ROLE_USER_RES_CONST_FAILURE = "ROLE_USER_RES_CONST_FAILURE";

        //DB   
        public readonly static string DB_CONNECT_CONST_SUCCESS = "DB_CONNECT_CONST_SUCCESS";
        public readonly static string DB_CONNECT_CONST_FAILURE = "DB_CONNECT_CONST_FAILURE";
        public readonly static string DB_EXECUTE_CONST_SUCCESS = "DB_EXECUTE_CONST_SUCCESS";
        public readonly static string DB_EXECUTE_CONST_FAILURE = "DB_EXECUTE_CONST_FAILURE";




    }
}
