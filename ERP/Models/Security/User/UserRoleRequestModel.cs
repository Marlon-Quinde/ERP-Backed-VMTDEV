namespace ERP.Models.Security.User
{
    public class UserRoleRequestModel
    {
        public int UsuRolId { get; set; }
        public int? UsuId { get; set; }
        public int? RolId { get; set; }
        public short? State { get; set; }
        public DateTime? DatetimeReg { get; set; }
        public DateTime? DatetimeAct { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }
    }
}
