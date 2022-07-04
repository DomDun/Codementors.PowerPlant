using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Details(string login)
        {
            var member = _memberService.GetMember(login);
            return member == null
                ? View("Error")
                : View(member);
        }

        //// GET: Members/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Members/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Member member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                _memberService.Add(member);

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

        //// GET: Members/Delete/login
        [Authorize]
        public ActionResult Delete(string login)
        {
            var member = _memberService.GetMember(login);
            return member == null 
                ? View("Error") 
                : View(member);
        }

        //// POST: Members/Delete/login
        [Authorize]
        [HttpPost]
        public ActionResult Delete(string login, FormCollection collection)
        {
            try
            {
                if (!_memberService.Delete(login))
                {
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
