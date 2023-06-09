using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using PatientManagementAPI.Controllers;
using PatientManagementAPI.Models;
using PatientManagementAPI.Repositories;

namespace web_api_tests
{
    public class PatientManagementTest
    {
        private readonly PatientsController _controller;
        private readonly IPatientRepository _service;
        private readonly ILogger<PatientsController> _logger;
        private readonly IConfiguration _config;

        public PatientManagementTest(ILogger<PatientsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _service = new PatientRepository(_logger, _config);
            _controller = new PatientsController(_logger, _service);
        }

        [Fact]
        public void Get_AllPatients_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.GetAllPatients();
            Assert.IsType<OkObjectResult>(okResult as IEnumerable<Patients>);
        }

        [Fact]
        public void Get_AllPatients_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.GetAllPatients() as IEnumerable<Patients>;
            var items = Assert.IsType<List<Patients>>(okResult);
            Assert.Equal(6, items.Count);
        }

        [Fact]
        public void GetPatientById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = _controller.GetPatientByID(1);
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetPatientById_KnownIdPassed_ReturnsOkResult()
        {
            var patientId = 5;
            var okResult = _controller.GetPatientByID(patientId);
            Assert.IsType<OkObjectResult>(okResult as IEnumerable<Patients>);
        }

        [Fact]
        public void GetPatientById_KnownIdPassed_ReturnsRightItem()
        {
            var patientId = 7;
            var okResult = _controller.GetPatientByID(patientId) as IEnumerable<Patients>;
            Assert.IsType<Patients>(okResult);
            Assert.Equal(patientId, (okResult as Patients).PatientID);
        }
    }
}