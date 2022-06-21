using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using PowerPlantCzarnobyl.WebApi.Server.Models;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/members")]
    public class MemberController : ApiController
    {
        private readonly MemberService _memberService;

        public MemberController()
        {
            _memberService = new MemberService(new MembersRepository());
        }

        [HttpPost]
        [Route("")]
        public bool AddMember([FromBody] Member member)
        {
            return _memberService.Add(member);
        }

        [HttpPost]
        [Route("credentials")]
        public bool Login([FromBody] MemberCredentials MemberCredentials)
        {
            return _memberService.CheckUserCredentials(MemberCredentials.Login, MemberCredentials.Password);
        }

        [HttpGet]
        [Route("{login}")]
        public Member CheckMemberRole(string login)
        {
            Member member = _memberService.CheckMemberRole(login);
            return member;
        }

        [HttpDelete()]
        [Route("{login}")]
        public bool DeleteMember(string login)
        {
            bool result = _memberService.Delete(login);
            return result;
        }
    }
}
