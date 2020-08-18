﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Sandogh_DBEntities : DbContext
    {
        public Sandogh_DBEntities()
            : base("name=Sandogh_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Jobs> Tbl_Jobs { get; set; }
        public virtual DbSet<Tbl_Users> Tbl_Users { get; set; }
        public virtual DbSet<Vw_UsersJob> Vw_UsersJob { get; set; }
    
        public virtual ObjectResult<Sp_Login_Result> Sp_Login(string username, string pass)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("Pass", pass) :
                new ObjectParameter("Pass", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_Login_Result>("Sp_Login", usernameParameter, passParameter);
        }
    }
}
