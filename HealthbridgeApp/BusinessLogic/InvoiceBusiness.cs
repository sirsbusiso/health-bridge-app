using HealthbridgeApp.Interface;
using HealthbridgeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthbridgeApp.Models;

namespace HealthbridgeApp.BusinessLogic
{
    public class InvoiceBusiness : IInvoice
    {

        /// <summary>
        /// This method creates an invoice and the invoice lines.
        /// </summary>
        public string CreateInvoice(InvoiceViewModel model)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var i = new tb_Invoice
                {
                    InvoiceDateTime = model.InvoiceDateTime,
                    InvoiceTotal = 0,
                    PatientId = model.PatientId
                };

                foreach (var item in model.CreateInvoiceLineViewModels)
                {
                    var il = new tb_Invoice_Line
                    {
                        Qty = item.Qty,
                        Code = item.Code,
                        Description = item.Description,
                        LineTotal = item.LineTotal,
                        tb_Invoice = i
                    };

                    i.InvoiceTotal += il.LineTotal * (decimal)il.Qty;

                    _db.tb_Invoice_Line.Add(il);
                }

                _db.tb_Invoice.Add(i);

                //_db.tb_Invoice.Add(i);
                _db.SaveChanges();
                return "Successfully Created";
            }
        }

        /// <summary>
        /// This method creates an invoice line.
        /// </summary>
        public string CreateInvoiceLine(CreateInvoiceLineViewModel model)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var l = new tb_Invoice_Line()
                {
                    Code = model.Code,
                    Description = model.Description,
                    LineTotal = model.LineTotal,
                    Qty = model.Qty,
                    InvoiceId = model.InvoiceId
                };
                _db.tb_Invoice_Line.Add(l);
                _db.SaveChanges();
                return "Successfully Created";

            }
        }

        /// <summary>
        /// This method removes invoice from the database.
        /// It checks if all the invoice lines belonging to this invoice have been removed first if not, it does not delete if yes, it does.
        /// </summary>
        public string DeleteInvoice(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var i = _db.tb_Invoice.FirstOrDefault(x => x.InvoiceId == id);
                if (i != null)
                {
                    if (DeleteAllInvoiceLines(i.InvoiceId))
                    {
                        _db.tb_Invoice.Remove(i);
                        _db.SaveChanges();
                        return "Successfully Deleted";
                    }
                    else
                    {
                        return "Invoice Can Not Be Deleted";
                    }
                }
                else
                {
                    return "Not Found";
                }
            }
        }
        /// <summary>
        /// This method deletes all the invoice lines belonging to the invoice parameter id.
        /// It returns a boolean value.
        /// </summary>
        public bool DeleteAllInvoiceLines(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var i = _db.tb_Invoice_Line.Where(x => x.InvoiceId == id).ToList();
                if (i != null)
                {
                    foreach (var invoice in i)
                    {
                        _db.tb_Invoice_Line.Remove(invoice);
                        _db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// This method get the invoice line object by parameter id.
        /// It returns an object and map the results to a view model.
        /// </summary>
        public UpdateInvoiceViewModel GetInvoiceById(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Invoice_Line.Where(x => x.InvoiceLineId == id).Select(x => new UpdateInvoiceViewModel
                {
                    Code = x.Code,
                    Description = x.Description,
                    LineTotal = x.LineTotal,
                    Qty = x.Qty,
                    InvoiceId = (long)x.InvoiceId,
                    InvoiceLineId = x.InvoiceLineId
                }).FirstOrDefault();

            }
        }

        /// <summary>
        /// This method returns a list of invoice lines from the database.
        /// </summary>
        public List<AllInvoiceLineViewModel> GetInvoiceLines()
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Invoice_Line.Select(x => new AllInvoiceLineViewModel
                {
                    Code = x.Code,
                    Description = x.Description,
                    InvoiceDate = x.tb_Invoice.InvoiceDateTime,
                    InvoiceLineId = x.InvoiceLineId,
                    LineTotal = x.LineTotal,
                    Patient = x.tb_Invoice.tb_Patient.FirstName + " " + x.tb_Invoice.tb_Patient.LastName,
                    Qty = x.Qty,
                }).ToList();
            }
        }

        /// <summary>
        /// This method returns a list of invoices from the database.
        /// </summary>
        public List<InvoiceViewModel> GetInvoices()
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Invoice.Select(x => new InvoiceViewModel
                {
                    InvoiceDateTime = x.InvoiceDateTime,
                    InvoiceTotal = x.InvoiceTotal,
                    Patient = x.tb_Patient.FirstName + " " + x.tb_Patient.LastName,
                    InvoiceId = x.InvoiceId
                }).ToList();
            }
        }

        /// <summary>
        /// This method update an invoice line record and returns a string successfully message.
        /// It updates the total on the invoice table.
        /// </summary>
        public string UpdateInvoiceLine(UpdateInvoiceViewModel model)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                var i = _db.tb_Invoice_Line.FirstOrDefault(x => x.InvoiceLineId == model.InvoiceLineId);

                if (i != null)
                {
                    i.LineTotal = model.LineTotal;
                    i.Qty = model.Qty;
                    i.Description = model.Description;
                    i.Code = model.Code;
                    _db.SaveChanges();

                    var il = _db.tb_Invoice_Line.Where(x => x.InvoiceLineId == model.InvoiceLineId).ToList();
                    foreach (var item in il)
                    {
                        item.tb_Invoice.InvoiceTotal = 0;
                        item.tb_Invoice.InvoiceTotal += item.LineTotal * (decimal)item.Qty;
                    }
                    _db.SaveChanges();
                    return "Successfully Updated";

                }
                else
                {
                    return "Update Failed";
                }
            }
        }

        /// <summary>
        /// This method removes an invoice record from the database.
        /// It checks if there are invoice line records belonging to the invoice if no, it resets the invoice total to 0, if yes, it substract the invoice line total from the overall total.
        /// </summary>
        public string DeleteInvoiceLine(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {

                var il = _db.tb_Invoice_Line.FirstOrDefault(x => x.InvoiceLineId == id);
                var i = _db.tb_Invoice.FirstOrDefault(x => x.InvoiceId == il.InvoiceId);
               
                if (il != null)
                {
                    _db.tb_Invoice_Line.Remove(il);
                    _db.SaveChanges();

                    var illist = _db.tb_Invoice_Line.Where(x => x.InvoiceId == i.InvoiceId).ToList();
                    if (illist.Count == 0)
                    {
                        i.InvoiceTotal = 0;
                    }
                    else
                    {
                        var t = i.InvoiceTotal - il.LineTotal;
                        i.InvoiceTotal = t;
                    }
                    _db.SaveChanges();
                    return "Successfully Deleted";


                }
                else
                {
                    return "Delete Failed";
                }
            }
        }

        /// <summary>
        /// This method returns a list of invoice lines from the database that matches the invoice id parameter.
        /// </summary>
        public List<AllInvoiceLineViewModel> GetAllInvoiceLinesById(long id)
        {
            using (var _db = new HealthBridgeDBEntities())
            {
                return _db.tb_Invoice_Line.Where(x => x.InvoiceId == id).Select(x => new AllInvoiceLineViewModel
                {
                    Code = x.Code,
                    Description = x.Description,
                    LineTotal = x.LineTotal,
                    InvoiceTotal = x.tb_Invoice.InvoiceTotal,
                    Qty = x.Qty,
                    InvoiceId = x.tb_Invoice.InvoiceId,
                    InvoiceLineId = x.InvoiceLineId,
                    InvoiceDate = x.tb_Invoice.InvoiceDateTime,
                    Patient = x.tb_Invoice.tb_Patient.FirstName + " " + x.tb_Invoice.tb_Patient.LastName
                }).ToList();

            }
        }
    }
}