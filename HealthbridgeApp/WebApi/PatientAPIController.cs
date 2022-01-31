using HealthbridgeApp.BusinessLogic;
using HealthbridgeApp.Interface;
using HealthbridgeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HealthbridgeApp.WebApi
{
    public class PatientAPIController : ApiController
    {
        private IPatient _patientBusiness;

        public PatientAPIController(IPatient patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/Patient")]
        public IEnumerable<PatientViewModel> Get()
        {
            var list = _patientBusiness.GetPatients();

            return list.AsEnumerable();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Patient/{id}")]
        public PatientViewModel GetDetails(int id)
        {
            return _patientBusiness.GetById(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Patient")]
        public string Post(PatientViewModel model)
        {
            var result = _patientBusiness.CreatePatient(model);

            return result;
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Patient")]
        public string Put(PatientViewModel model)
        {
            var result = _patientBusiness.UpdatePatient(model);

            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Patient/{id}")]
        public string Delete(int id)
        {
            var result = _patientBusiness.DeletePatient(id);

            return result;
        }

        [HttpGet]
        [Route("api/Patient/GetById/{id}")]
        public List<PatientViewModel> GetByIdNumber(string id)
        {
            var result = _patientBusiness.GetByIdNumber(id);

            return result;
        }

    }
}
