//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sandogh.DataLayer.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phone
    {
        public int PhoneID { get; set; }
        public int PersonID { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
