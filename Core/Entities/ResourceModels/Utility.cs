using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class Utility
    {
        public static RequestStatus RequestStatus(string code)
        {
            RequestStatus _requestStatus = new RequestStatus();

            Messages msg = new Messages();
            var responseMessage = msg.ResponseMessageList.Find(x => x.Code == code);

            _requestStatus.IsSuccess = responseMessage.Returned;
            _requestStatus.StrMesssage = responseMessage.Message;
            return _requestStatus;
        }

    }
}
