using ERP.Bll.Location;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Location.City;
using ERP.Models.Location.Country;
using Microsoft.AspNetCore.Mvc;
using static ERP.Bll.Location.LocationBll;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Locations.CountryAndCity
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationBll locationBll;

        public LocationController(ILocationBll bll)
        {
            locationBll = bll;
        }

        [HttpGet("listar-Ciudad")]
        public ResponseGeneralModel<List<CityAndCountryDto>?> GetCity()  
        {
            try
            {
                return new ResponseGeneralModel<List<CityAndCountryDto>?>(200, locationBll.GetCityAndCountry(), MessageHelper.CityCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<CityAndCountryDto>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Ciudad")]
        public ResponseGeneralModel<string?> CreateCity([FromBody] CityRequestModel request)  
        {
            return locationBll.CreateCity(request);
        }

        [HttpGet("listar-País")]
        public ResponseGeneralModel<List<CountryAndCityDto>?> GetCountry()
        {
            try
            {
                return new ResponseGeneralModel<List<CountryAndCityDto>?>(200, locationBll.GetCountryAndCity(), MessageHelper.CountryCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<CountryAndCityDto>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Pais")]
        public ResponseGeneralModel<string?> CreateCountry([FromBody] CountryRequestModel request)
        {
            return locationBll.CreateCountry(request);
        }
    }
}
