using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthbridgeApp.BusinessLogic;
using HealthbridgeApp.Interface;
using HealthbridgeApp.ViewModel;

namespace HealthbridgeApp.WebApi
{
    public class InvoiceAPIController : ApiController
    {

        public IInvoice _invoiceBusiness;

        public InvoiceAPIController(IInvoice invoiceBusiness)
        {
            _invoiceBusiness = invoiceBusiness;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/InvoiceLine/Get")]
        public IEnumerable<AllInvoiceLineViewModel> GetDetails()
        {
            var list = _invoiceBusiness.GetInvoiceLines();

            return list.AsEnumerable();
        }

        [HttpGet]
        [Route("api/Invoice/Get")]
        public IEnumerable<InvoiceViewModel> GetInvoice()
        {
            var list = _invoiceBusiness.GetInvoices();

            return list.AsEnumerable();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Invoice/Get/{id}")]
        public UpdateInvoiceViewModel Get(long id)
        {
            var result = _invoiceBusiness.GetInvoiceById(id);

            return result;
        }
        [HttpGet]
        [Route("api/InvoiceLine/{id}")]
        public IEnumerable<AllInvoiceLineViewModel> GetInvoiceLineById(long id)
        {
            var result = _invoiceBusiness.GetAllInvoiceLinesById(id);

            return result.AsEnumerable();
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Invoice")]
        public string PostInvoice(InvoiceViewModel model)
        {
            var result = _invoiceBusiness.CreateInvoice(model);

            return result;
        }

        // PUT api/<controller>
        [HttpPut]
        [Route("api/InvoiceLine")]
        public string Put(UpdateInvoiceViewModel model)
        {
            var result = _invoiceBusiness.UpdateInvoiceLine(model);

            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/InvoiceLine/{id}")]
        public string DeleteInvoiceLine(int id)
        {
            var result = _invoiceBusiness.DeleteInvoiceLine(id);

            return result;
        }

        [HttpDelete]
        [Route("api/Invoice/{id}")]
        public string DeleteInvoice(int id)
        {
            var result = _invoiceBusiness.DeleteInvoice(id);

            return result;
        }
    }
}