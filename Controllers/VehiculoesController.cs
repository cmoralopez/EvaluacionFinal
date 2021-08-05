using Autos.Model;
using Autos.ModelosNuevos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionFinal.Controllers
{
    [Authorize]
    public class VehiculoesController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public VehiculoesController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Owner,Client")]
        // GET: VehiculoesController
        public ActionResult Index()
        {
            List<Vehiculo> lstVehiculo = _context.Vehiculos.ToList();
            return View(lstVehiculo);
        }
        [Authorize(Roles = "Owner,Client")]
        // GET: VehiculoesController/Details/5
        public ActionResult Details(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        // POST: VehiculoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {
            try
            {
                _context.Add(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Edit/5
        public ActionResult Edit(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize(Roles = "Owner")]
        // POST: VehiculoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if(id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }
        [Authorize(Roles = "Owner")]
        // GET: VehiculoesController/Delete/5
        public ActionResult Desactivar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Owner")]
        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
