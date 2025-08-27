namespace ERP.Models.Security.Authentication
{
    public class ClientRequestModel
    {
        public int id { get; set; }
        public string ruc { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string lastname1 { get; set; }
        public string lastname2 { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string state { get; set; }

    }
}
