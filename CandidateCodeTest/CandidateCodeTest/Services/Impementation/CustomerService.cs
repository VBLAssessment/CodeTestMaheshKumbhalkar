using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using System;

namespace CandidateCodeTest
{
    public class CustomerService : ICustomerService
    {
        public ILogEntry _AddLogsr;
        public IMessageService _messageService;
        public TimeSpan? _startTime;
        public TimeSpan? _endTime;
        public CustomerService(IMessageService messageService, TimeSpan? startTime, TimeSpan? endTime, ILogEntry AddLogsr)
        {
            _AddLogsr = AddLogsr;
            _startTime = startTime;
            _endTime = endTime;
            _messageService = messageService;
        }
        public bool HasEmailBeenSent()
        {
            _AddLogsr.AddLogs("HasEmailBeenSent Method Started");

            try
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                if (_startTime == null || _endTime == null)
                {
                    return false;
                }

                if ((now > _startTime) && (now < _endTime))
                {
                    _messageService.SendEmail();
                    _AddLogsr.AddLogs("HasEmailBeenSent Method ended");
                    return true;
                }
                else
                {
                    _AddLogsr.AddLogs("HasEmailBeenSent :  Email Sent Failed Due To Time Constraints");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _AddLogsr.AddLogs($"HasEmailBeenSent : Email Sent Failed, exception  : {ex.Message}");
                return false;
            }
        }
    }


}
