using Moq;
using NUnit.Framework;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System.Collections.Generic;

namespace PowerPlantCzarnobyl.Tests
{
    [TestFixture]
    public class LoginHandlerTests
    {
        private Mock<ILoginHandler> _loginHandlerMock;
        private Mock<IMembersService> _membersServiceMock;
        private Mock<IMembersRepository> _membersRepositoryMock;

        private LoginHandler _sut;

        [SetUp]
        public void Setup()
        {
            _loginHandlerMock = new Mock<ILoginHandler>();
            _membersServiceMock = new Mock<IMembersService>();
            _membersRepositoryMock = new Mock<IMembersRepository>();

            _sut = new LoginHandler(_membersServiceMock.Object);
        }

        [Test]
        public void AddMember_Member_AdminCanAddMember()
        {
            _loginHandlerMock
                .SetupRemove(x => x.Clear());

            var loggedMember = new Member();
            loggedMember.Login = "admin";
            loggedMember.Role = "Admin";

            //var members = new List<Member>();

            //_loginHandlerMock
            //    .Setup(x => x.AddMember(Capture.In(members)));

            var success = _sut.AddMember(loggedMember);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void AddMember_Member_UserCantAddMember()
        {
            var loggedMember = new Member();
            loggedMember.Login = "user";
            loggedMember.Role = "User";

            var members = new List<Member>();

            //_membersRepositoryMock
            //    .Setup(x => x.Add(Capture.In(members)));

            var success = _sut.AddMember(loggedMember);

            Assert.AreEqual(false, success);
            Assert.AreEqual(0, members.Count);
        }

        [Test]
        public void DeleteMember_Member_AdminCanDeleteMember()
        {
            var loggedMember = new Member();
            loggedMember.Login = "admin";
            loggedMember.Role = "Admin";

            var success = _sut.DeleteMember(loggedMember);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void DeleteMember_Member_UserCantDeleteMember()
        {
            var loggedMember = new Member();
            loggedMember.Login = "user";
            loggedMember.Role = "User";

            var success = _sut.DeleteMember(loggedMember);

            Assert.AreEqual(false, success);
            //todo: naprawić te testy
        }

    }
}
