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
    
    public partial class Vote
    {
        public int Id { get; set; }
        public int VoterId { get; set; }
        public int EntityId { get; set; }
        public string EntityGroup { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public int VoteeId { get; set; }
        public int Value { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
