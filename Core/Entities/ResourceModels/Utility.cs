﻿using System.Collections.Generic;

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
                _requestStatus.StatusType = responseMessage.Type;
                _requestStatus.StrMessage.Add(responseMessage.Message);
            }

            return _requestStatus;
        }

        //public static T AppendStatus<T>(string code) where T : new()
        //{
        //    T _parent = new T();
        //    T obj = new T();

        //    if (code != null)
        //    {
        //        Messages msg = new Messages();
        //        var responseMessage = msg.ResponseMessageList.Find(x => x.Code == code);
        //        List<string> responseMsgs = new List<string>
        //                                        {
        //                                            responseMessage.Message
        //                                        };

        //        if (responseMessage != null)
        //        {
        //            // is success
        //            var isSuccessAppend = _parent.GetType().GetProperty("IsSuccess").GetValue(_parent);
        //            _parent.GetType().GetProperty("IsSuccess").SetValue(_parent, responseMessage.Returned);

        //            // status type
        //            var statusTypeAppend = _parent.GetType().GetProperty("StatusType").GetValue(_parent);
        //            _parent.GetType().GetProperty("StatusType").SetValue(_parent, responseMessage.Type);

        //            // message
        //            var strMessageAppend = _parent.GetType().GetProperty("StrMessage").GetValue(_parent);
        //            _parent.GetType().GetProperty("StrMessage").SetValue(_parent, responseMsgs);
        //        }

        //        obj = (T)_parent;
        //    }
        //    return obj;
        //}

        public static T AppendStatus<T>(string code, T objectWithData)
        {
            if (code != null)
            {
                Messages msg = new Messages();
                var responseMessage = msg.ResponseMessageList.Find(x => x.Code == code);
                List<string> responseMsgs = new List<string>();

                responseMsgs.Add(responseMessage.Message);

                if (responseMessage != null)
                {
                    // is success
                    var isSuccessAppend = objectWithData.GetType().GetProperty("IsSuccess").GetValue(objectWithData);
                    objectWithData.GetType().GetProperty("IsSuccess").SetValue(objectWithData, responseMessage.Returned);

                    // status type
                    var statusTypeAppend = objectWithData.GetType().GetProperty("StatusType").GetValue(objectWithData);
                    objectWithData.GetType().GetProperty("StatusType").SetValue(objectWithData, responseMessage.Type);

                    // message
                    var strMessageAppend = objectWithData.GetType().GetProperty("StrMessage").GetValue(objectWithData);
                    objectWithData.GetType().GetProperty("StrMessage").SetValue(objectWithData, responseMsgs);
                }
            }
            return objectWithData;
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

        public readonly static int INT_CONST_SUCCESS = 1;
        public readonly static int INT_CONST_FAILURE = 0;
    }

    public class CONSTUINAME
    {
        public readonly static string UI_NAME = "Craig";
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

        public readonly static string USER_UPDATE_CONST_SUCCESS = "USER_UPDATE_CONST_SUCCESS";
        public readonly static string USER_UPDATE_CONST_FAILURE = "USER_UPDATE_CONST_FAILURE";

        public readonly static string USER_PROFILE_CONST_SUCCESS = "USER_PROFILE_CONST_SUCCESS";
        public readonly static string USER_PROFILE_CONST_FAILURE = "USER_PROFILE_CONST_FAILURE";

        public readonly static string COMPANY_UPDATE_CONST_SUCCESS = "COMPANY_UPDATE_CONST_SUCCESS";
        public readonly static string COMPANY_UPDATE_CONST_FAILURE = "COMPANY_UPDATE_CONST_FAILURE";

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
