using ERP.Models.Location.City;

namespace ERP.Models.Location.Country
{
    public class CountryAndCityDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public short? State { get; set; }
        public DateTime? DatetimeReg { get; set; }
        public DateTime? DatetimeAct { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }
        public List<CityResponseModel> Cities { get; set; }
    }
}
