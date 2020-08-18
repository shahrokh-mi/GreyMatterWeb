using GreyMatterWeb.Domain;
using GreyMatterWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreyMatterWeb.Controllers
{
    public class PlayerController : Controller
    {
        private GreyDataEntities db = new GreyDataEntities();

        // GET: Player
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Vote(int EntityId, string EntityGroup,bool IsUpvote)
        {
            int voteeId;
            switch (EntityGroup)
            {
                case "Outcome":
                    voteeId = db.Outcomes.SingleOrDefault(o => o.Id == EntityId).UserId;
                    break;
                case "Situation":
                    voteeId = db.Situations.SingleOrDefault(s => s.Id == EntityId).UserId;
                    break;
                case "Option":
                    voteeId = db.Options.SingleOrDefault(o => o.Id == EntityId).UserId;
                    break;
                default:
                    return RedirectToAction("Options");
            }
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var vote = db.Votes.SingleOrDefault(v => v.EntityGroup == EntityGroup && v.EntityId == EntityId);
            if (vote == null)
            {
                if (IsUpvote)
                {
                    db.Votes.Add(new Vote
                    {
                        VoterId = user.Id,
                        VoteeId = voteeId,
                        EntityId = EntityId,
                        EntityGroup = EntityGroup,
                        Value = 1
                    });
                }

                else
                {
                    db.Votes.Add(new Vote
                    {
                        VoterId = user.Id,
                        VoteeId = voteeId,
                        EntityId = EntityId,
                        EntityGroup = EntityGroup,
                        Value = -1
                    });
                }
            }

            else
            {
                if (IsUpvote)
                {
                    vote.Value = 1;
                }

                else
                {
                    vote.Value = -1;
                }

                db.Entry(vote).State = EntityState.Modified;
            }

            db.SaveChanges();

            switch (EntityGroup)
            {
                case "Outcome":
                    return RedirectToAction("Outcomes");
                case "Situation":
                    return RedirectToAction("Situations");
                case "Option":
                    return RedirectToAction("Options");
                default:
                    return RedirectToAction("Options");
            }
        }

        public ActionResult Options()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var options = db.Options.Where(o => o.UserId != user.Id);
            var model = new List<OptionViewModel>();
            foreach (var item in options)
            {
                var modelItem = new OptionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    Situation = item.Situation,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);

            }

            return View(model);
        }

        public ActionResult Outcomes()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var outcomes = db.Outcomes.Where(o => o.UserId != user.Id);
            var model = new List<OutcomeViewModel>();
            foreach (var item in outcomes)
            {
                var modelItem = new OutcomeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Situation = item.Situation,
                    Option = item.Option,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);
            }
            return View(model);
        }

        public ActionResult Situations()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var situations = db.Situations.Where(s => s.UserId != user.Id);
            var model = new List<SituationViewModel>();
            foreach (var item in situations)
            {
                var modelItem = new SituationViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Situation" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Situation" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateOutcome()
        {
            var model = new OutcomeViewModel();
            ViewBag.OptionId = new SelectList(db.Options, "Id", "Name");
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult CreateOutcome(OutcomeViewModel model)
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            db.Outcomes.Add(new Outcome
            {
                OptionId = model.OptionId,
                Name = model.Name,
                Description = model.Description,
                UserId = user.Id,
                SituationId = model.SituationId,

            });

            db.SaveChanges();

            return RedirectToAction("MyOutcomes");
        }

        public ActionResult MyOutcomes()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var outcomes = db.Outcomes.Where(o => o.UserId == user.Id);
            var model = new List<OutcomeViewModel>();
            foreach (var item in outcomes)
            {
                var modelItem = new OutcomeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Situation = item.Situation,
                    Option = item.Option,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateSituation()
        {
            var model = new SituationViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSituation(SituationViewModel model)
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            db.Situations.Add(new Situation
            {
                Name = model.Name,
                Description = model.Description,
                UserId = model.UserId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            });
            db.SaveChanges();
            return RedirectToAction("MySituations");
        }

        public ActionResult MySituations()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var situations = db.Situations.Where(s => s.UserId == user.Id);
            var model = new List<SituationViewModel>();
            foreach (var item in situations)
            {
                var modelItem = new SituationViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Situation" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Situation" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateOption()
        {
            var model = new OptionViewModel();
            ViewBag.SituationId = new SelectList(db.Situations, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOption(OptionViewModel model)
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            db.Options.Add(new Option
            {
                Name = model.Name,
                Description = model.Description,
                UserId = model.UserId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                SituationId = model.SituationId
            });
            db.SaveChanges();
            return RedirectToAction("MyOptions");
        }

        public ActionResult MyOptions()
        {
            var username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var options = db.Options.Where(o => o.UserId == user.Id);
            var model = new List<OptionViewModel>();
            foreach (var item in options)
            {
                var modelItem = new OptionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    Situation = item.Situation,
                    User = item.User
                };
                int score = db.Votes.Where(v => v.Value > 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count() - db.Votes.Where(v => v.Value < 0 && v.EntityGroup == "Option" && v.EntityId == item.Id).Count();
                modelItem.Score = score;
                model.Add(modelItem);

            }

            return View(model);
        }
    }
}