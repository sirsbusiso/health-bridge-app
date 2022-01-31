using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthbridgeApp.Models;
using HealthbridgeApp.ViewModel;

namespace HealthbridgeApp.Interface
{
    public interface IPatient
    {
        List<PatientViewModel> GetPatients();
        string CreatePatient(PatientViewModel model);
        string UpdatePatient(PatientViewModel model);
        string DeletePatient(long id);
        PatientViewModel GetById(long id);
        List<PatientViewModel> GetByIdNumber(string IdNumber);
    }
}