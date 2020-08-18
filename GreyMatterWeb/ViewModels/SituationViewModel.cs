using GreyMatterWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreyMatterWeb.ViewModels
{
    public class SituationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual User User { get; set; }
    }
}