namespace ADSGroupSMS.Models
{
    public class OperationResult
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public OperationResult(bool result,string message)
        {
            isSuccess = result;
            message = Message;
        }
    }
}
