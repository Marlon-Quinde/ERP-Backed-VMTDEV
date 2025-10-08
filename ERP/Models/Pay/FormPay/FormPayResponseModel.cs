namespace ERP.Models.Pay.FormPay
{
    public class FormPayResponseModel
    {
        public int FormPayId { get; set; }

        public string? FormPayDescription { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
