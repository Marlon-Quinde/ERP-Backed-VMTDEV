namespace ERP.Models.Security.Authentication
{
    public class RegisterRequestModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int companyId { get; set; }
    }
}