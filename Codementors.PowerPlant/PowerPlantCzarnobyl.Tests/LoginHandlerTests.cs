using Moq;
using NUnit.Framework;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Tests
{
    [TestFixture]
    public class LoginHandlerTests
    {
        private Mock<IMembersService> _membersServiceMock;
        private Mock<IConsoleManager> _consoleManagerMock;
        private Mock<ICliHelper> _cliHelperMock;

        private LoginHandler _sut;

        [SetUp]
        public void Setup()
        {
            _membersServiceMock = new Mock<IMembersService>();
            _consoleManagerMock = new Mock<IConsoleManager>();
            _cliHelperMock = new Mock<ICliHelper>();

            _sut = new LoginHandler(_membersServiceMock.Object, _consoleManagerMock.Object, _cliHelperMock.Object);
        }

        [Test]
        public void AddMember_Member_AdminCanAddMember()
        {
            var member = new Member
            {
                Login = "admin",
                Role = "Admin"
            };

            var newMember = new Member();

            _cliHelperMock
                .Setup(x=>x.GetMemberFromAdmin())
                .Returns(newMember);

            _membersServiceMock
                .Setup(x => x.Add(newMember))
                .Returns(true);

            var success = _sut.AddMember(member);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void AddMember_Member_UserCantAddMember()
        {
            var member = new Member
            {
                Login = "user",
                Role = "User"
            };

            var success = _sut.AddMember(member);

            Assert.AreEqual(false, success);
        }

        [Test]
        public void DeleteMember_Member_AdminCanDeleteMember()
        {
            var loggedMember = new Member
            {
                Login = "admin",
                Password = "admin",
                Role = "Admin"
            };

            _cliHelperMock
                .Setup(x=>x.GetStringFromUser("Type Your password to confirm You are Admin"))
                .Returns(loggedMember.Password);
            _membersServiceMock
                .Setup(x => x.CheckUserCredentials(loggedMember.Password, loggedMember.Password))
                .Returns(true);
            _cliHelperMock
                .Setup(x => x.GetStringFromUser("Type login of member You want to delete"))
                .Returns("user");
            _membersServiceMock
                .Setup(x => x.Delete("user"))
                .Returns(true);

            var success = _sut.DeleteMember(loggedMember);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void DeleteMember_Member_UserCantDeleteMember()
        {
            var loggedMember = new Member
            {
                Login = "user",
                Role = "User"
            };

            var success = _sut.DeleteMember(loggedMember);

            Assert.AreEqual(false, success);
        }
    }
}
