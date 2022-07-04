using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerPlantCzarnobyl.MVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly MemberService _memberService;

        public MembersController()
        {
            _memberService = new MemberService(new MembersRepository());
        }
        // GET: Members
        public ActionResult Index()
        {
            return View(_memberService.GetAllMembers());
        }

        //// GET: Members/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Members/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Members/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Members/Edit/5
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

        //// GET: Members/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //// POST: Members/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
