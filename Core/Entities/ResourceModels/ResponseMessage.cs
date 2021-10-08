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

    public class RequestStatus
    {
        public bool IsSuccess { get; set; }
        public string StrMesssage { get; set; }
    }

    public class Messages
    {
        public List<ResponseMessages> ResponseMessageList
        {
            get
            {
                return new List<ResponseMessages>() {
                //general
                new ResponseMessages{Code="0", Returned=false, Type="success", Message="successful"},
                new ResponseMessages{Code="-1", Returned=false, Type="error", Message="Exception occured"},
                //backend
                new ResponseMessages{Code="-2", Returned=false, Type="error", Message="Backend returned error, please try again."},
                new ResponseMessages{Code="-3", Returned=false, Type="error", Message="SVC is returning null."},
                new ResponseMessages{Code="-4", Returned=false, Type="error", Message="System Error, Please try again."},
                //Auth
                new ResponseMessages{Code="-5", Returned=false, Type="error", Message="UserRole is null"},
                new ResponseMessages{Code="-5", Returned=false, Type="error", Message="User is not able to authorize."}
            };
            }
        }
    }
}
