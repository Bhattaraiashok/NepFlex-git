//using System.Collections.Generic;

//namespace NepFlex.Core.Entities.ResourceModels
//{
//    public class ResponseMessages
//    {
//        public string Type { get; set; }
//        public string Code { get; set; }
//        public string Message { get; set; }
//        public bool Returned { get; set; }
//    }

//    public class ErrorPageDetails
//    {
//        public string ErrorTitle { get; set; }
//        public string ErrorDetails { get; set; }
//        public string ErrorCode { get; set; }
//        public bool IsErrorOccured { get; set; }
//    }

//    public class ResponseStatus
//    {
//        public bool IsSuccess { get; set; }
//        public string StatusType { get; set; }
//        public List<string> StrMessage { get; set; }
//    }

//    public class Messages
//    {
//        public List<ResponseMessages> ResponseMessageList
//        {
//            get
//            {
//                return new List<ResponseMessages>()
//                {
//                //general
//                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_SUCCESS, Returned=true, Type="success", Message="Successful."},
//                new ResponseMessages{Code=ConstList.REQ_OBJ_CONST_SUCCESS, Returned=true, Type="success", Message="Successful."},
//                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_FAILURE, Returned=false, Type="error", Message="Exception occured"},
//                //backend
//                new ResponseMessages{Code=ConstList.RES_OBJ_CONST_NULL, Returned=false, Type="info", Message="SVC is returning null."},
//                new ResponseMessages{Code=ConstList.SYS_OBJ_CONST_FAILURE, Returned=false, Type="error", Message="System Error, Please try again."},
//                //Auth
//                //
//                new ResponseMessages{Code=ConstList.USER_VALID_SUCCESS, Returned=true, Type="success", Message="Validation is completed."},
//                new ResponseMessages{Code=ConstList.USER_INVALID_FAILURE, Returned=false, Type="error", Message="Invalid user request."},
//                new ResponseMessages{Code=ConstList.ROLE_USER_RES_CONST_FAILURE, Returned=false, Type="error", Message="User not authorized."},
//                new ResponseMessages{Code=ConstList.RES_USER_EMAIL_EXISTS_CONST_FAILURE, Returned=false, Type="error", Message="Account exist with provided email id."},
//                new ResponseMessages{Code=ConstList.RES_USERNAME_EXISTS_CONST_FAILURE, Returned=false, Type="error", Message="Username already taken. Try something else."},
//                new ResponseMessages{Code=ConstList.USER_INACTIVE_FAILURE, Returned=false, Type="error", Message="User profile doesn't exist or profile is already deleted/disabled"},
//                new ResponseMessages{Code=ConstList.MULTIPLE_PASSWORD_ATTEMPT_FAILURE, Returned=false, Type="error", Message="Exceeded total allowed attempt. Account is currently locked and auto unlocks after 15 min."},
//                //login-register-update
//                new ResponseMessages{Code=ConstList.USER_LOGIN_CONST_SUCCESS, Returned=true, Type="success", Message="Login successful."},
//                new ResponseMessages{Code=ConstList.USER_LOGIN_CONST_FAILURE, Returned=false, Type="error", Message="Login failed."},
//                new ResponseMessages{Code=ConstList.COMPANY_LOGIN_CONST_FAILURE, Returned=false, Type="error", Message="Login attempt: failed."},
//                new ResponseMessages{Code=ConstList.COMPANY_UPDATE_CONST_SUCCESS, Returned=true, Type="success", Message="Successfully, company profile updated."},
//                new ResponseMessages{Code=ConstList.COMPANY_UPDATE_CONST_FAILURE, Returned=false, Type="error", Message="Failed to update company profile."},

//                new ResponseMessages{Code=ConstList.USER_UPDATE_CONST_SUCCESS, Returned=true, Type="success", Message="Successfully, user info updated."},
//                new ResponseMessages{Code=ConstList.USER_UPDATE_CONST_FAILURE, Returned=false, Type="error", Message="Issue occured when updating Your changes."},
//                new ResponseMessages{Code=ConstList.USER_PROFILE_CONST_SUCCESS, Returned=true, Type="success", Message="Successfully, your Profile is updated."},
//                new ResponseMessages{Code=ConstList.USER_PROFILE_CONST_FAILURE, Returned=false, Type="error", Message="User profile not found."},
                
//                //DB CODE
//                new ResponseMessages{Code=ConstList.USER_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your user account."},
//                new ResponseMessages{Code=ConstList.COMPANY_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your company account."},
//                new ResponseMessages{Code=ConstList.USERANDCOMPANY_REGISTER_CONST_FAILURE, Returned=false, Type="error", Message="Occured some problem while registering your account."},

//                new ResponseMessages{Code=ConstList.DB_ACCESS_RESTRICTED_FAILURE, Returned=false, Type="error", Message="Access is restricted."},
//                };
//            }
//        }
//    }
//}
