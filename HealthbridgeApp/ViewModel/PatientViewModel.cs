using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthbridgeApp.ViewModel
{
    public class PatientViewModel
    {
        public long PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }

    }
}