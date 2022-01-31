using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthbridgeApp.ViewModel
{
    public class InvoiceViewModel
    {
        public long InvoiceId { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public long PatientId { get; set; }
        public decimal InvoiceTotal {get;set;}

        public string Patient { get; set; }

        public List<CreateInvoiceLineViewModel> CreateInvoiceLineViewModels { get; set; }
    }


    public class UpdateInvoiceViewModel
    {
        public long InvoiceId { get; set; }
        public double Qty { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal LineTotal { get; set; }
        public long InvoiceLineId { get; set; }
    }

    public class AllInvoiceLineViewModel
    {
        public long InvoiceLineId { get; set; }
        public double Qty { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long InvoiceId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public string Patient { get; set; }
    }
    public class CreateInvoiceLineViewModel
    {
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public double Qty { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal LineTotal { get; set; }
        public string Patient { get; set; }
    }
}