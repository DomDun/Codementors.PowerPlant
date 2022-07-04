﻿using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PowerPlantCzarnobyl.MVC.Controllers
{
    public class InspectionsController : Controller
    {
        private readonly InspectionService _inspectionService;

        public InspectionsController()
        {
            _inspectionService = new InspectionService(new InspectionRepository());
        }
        // GET: Inspections
        public ActionResult Index()
        {
            return View(_inspectionService.GetAllInspections());
        }

        //// GET: Inspections/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Inspections/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Inspections/Create
        [HttpPost]
        public async Task<ActionResult> Create(Inspection inspection)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }
                await _inspectionService.AddInspectionAsync(inspection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Inspections/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Inspections/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Inspections/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Inspections/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}