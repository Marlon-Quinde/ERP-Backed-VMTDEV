using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.test;

namespace ERP.Bll.Location
{
    public class PaisBll : IPaisBll
    {
        private readonly BaseErpContext _context;

        public PaisBll(BaseErpContext context)
        {
            _context = context;
        }

        public ResponseGeneralModel<string?> CrearPaisYCiudad(TestDbCommitRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                // PAÍS
                var paisExistente = _context.Pais
                    .FirstOrDefault(p => p.PaisNombre.ToUpper() == request.namePais.ToUpper());

                if (paisExistente == null)
                {
                    var ultimoPais = _context.Pais.OrderByDescending(p => p.PaisId).FirstOrDefault();
                    int nuevoPaisId = (ultimoPais == null) ? 1 : ultimoPais.PaisId + 1;

                    paisExistente = new Pai
                    {
                        PaisId = nuevoPaisId,
                        PaisNombre = request.namePais,
                        Estado = 1,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Pais.Add(paisExistente);
                    _context.SaveChanges();
                }

                // CIUDAD
                var ciudadExistente = _context.Ciudads
                    .FirstOrDefault(c => c.CiudadNombre.ToUpper() == request.nameCiudad.ToUpper());

                if (ciudadExistente == null)
                {
                    var ultimaCiudad = _context.Ciudads.OrderByDescending(c => c.CiudadId).FirstOrDefault();
                    int nuevaCiudadId = (ultimaCiudad == null) ? 1 : ultimaCiudad.CiudadId + 1;

                    ciudadExistente = new Ciudad
                    {
                        CiudadId = nuevaCiudadId,
                        PaisId = paisExistente.PaisId,
                        CiudadNombre = request.nameCiudad,
                        Estado = 1,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Ciudads.Add(ciudadExistente);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, "País y ciudad creados correctamente");
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, "Error al registrar país/ciudad", ex.ToString());
            }
        }
    }
}
