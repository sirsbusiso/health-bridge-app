//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthbridgeApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Invoice()
        {
            this.tb_Invoice_Line = new HashSet<tb_Invoice_Line>();
        }
    
        public long InvoiceId { get; set; }
        public System.DateTime InvoiceDateTime { get; set; }
        public Nullable<long> PatientId { get; set; }
        public decimal InvoiceTotal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Invoice_Line> tb_Invoice_Line { get; set; }
        public virtual tb_Patient tb_Patient { get; set; }
    }
}
