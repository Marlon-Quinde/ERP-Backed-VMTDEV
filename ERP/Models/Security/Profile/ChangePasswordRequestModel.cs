namespace ERP.Models.Security.Profile
{
    public class ChangePasswordRequestModel
    {
        public string oldPassword {  get; set; }
        public string newPassword { get; set; }
        public string repeatNewPassword { get; set; }
    }
}
