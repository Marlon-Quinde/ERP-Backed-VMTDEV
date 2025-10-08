using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Location.City;
using ERP.Models.Location.Country;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Diagnostics.Metrics;

namespace ERP.Bll.Location
{
    public class LocationBll : ILocationBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public LocationBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token requerido");

            MethodsHelper<object> metHel = new MethodsHelper<object>();
            var decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            else
                throw new UnauthorizedAccessException("Token inválido o expirado");
        }

        public ResponseGeneralModel<string?> CreateCity(CityRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                // PAÍS
                var CityExist = _context.Ciudads
                    .FirstOrDefault(p => p.CiudadNombre.ToUpper() == request.CityName.ToUpper());

                if (CityExist == null)
                {
                    var LastCity = _context.Ciudads.OrderByDescending(p => p.CiudadId).FirstOrDefault();
                    int NewCityId = (LastCity == null) ? 1 : LastCity.CiudadId + 1;

                    CityExist = new Ciudad
                    {
                        CiudadId = NewCityId,
                        CiudadNombre = request.CityName,
                        PaisId = request.CountryId,
                        Estado = 1,
                        FechaHoraAct = DateTime.Now,
                        UsuIdReg = sessMod.id,
                        FechaHoraReg = DateTime.Now
                    };
                    _context.Ciudads.Add(CityExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.CityCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CityIncorrect, ex.ToString());
            }
        }

        public List<Pai> GetCountry()
        {
            return _context.Pais.ToList();
        }

        public List<Ciudad> GetCity()
        {
            return _context.Ciudads
                .Include(c => c.Pais)
                .ToList();
        }

        public ResponseGeneralModel<string?> CreateCountry(CountryRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                // PAÍS
                var CountryExist = _context.Pais
                    .FirstOrDefault(p => p.PaisNombre.ToUpper() == request.CountryName.ToUpper());

                if (CountryExist == null)
                {
                    var LastCountr = _context.Pais.OrderByDescending(p => p.PaisId).FirstOrDefault();
                    int NewCountryId = (LastCountr == null) ? 1 : LastCountr.PaisId + 1;

                    CountryExist = new Pai
                    {
                        PaisId = NewCountryId,
                        PaisNombre = request.CountryName,
                        Estado = 1,
                        FechaHoraAct = DateTime.Now,
                        UsuIdReg = sessMod.id,
                        FechaHoraReg = DateTime.Now
                    };
                    _context.Pais.Add(CountryExist);
                    _context.SaveChanges();
                }

                
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.CountryCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CountryIncorrect, ex.ToString());
            }
        }

        public List<CityAndCountryDto> GetCityAndCountry()
        {
            return _context.Ciudads
                .Include(c => c.Pais)
                .Select(c => new CityAndCountryDto
                {
                    CityID = c.CiudadId,
                    CityName = c.CiudadNombre,
                    State = (short?)(c.Estado ?? 0),
                    DatetimeAct = c.FechaHoraAct,
                    CountryId = c.PaisId,
                    UsuIdAct = c.UsuIdAct,
                    UsuIdReg = c.UsuIdReg,
                    CountryName = c.Pais != null ? c.Pais.PaisNombre : null
                })
                .ToList();
        }
        public List<CountryAndCityDto> GetCountryAndCity()
        {
            return _context.Pais
                .Include(p => p.Ciudads)
                .Select(p => new CountryAndCityDto
                {
                    CountryId = p.PaisId,
                    CountryName = p.PaisNombre,
                    State = (short?)(p.Estado ?? 0),
                    DatetimeReg = p.FechaHoraReg, 
                    DatetimeAct = p.FechaHoraAct,
                    UsuIdReg = p.UsuIdReg,       
                    UsuIdAct = p.UsuIdAct,
                    Cities = p.Ciudads.Select(c => new CityResponseModel
                    {
                        CityId = c.CiudadId,
                        CityName = c.CiudadNombre,
                        CountryId = c.PaisId,
                        State = (short?)(c.Estado ?? 0),
                        DatetimeReg = c.FechaHoraReg,
                        DatetimeAct = c.FechaHoraAct,
                        UsuIdReg = c.UsuIdReg,
                        UsuIdAct = c.UsuIdAct
                    }).ToList()
                })
                .ToList();
        }

    }
}
