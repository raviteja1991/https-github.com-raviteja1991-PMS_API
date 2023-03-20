using Dapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagementAPI.Controllers;
using PatientManagementAPI.Models;
using System.Data.SqlClient;

namespace PatientManagementAPI.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<PatientsController> _logger;

        public PatientRepository(ILogger<PatientsController> logger, IConfiguration config)
        {
            _logger = logger;

            _config = config;
        }

        public async Task<IEnumerable<Patients>> GetAllPatients()
        {
            IEnumerable<Patients> patients = Enumerable.Empty<Patients>();
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

                patients = await SelectAllPatients(connection);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return patients.ToList();
        }

        public async Task<Patients> GetPatientByID(int patientID)
        {
            var patient = new Patients { };

            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

                patient = await connection.QueryFirstAsync<Patients>("Select * from MainSchema.Patients where PatientID = @PatientID",
                    new { PatientID = patientID });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return patient;
        }

        public async Task<IEnumerable<Patients>> CreatePatient(Patients patient)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            try
            {
                await connection.ExecuteAsync("insert into MainSchema.Patients (FirstName, LastName, FullName, DOB, Age, Gender, PatientAddress, EmailAddress, PhoneNumber) " +
                "values (@FirstName, @LastName, @FullName, @DOB, @Age, @Gender, @PatientAddress, @EmailAddress, @PhoneNumber)", patient);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return await SelectAllPatients(connection);
        }

        public async Task<IEnumerable<Patients>> UpdatePatient(Patients patient)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            try
            {
                await connection.ExecuteAsync("update MainSchema.Patients set FirstName=@FirstName, LastName=@LastName," +
                    " FullName=@FullName, DOB=@DOB, Age=@Age, Gender=@Gender, PatientAddress=@PatientAddress, EmailAddress=@EmailAddress," +
                    " PhoneNumber=@PhoneNumber where PatientID=@PatientID", patient);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return await SelectAllPatients(connection);
        }

        public async Task<IEnumerable<Patients>> DeletePatient(int patientID)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            try
            {
                await connection.ExecuteAsync("delete from mainschema.patients where PatientID=@PatientID", new { PatientID = patientID });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return await SelectAllPatients(connection);
        }

        private static async Task<IEnumerable<Patients>> SelectAllPatients(SqlConnection connection)
        {
            return await connection.QueryAsync<Patients>("Select * from MainSchema.Patients");
        }
    }
}
