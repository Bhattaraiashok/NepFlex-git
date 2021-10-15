using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class ResponseMessages
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool Returned { get; set; }
    }

    public class ErrorPageDetails
    {
        public string ErrorTitle { get; set; }
        public string ErrorDetails { get; set; }
        public string ErrorCode { get; set; }
        public bool IsErrorOccured { get; set; }
    }

    public class ResponseStatus
    {
        public bool IsSuccess { get; set; }
        public List<string> StrMessage { get; set; }
    }

    public class Messages
    {
        public List<ResponseMessages> ResponseMessageList
        {
            get
            {
                return new List<ResponseMessages>() {
                //general
                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_SUCCESS, Returned=false, Type="success", Message="successful"},
                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_FAILURE, Returned=false, Type="error", Message="Exception occured"},
                //backend
                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_NULL, Returned=false, Type="error", Message="SVC is returning null."},
                new ResponseMessages{Code=ConstList.SYS_OBJ_CONST_FAILURE, Returned=false, Type="error", Message="System Error, Please try again."},
                //Auth
                new ResponseMessages{Code=ConstList.ROLE_USER_RES_CONST_FAILURE, Returned=false, Type="error", Message="User is not able to authorize."},

                //DB CODE
                 new ResponseMessages{Code=ConstList.USER_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your user account."},
                 new ResponseMessages{Code=ConstList.COMPANY_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your company account."},
                 new ResponseMessages{Code=ConstList.USERANDCOMPANY_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your account."},
            };
            }
        }
    }
}
