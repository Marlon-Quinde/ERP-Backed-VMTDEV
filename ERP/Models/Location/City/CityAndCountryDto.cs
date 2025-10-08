using ERP.Models.Location.Country;

namespace ERP.Models.Location.City
{
    public class CityAndCountryDto
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? State { get; set; }
        public DateTime? DatetimeAct { get; set; }
        public DateTime? DatetimeReg { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }

    }
}
