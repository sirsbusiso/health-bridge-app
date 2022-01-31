using HealthbridgeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthbridgeApp.Interface
{
    public interface IInvoice
    {
        List<InvoiceViewModel> GetInvoices();
        string CreateInvoice(InvoiceViewModel model);
        string CreateInvoiceLine(CreateInvoiceLineViewModel model);
        string UpdateInvoiceLine(UpdateInvoiceViewModel model);
        UpdateInvoiceViewModel GetInvoiceById(long id);
        List<AllInvoiceLineViewModel> GetAllInvoiceLinesById(long id);
        string DeleteInvoice(long id);
        string DeleteInvoiceLine(long id);
        bool DeleteAllInvoiceLines(long id);
        List<AllInvoiceLineViewModel> GetInvoiceLines();
    }
}