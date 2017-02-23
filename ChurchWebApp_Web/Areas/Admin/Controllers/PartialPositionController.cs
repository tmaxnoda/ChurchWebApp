using AutoMapper;
using ChurchWebApp_BusinessServices.BusinessRepositories;
using ChurchWebApp_DAL;
using ChurchWebApp_Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChurchWebApp_Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("partialposition")]
    public class PartialPositionController : Controller
    {
        IPositionService _positionservice;

        //private ChurchAnalytics _analytics = new ChurchAnalytics();
        public PartialPositionController(IPositionService positionservice)
        {
            _positionservice = positionservice;

        }
        // GET: Admin/Position
        [HttpGet]
        [Route("createpartialposition")]
        public ActionResult createpartialposition()
        {


            return PartialView("_createpartialposition");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createpartialposition")]
        public ActionResult createpartialposition(PositionVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            try
            {
                tbl_position position = new tbl_position();
                position.position_name = model.position_name.ToUpper();

                _positionservice.CreatePosition(position);

                string url = Url.Action("createBioDepPosition", "BioDepPosition");
                return Json(new { success = true });





            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors: ", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(" Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;


            }
            return PartialView("createpartialposition", model);

        }

        [ChildActionOnly]
        public ActionResult partialpositions()
        {

            IList<tbl_position> position = _positionservice.Positions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_position, PositionVM>();
            });

            IMapper mapper = config.CreateMapper();
            var positions = mapper.Map<IList<tbl_position>, IList<PositionVM>>(position);
            return PartialView("_partialpositions", positions.ToList());
        }

        [HttpGet]
        [Route("editpartialposition/id")]
        public ActionResult editpartialposition(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbl_position pos = new tbl_position();
            pos = _positionservice.findPosition(id);
            if (pos == null)
            {
                return HttpNotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_position, PositionVM>();


            });

            IMapper mapper = config.CreateMapper();
            var Organization = mapper.Map<tbl_position, PositionVM>(pos);
            //ViewBag.country_id = new SelectList(_cotext.tbl_country, "id", "country_name", org.country_id);
            return PartialView("_Edit", Organization);
        }

        [HttpPost]
        [Route("editpartialposition/id")]
        public ActionResult editpartialposition(PositionVM model)
        {
            var position = _positionservice.findPosition(model.id);
            if (position == null)
            {
                return HttpNotFound();
            }
            position.position_name = model.position_name;
            var editedPosition = _positionservice.UpdatePosition(position);
            //if (editedPosition == true)
            //{
            //    Success("Position updated successfully");
            //    ModelState.Clear();
            //  return  RedirectToAction("createposition", "Position");
            //}

            return PartialView("createpositionForm");
            //string url = Url.Action("positions", "Position", new { id = model.id });
            //return Json(new { success = true, url = url });
        }
    }
}