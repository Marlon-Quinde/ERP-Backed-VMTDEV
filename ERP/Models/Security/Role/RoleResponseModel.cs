namespace ERP.Models.Security.Role
{
    public class RoleResponseModel
    {
        public int? RolId { get; set; }
        public string? RoleDescrip { get; set; }
        public short? State { get; set; }
        public DateTime? DatetimeReg { get; set; }
        public DateTime? DatetimeAct { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }
    }
}
