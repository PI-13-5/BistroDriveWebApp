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
    
    public partial class dishreview
    {
        public int Id_Review { get; set; }
        public string Id_Owner { get; set; }
        public int Id_Dish { get; set; }
        public string Description { get; set; }
        public Nullable<int> Mark { get; set; }
    
        public virtual aspnetuser aspnetuser { get; set; }
        public virtual dish dish { get; set; }
    }
}
