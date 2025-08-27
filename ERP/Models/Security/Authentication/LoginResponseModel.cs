namespace ERP.Models.Security.Authentication
{
    public class LoginResponseModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string jwt { get; set; }

        public List<string> roles { get; set; } = new List<string>();
    }
}