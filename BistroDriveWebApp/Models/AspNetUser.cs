//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BistroDriveWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class aspnetuser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aspnetuser()
        {
            this.aspnetuserclaims = new HashSet<aspnetuserclaim>();
            this.aspnetuserlogins = new HashSet<aspnetuserlogin>();
            this.userdescriptions = new HashSet<userdescription>();
            this.dishes = new HashSet<dish>();
            this.orders = new HashSet<order>();
            this.usertokens = new HashSet<usertoken>();
            this.aspnetroles = new HashSet<aspnetrole>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public sbyte EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public sbyte PhoneNumberConfirmed { get; set; }
        public sbyte TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public sbyte LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FristName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Avatar_Url { get; set; }
        public Nullable<System.DateTime> LastOnlineTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aspnetuserclaim> aspnetuserclaims { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aspnetuserlogin> aspnetuserlogins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userdescription> userdescriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dish> dishes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usertoken> usertokens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aspnetrole> aspnetroles { get; set; }
    }
}
