using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using System.Web.Mvc;

namespace PowerPlantCzarnobyl.MVC.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
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
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Members/Create
        [HttpPost]
        public ActionResult Create(Member member)
        {
            try
            {

                if (!ModelState.IsValid || member.Role != "Admin" && member.Role != "User" && member.Role != "Engineer")
                {
                    return View("Error");
                }
                _memberService.Add(member);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        //// GET: Members/Delete/login
        public ActionResult Delete(string login)
        {
            var member = _memberService.GetMember(login);
            return member == null 
                ? View("Error") 
                : View(member);
        }

        //// POST: Members/Delete/login
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
