//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hospitalmanagement.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class islemler
    {
        public int ISLEMID { get; set; }
        public int USERID { get; set; }
        public Nullable<bool> ISTAHLIL { get; set; }
        public Nullable<bool> ISRONTGEN { get; set; }
        public Nullable<bool> ISULTRASON { get; set; }
        public string TAHLILIMG { get; set; }
        public string RONTGENIMG { get; set; }
        public string ULTRASONIMG { get; set; }
        public string TACIKLAMA { get; set; }
        public string RACIKLAMA { get; set; }
        public string UACIKLAMA { get; set; }
        public Nullable<System.DateTime> ISTENILATARIH { get; set; }
        public Nullable<System.DateTime> EKLENENTARIH { get; set; }
    
        public virtual user user { get; set; }
    }
}
