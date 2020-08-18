using GreyMatterWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreyMatterWeb.ViewModels
{
    public class OutcomeViewModel
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int SituationId { get; set; }
        public int Score { get; set; }

        public virtual Option Option { get; set; }
        public virtual Situation Situation { get; set; }
        public virtual User User { get; set; }
    }
}