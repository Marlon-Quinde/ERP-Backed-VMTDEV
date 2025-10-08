namespace ERP.Models.Security.User
{
    public class UserPermissionRequestModel
    {
        public int PermissionId { get; set; }

        public int? ModulId { get; set; }

        public int? OptionId { get; set; }

        public int? UsuId { get; set; }
        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
