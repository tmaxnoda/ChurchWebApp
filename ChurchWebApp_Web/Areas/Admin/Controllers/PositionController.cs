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
    [RoutePrefix("position")]
    public class PositionController : AlertController
    {
        IPositionService _positionservice;

        //private ChurchAnalytics _analytics = new ChurchAnalytics();
        public PositionController(IPositionService positionservice)
        {
            _positionservice = positionservice;

        }
        // GET: Admin/Position
        [HttpGet]
        [Route("createposition")]
        public ActionResult createposition()
        {
            return View("CreatePosition");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createposition")]
        public ActionResult savePosition(PositionVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            try
            {
                if (model.id == 0)
                {
                    tbl_position position = new tbl_position();
                    position.position_name = model.position_name.ToUpper();

                    _positionservice.CreatePosition(position);
                }
                else
                {
                    var findposition = _positionservice.findPosition(model.id);
                    if (findposition == null)
                    {
                        Information("Position does not exist",true);
                        return RedirectToAction("createposition", true);
                    }

                    findposition.position_name = model.position_name;
                    _positionservice.UpdatePosition(findposition);
                    Success("Position updated successfully",true);
                    return RedirectToAction("createposition");
                }
               
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
            Success("Position created successfully", false);
            return RedirectToAction("createposition");

        }

 
        public ActionResult positions()
        {

            IList<tbl_position> position = _positionservice.Positions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_position, PositionVM>();
            });

            IMapper mapper = config.CreateMapper();
            var positions = mapper.Map<IList<tbl_position>, IList<PositionVM>>(position);
            return View("Positions", positions.ToList());
        }

        [HttpGet]
        [Route("editposition/id")]
        public ActionResult editposition(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbl_position pos = new tbl_position();
            pos = _positionservice.findPosition(id);
            if (pos == null)
            {
                Information("Position cannont be found", true);
                return View("EditPosition");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_position, PositionVM>();


            });

            IMapper mapper = config.CreateMapper();
            var Positn= mapper.Map<tbl_position, PositionVM>(pos);
            //ViewBag.country_id = new SelectList(_cotext.tbl_country, "id", "country_name", org.country_id);
            return View("EditPosition", Positn);
        }

        
        public ActionResult removePosition(int id)
        {

            var isPositionDelete = _positionservice.DeletePosition(id);
            if(isPositionDelete == false)
            {
                Information("Position does not exist", true);
                return RedirectToAction("positions");
            }
            else
            {
                Success("Position Deleted Successfully", true);

            }
            return RedirectToAction("positions", "Position", new { area = "Admin" });
        }
    }
}