using Dapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagementAPI.Models;
using System.Data.SqlClient;

namespace PatientManagementAPI.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _config;

        public PatientRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Patients>> GetAllPatients()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            IEnumerable<Patients> patients = await SelectAllPatients(connection);
            return patients.ToList();
        }

        public async Task<Patients> GetPatientByID(int patientID)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var patient = await connection.QueryFirstAsync<Patients>("Select * from MainSchema.Patients where PatientID = @PatientID",
                new { PatientID = patientID });
            return patient;
        }

        public async Task<IEnumerable<Patients>> CreatePatient(Patients patient)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("insert into MainSchema.Patients (FirstName, LastName, FullName, DOB, Age, Gender, PatientAddress, EmailAddress, PhoneNumber) " +
            "values (@FirstName, @LastName, @FullName, @DOB, @Age, @Gender, @PatientAddress, @EmailAddress, @PhoneNumber)", patient);

            return await SelectAllPatients(connection);
        }

        public async Task<IEnumerable<Patients>> UpdatePatient(Patients patient)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("update MainSchema.Patients set FirstName=@FirstName, LastName=@LastName," +
                " FullName=@FullName, DOB=@DOB, Age=@Age, Gender=@Gender, PatientAddress=@PatientAddress, EmailAddress=@EmailAddress," +
                " PhoneNumber=@PhoneNumber where PatientID=@PatientID", patient);

            return await SelectAllPatients(connection);
        }

        public async Task<IEnumerable<Patients>> DeletePatient(int patientID)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("delete from mainschema.patients where PatientID=@PatientID", new { PatientID = patientID });

            return await SelectAllPatients(connection);
        }

        private static async Task<IEnumerable<Patients>> SelectAllPatients(SqlConnection connection)
        {
            return await connection.QueryAsync<Patients>("Select * from MainSchema.Patients");
        }
    }
}
