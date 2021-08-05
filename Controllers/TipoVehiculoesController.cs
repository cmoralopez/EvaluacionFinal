using Autos.Model;
using Autos.ModelosNuevos;
using Autos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionFinal.Controllers
{
    [Authorize]
    public class TipoVehiculoesController : Controller
    {

        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculoesController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        public void Combox()
        {
            ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(x => new ViewModelVehiculoTipoVehiculo
            {
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                Estado = x.Estado
            }).Where(x => x.Estado == 1).ToList(), "Codigo", "Nombre", "Descripcion");
        }
        [Authorize (Roles = "Owner, Client")]
        // GET: TipoVehiculoesController
        public ActionResult Index()
        {
            //List<TipoVehiculo> lstTipoVehiculo = _context.TipoVehiculos.ToList();
            List<ViewModelVehiculoTipoVehiculo> lstTipoVehiculo = _context.TipoVehiculos.Select(x => new ViewModelVehiculoTipoVehiculo
            {
                CodigoVehiculo = x.CodigoVehiculo,
                Codigo = x.Codigo,
                Nombre = $"{x.CodigoVehiculoNavigation.Nombre}",
                Estado = x.Estado
            }).ToList();
            return View(lstTipoVehiculo);
        }
        [Authorize(Roles = "Owner,Client")]
        // GET: VehiculoesController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(tipoVehiculo);
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Owner")]
        // POST: VehiculoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipoVehiculo)
        {
            try
            {
                tipoVehiculo.Estado = 1;
                _context.Add(tipoVehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipoVehiculo);
            }
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(tipoVehiculo);
        }
        [Authorize(Roles = "Owner")]
        // POST: VehiculoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(tipoVehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipoVehiculo);
            }
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Delete/5
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            tipoVehiculo.Estado = 0;
            _context.Update(tipoVehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Owner")]
        public ActionResult Activar(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            tipoVehiculo.Estado = 1;
            _context.Update(tipoVehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
