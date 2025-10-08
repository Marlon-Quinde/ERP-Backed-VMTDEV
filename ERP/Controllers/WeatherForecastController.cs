using ERP.Bll.Security.User;
using ERP.CoreDB;
using ERP.Filters;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace ERP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ServiceFilter(typeof(SessionUserFilter))]
    public class WeatherForecastController : ControllerBase
    {
        BaseErpContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        IUserBll userBll;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserBll userBll, BaseErpContext context)
        {
            _logger = logger;
            this.userBll = userBll;

            _context = context;
        }

        [HttpGet("EncryptPass")]
        public ResponseGeneralModel<string> EncryptPass(string text)
        {
            return (new MethodsHelper<string>()).EncryptDataByMethod("passUser", text);
        }

        [HttpGet("testDb")]
        public ResponseGeneralModel<List<Usuario>?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<List<Usuario>?>(200, userBll.GetUsers(), "");
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<Usuario>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        //[HttpPost("testDbCommit")]
        //public ResponseGeneralModel<string?> testDbCommit(TestDbCommitRequestModel requestModel)
        //{
        //    try
        //    {
        //        _context.Database.BeginTransaction();
        //        Pai? paisM = _context.Pais.OrderByDescending(item => item.PaisId).FirstOrDefault();
        //        int idPais = (paisM == null) ? 1 : paisM.PaisId + 1;
        //        Pai? paisFind = _context.Pais.FirstOrDefault(item => item.PaisNombre.ToUpper() == requestModel.namePais.ToUpper());
        //        if (paisFind == null)
        //        {
        //            paisFind = new Pai()
        //            {
        //                PaisId = idPais,
        //                PaisNombre = requestModel.namePais,
        //                Estado = 1,
        //                FechaHoraAct = DateTime.Now
        //            };
        //            _context.Pais.Add(paisFind);
        //            _context.SaveChanges();
        //        }


        //        Ciudad? cdad = _context.Ciudads.OrderByDescending(item => item.PaisId).FirstOrDefault();
        //        int idCiudad = (cdad == null) ? 1 : cdad.CiudadId + 1;
        //        Ciudad? cdadFind = _context.Ciudads.FirstOrDefault(item => item.CiudadNombre.ToUpper() == requestModel.nameCiudad.ToUpper());
        //        if (cdadFind == null)
        //        {
        //            cdadFind = new Ciudad()
        //            {
        //                CiudadId = idCiudad,
        //                PaisId = paisFind.PaisId,
        //                CiudadNombre = requestModel.nameCiudad,
        //                Estado = 1,
        //                FechaHoraAct = DateTime.Now
        //            };
        //            _context.Ciudads.Add(cdadFind);
        //            _context.SaveChanges();
        //        }

        //        _context.Database.CommitTransaction();

        //        return new ResponseGeneralModel<string?>(200, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        _context.Database.RollbackTransaction();
        //        return new ResponseGeneralModel<string?>(500, null, "error", ex.ToString());
        //    }
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}