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
    
    public partial class usertoken
    {
        public int Id_Token { get; set; }
        public string Id_User { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> Expiration { get; set; }
    
        public virtual aspnetuser aspnetuser { get; set; }
    }
}