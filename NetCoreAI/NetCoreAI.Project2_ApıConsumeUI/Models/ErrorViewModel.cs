namespace NetCoreAI.Project2_ApıConsumeUI.Models
{
    public class ErrorViewModel
    {
        public string? RequestId
        {
            get; set;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
