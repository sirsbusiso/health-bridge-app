using HealthbridgeApp.Interface;
using HealthbridgeApp.Models;
using HealthbridgeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthbridgeApp.BusinessLogic
{
    public class PatientBusiness : IPatient
    {
        /// <summary>
        /// This method creates a new patient record on the database.
        /// It returns a string successfully messege.
        /// </summary>
        public string CreatePatient(PatientViewModel model)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var get = _db.tb_Patient.FirstOrDefault(x => x.IdNumber == model.IdNumber);
                if(get == null)
                {
                    var p = new tb_Patient
                    {
                        FirstName = model.FirstName,
                        IdNumber = model.IdNumber,
                        LastName = model.LastName,
                    };
                    _db.tb_Patient.Add(p);
                    _db.SaveChanges();
                    return "Successfully Created";
                }
                else
                {
                    return "Patient Already Exist";
                }
               
            }
        }

        /// <summary>
        /// This method removes the patient record that matches the parameter patient id from the database.
        /// It returns a string successfully messege.
        /// </summary>
        public string DeletePatient(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var p = _db.tb_Patient.FirstOrDefault(x => x.PatientId == id);
                if(p != null)
                {
                    if (Existing(p.PatientId))
                    {
                        return "You can not delete patients with invoices";
                    }
                    else
                    {
                        _db.tb_Patient.Remove(p);
                        _db.SaveChanges();
                        return "Successfully Deleted";
                    }
                }
                else
                {
                    return "Not Found";
                }
            }
        }

        /// <summary>
        /// This method checks if there is an invoice belonging to a patient from the invoice table by passing the patient id parameter.
        /// It returns a boolean value.
        /// </summary>
        public bool Existing(long id)
        {
            using(var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Invoice.Any(x => x.PatientId == id);
            }
        }

        /// <summary>
        /// This method returns a patient record that matches patient id parameter
        /// It returns a patient object.
        /// </summary>
        public PatientViewModel GetById(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Patient.Where(x => x.PatientId == id).Select(x => new PatientViewModel
                {
                    PatientId = x.PatientId,
                    FirstName = x.FirstName,
                    IdNumber = x.IdNumber,
                    LastName = x.LastName
                }).FirstOrDefault();
            }
        }

        /// <summary>
        /// This method returns all patient records from the database
        /// It returns a patient a list of patients.
        /// </summary>
        public List<PatientViewModel> GetPatients()
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Patient.Select(x => new PatientViewModel
                {
                    PatientId = x.PatientId,
                    FirstName = x.FirstName,
                    IdNumber = x.IdNumber,
                    LastName = x.LastName
                }).ToList();
            }
        }


        /// <summary>
        /// This method returns all patient records from the database that contains the numbers from the id number parameter
        /// It is used for autocomplete to search for a patient.
        /// It returns a patient a list of patients.
        /// </summary>
        public List<PatientViewModel> GetByIdNumber(string IdNumber)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var patients = _db.tb_Patient.Where(x => x.IdNumber.Contains(IdNumber)).Select(x => 
                    new PatientViewModel 
                    { 
                        IdNumber = x.IdNumber,
                        LastName = x.LastName,
                        FirstName = x.FirstName,
                        PatientId = x.PatientId 
                    }).ToList();

                return patients;             
            }
        }

        /// <summary>
        /// This method update the patient details.
        /// It returns a string successfully message.
        /// </summary>
        public string UpdatePatient(PatientViewModel model)
        {
            using(var _db = new HealthBridgeDBEntities())
            {
                var p = _db.tb_Patient.FirstOrDefault(x => x.PatientId == model.PatientId);
                if(p !=null)
                {
                    p.IdNumber = model.IdNumber;
                    p.LastName = model.LastName;
                    p.FirstName = model.FirstName;

                    _db.SaveChanges();
                    return "Successfully Updated";
                }
                else
                {
                    return "Update Failed";
                }
            }
        }

       
    }
}