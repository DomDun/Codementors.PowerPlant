using Moq;
using NUnit.Framework;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;

namespace PowerPlantCzarnobyl.Tests
{
    [TestFixture]
    public class ErrorServiceTests
    {
        private Mock<IErrorsRepository> _errorsRepositoryMock;
        private Mock<IDateProvider> _dateProviderMock;

        private readonly DateTime _now = new DateTime(1992, 11, 02, 20, 10, 00);
        private readonly string _user = "Domin";
        private readonly string _machineName = "Turbine";
        private readonly string _parameter = "RotationSpeed";
        private PowerPlantDataSetData _plant;
        private ErrorService _sut;

        [SetUp]
        public void Setup()
        {
            _errorsRepositoryMock = new Mock<IErrorsRepository>();
            _dateProviderMock = new Mock<IDateProvider>();
            _dateProviderMock
                .SetupGet(x => x.Now)
                .Returns(_now);

            _plant = new PowerPlantDataSetData()
            {
                PlantName = "Czarnobyl",
            };

            _sut = new ErrorService(_errorsRepositoryMock.Object, _dateProviderMock.Object);
        }

        [Test]
        public void CheckValue_AssetParameterData_ParameterIsOutOfTheRange()
        {
            var value = new AssetParameterData
            {
                MinValue = 2000,
                MaxValue = 2200,
                CurrentValue = 2400
            };

            var success = _sut.CheckValue(_machineName, _parameter, value, _plant, _user);

            Assert.AreEqual(false, success);
        }

        [Test]
        public void CheckValue_AssetParameterData_ParameterIsInRange()
        {
            var value = new AssetParameterData
            {
                MinValue = 2000,
                MaxValue = 2200,
                CurrentValue = 2100
            };

            var success = _sut.CheckValue(_machineName, _parameter, value, _plant, _user);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void AddError_Error_ErrorIsAddedSuccesfully()
        {
            var value = new AssetParameterData
            {
                MinValue = 2000,
                MaxValue = 2200,
                CurrentValue = 2400
            };

            var errors = new List<Error>();

            _errorsRepositoryMock
                .Setup(x => x.AddError(Capture.In(errors)));

            var success = _sut.CheckValue(_machineName, _parameter, value, _plant, _user);

            Assert.AreEqual(false , success);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(_plant.PlantName, errors[0].PlantName);
            Assert.AreEqual(_machineName, errors[0].MachineName);
            Assert.AreEqual(_parameter, errors[0].Parameter);
            Assert.AreEqual(_now, errors[0].ErrorTime);
            Assert.AreEqual(_user, errors[0].LoggedUser);
            Assert.AreEqual(value.MinValue, errors[0].MinValue);
            Assert.AreEqual(value.MaxValue, errors[0].MaxValue);
        }

        [Test]
        public void AddError_Error_ErrorIsNotAdded()
        {
            var value = new AssetParameterData
            {
                MinValue = 2000,
                MaxValue = 2200,
                CurrentValue = 2100
            };

            var errors = new List<Error>();

            _errorsRepositoryMock
                .Setup(x => x.AddError(Capture.In(errors)));

            var success = _sut.CheckValue(_machineName, _parameter, value, _plant, _user);

            Assert.AreEqual(true, success);
            Assert.AreEqual(0, errors.Count);
        }
    }
}
