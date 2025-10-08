
using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Location.City;
using ERP.Models.Location.Country;

namespace ERP.Bll.Location
{
    public interface ILocationBll
    {
        ResponseGeneralModel<string?> CreateCity(CityRequestModel request);
        public List<Pai> GetCountry();
        ResponseGeneralModel<string?> CreateCountry(CountryRequestModel request);
        public List<Ciudad> GetCity();
        public List<CountryAndCityDto> GetCountryAndCity();
        public List<CityAndCountryDto> GetCityAndCountry();





    }
}
