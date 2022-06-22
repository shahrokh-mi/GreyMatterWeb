//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreyMatterWeb.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Outcome
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> PictureId { get; set; }
        public int UserId { get; set; }
        public int SituationId { get; set; }
    
        public virtual Option Option { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual Situation Situation { get; set; }
        public virtual User User { get; set; }
    }
}