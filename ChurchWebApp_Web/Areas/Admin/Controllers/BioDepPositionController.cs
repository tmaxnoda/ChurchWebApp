using AutoMapper;
using ChurchWebApp_BusinessServices.BusinessRepositories;
using ChurchWebApp_DAL;
using ChurchWebApp_Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChurchWebApp_Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("biodepposition")]
    public class BioDepPositionController : AlertController
    {
        IBio_Dep_Pos_Services _bio_dep_pos_services;
        IBiodataRepositoryService _biodataservice;
        IDepartmentService _departmentservice;
        IPositionService _positionservice;

        //private ChurchAnalytics _analytics = new ChurchAnalytics();
        public BioDepPositionController(IBio_Dep_Pos_Services bio_dep_pos_services
            , IBiodataRepositoryService biodataservice
            , IDepartmentService departmentservice
            , IPositionService positionservice)
        {
            _bio_dep_pos_services = bio_dep_pos_services;
            _biodataservice = biodataservice;
            _departmentservice = departmentservice;
            _positionservice = positionservice;


        }
        // GET: Admin/BioDepPosition
        public ActionResult createBioDepPosition()
        {
            return View("CreateBiodDepPosition");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createbiodepposition")]
        public ActionResult saveBioDepPosition(BioDepPositionVM model)
        {
            //if (model == null)
            //{
            //    Information("Form cannot be empty", true);
            //    return RedirectToAction("createBioDepPossition");

            //}
            if (model.id == 0)
            {
                tbl_department_member_position tbl_member_position = new tbl_department_member_position();
                tbl_member_position.tbl_department_id = model.tbl_department_id;
                tbl_member_position.tbl_member_biodata_id = model.tbl_member_biodata_id;
                tbl_member_position.tbl_position_id = model.tbl_position_id;
                _bio_dep_pos_services.Createbio_dep_pos(tbl_member_position);
            }
            else
            {
                var find_id = _bio_dep_pos_services.findbio_dep_pos(model.id);
                if (find_id==null)
                {
                    Warning("Invalid Selection", true);
                    return View("EditDepartment");
                }

                find_id.tbl_department_id = model.tbl_department_id;
                find_id.tbl_member_biodata_id = model.tbl_member_biodata_id;
                find_id.tbl_position_id = model.tbl_position_id;
                var success = _bio_dep_pos_services.Updatebio_dep_pos(find_id);
                if (success == true)
                {
                    Success("Member Department With Position Update successfully!", true);
                    return RedirectToAction("BioDepPositions");
                }
            }

           

            Success("Members Department with Position created successfully", true);

            return View("BiogDepPositions");
        }

        [HttpGet]
        [Route("editbiodepposition/id")]
        public ActionResult editBioDepPosition(int? id)
        {
            if (id == null)
            {
                Information("Invalid Selection", true);
                return RedirectToAction("BiogDepPositions");
            }

            tbl_department_member_position depPos = new tbl_department_member_position();
            depPos = _bio_dep_pos_services.findbio_dep_pos(id);
            if (depPos == null)
            {
                Danger("Department with position cannot be found", true);
                return RedirectToAction("BiogDepPositions");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_department_member_position, BioDepPositionVM>();


            });

            IMapper mapper = config.CreateMapper();
            var BioDepPosition = mapper.Map<tbl_department_member_position, BioDepPositionVM>(depPos);
            //ViewBag.country_id = new SelectList(_cotext.tbl_country, "id", "country_name", org.country_id);
            return View("Editbiodepposition", BioDepPosition);
        }

        public ActionResult BioDepPositions(BioDepPositionVM model)
        {
            IList<tbl_department_member_position> biodata = _bio_dep_pos_services.bio_dep_pos();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_department_member_position, BioDepPositionVM>();
            });

            IMapper mapper = config.CreateMapper();
            var bio_dep_pos = mapper.Map<IList<tbl_department_member_position>, IList<BioDepPositionVM>>(biodata);
            return View("BiogDepPositions", bio_dep_pos.ToList());
        }


        public ActionResult removDepPos(int id)
        {
            var dep_position = _bio_dep_pos_services.Deletebio_dep_pos(id);
            if (dep_position ==true)
            {
                Success("Department with Position Deleted successfully",true);
            }else
            {
                Information("Such does not exit", true);
                return RedirectToAction("BioDepPositions");
            }

            return RedirectToAction("BioDepPositions", "BioDepPosition", new { area = "Admin" });
        }
        public JsonResult searchForMembers(string q)
        {
            var searchData = _biodataservice.getBiodatas().Where(x => x.first_name.StartsWith(q.ToLower()))

            .Select(f => new
            {
                Name = f.last_name + " " + f.first_name + " " + f.middle_name,
                Id = f.id
            }).ToList();

            return Json(searchData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchForPositions(string q)
        {
            var searchData = _positionservice.getPositions().Where(x => x.position_name.StartsWith(q.ToLower()))

            .Select(f => new
            {
                Name = f.position_name,
                Id = f.id
            }).ToList();

            return Json(searchData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchForDepartments(string q)
        {
            var searchData = _departmentservice.getDepartment().Where(x => x.department_name.StartsWith(q.ToLower()))

            .Select(f => new
            {
                Name = f.department_name,
                Id = f.id
            }).ToList();

            return Json(searchData, JsonRequestBehavior.AllowGet);
        }
    }
}