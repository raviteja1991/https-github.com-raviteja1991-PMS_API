using Microsoft.AspNetCore.Mvc;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patients>> GetAllPatients();
        Task<Patients> GetPatientByID(int patinetID);
        Task<IEnumerable<Patients>> CreatePatient(Patients patient);
        Task<IEnumerable<Patients>> UpdatePatient(Patients patient);
        Task<IEnumerable<Patients>> DeletePatient(int patinetID);
    }
}
