namespace ERP.Helper.Models.WorkerProcess
{
    public class WorkerProcessMailModel
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<SmtpSendRequestModel_File>? file { get; set; } = null;
    }
}
