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
    
    public partial class UserFullView
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string NationalCode { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] UserImage { get; set; }
        public int UsersJobID { get; set; }
        public string CreateDate { get; set; }
        public int JobID { get; set; }
        public string TActivity { get; set; }
        public bool Activity { get; set; }
        public string TGender { get; set; }
        public string JobName { get; set; }
        public Nullable<int> PhoneID { get; set; }
        public string PhoneNumber { get; set; }
    }
}