namespace DevTools.Common.General
{
    public class ApiMessage
    {
        public ApiMessage()
        {
        }

        public ApiMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}