namespace ERP.Models.Security.Options
{
    public class OptionResponseModel
    {
        public int OptionId { get; set; }

        public string? OptionDescription { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }

        public int? ModuleId { get; set; }
    }
}
