namespace ERP.Models.Location.City
{
    public class CityResponseModel
    {
        public int CityId { get; set; }

        public string? CityName { get; set; }
        public int? CountryId { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }

    }
}
