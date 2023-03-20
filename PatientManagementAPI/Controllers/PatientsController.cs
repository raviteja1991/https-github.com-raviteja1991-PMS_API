using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientManagementAPI.Repositories;
using PatientManagementAPI.Models;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace PatientManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;

        private readonly IPatientRepository _patienrRepo;

        public PatientsController(ILogger<PatientsController> logger, IPatientRepository patienrRepo)
        {
            _logger = logger;

            _patienrRepo = patienrRepo;
        }

        [HttpGet("PatientsDetail")]        
        public async Task<ActionResult<IEnumerable<Patients>>> GetAllPatients()
        {
            return Ok(await _patienrRepo.GetAllPatients());
        }

        [HttpGet("PatientDetails/{patientID}")]
        public async Task<ActionResult<Patients>> GetPatientByID(int patientID)
        {
            return Ok(await _patienrRepo.GetPatientByID(patientID));
        }

        [HttpPost("CreatePatient")]
        public async Task<ActionResult<IEnumerable<Patients>>> CreatePatient(Patients patient)
        {
            return Ok(await _patienrRepo.CreatePatient(patient));
        }

        [HttpPut("UpdatePatient/{patientID}")]
        public async Task<ActionResult<IEnumerable<Patients>>> UpdatePatient(int patientID, Patients patient)
        {
            patient.PatientID = patientID;
            return Ok(await _patienrRepo.UpdatePatient(patient));
        }

        [HttpDelete("DeletePatient/{patientID}")]
        public async Task<ActionResult<IEnumerable<Patients>>> DeletePatient(int patientID)
        {
            return Ok(await _patienrRepo.DeletePatient(patientID));
        }
    }
}
