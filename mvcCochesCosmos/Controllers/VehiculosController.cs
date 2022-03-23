using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvcCochesCosmos.Models;
using mvcCochesCosmos.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mvcCochesCosmos.Controllers {
    public class VehiculosController : Controller {
        private CochesCosmosService service;

        public VehiculosController(CochesCosmosService service) {
            this.service = service;
        }
        public IActionResult CreateCosmos() {
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> CreateCosmos(string accion) {
            await this.service.CreateDatabaseAsync();
            ViewData["MENSAJE"] = "Base de datos y container generados";
            return View();
        }
        public async Task<IActionResult> Index() {
            List<Vehiculo> coches = await this.service.GetVehiculosAsync();
            return View(coches);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string accion) {
            await this.service.CreateDatabaseAsync();
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        public async Task<IActionResult> Create(Vehiculo car, string existemotor) {
            if (existemotor == null) {
                car.Motor = null;
            }
            await this.service.AddVehiculosAsync(car);
            return RedirectToAction("Coches");

        }
        public async Task<IActionResult> Details(string id) {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }
        public async Task<IActionResult> Edit(string id) {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Vehiculo car) {
            await this.service.UpdateVehiculoAsinc(car);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id) {
            await this.service.DeleteVehiculoAsync(id);
            return RedirectToAction("Index");

        }
    }
}
