namespace ERP.Models.Security.Module
{
    public class ModuleResponseModel
    {
        public int ModuleId { get; set; }

        public string? ModuleDescription { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
