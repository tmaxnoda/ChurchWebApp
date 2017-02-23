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
    [RoutePrefix("department")]
    public class DepartmentController : AlertController
    {
        IDepartmentService _departmentservice;

        //private ChurchAnalytics _analytics = new ChurchAnalytics();
        public DepartmentController(IDepartmentService departmentservice)
        {
            _departmentservice = departmentservice;

        }
        // GET: Admin/Department
        [HttpGet]
        [Route("createdepartment")]
        public ActionResult createdepartment()
        {
            return View("CreateDepartment");
        }

        [HttpPost]
        [Route("createdepartment")]
        public ActionResult Savedepartment(DepartmentVM model)
        {
            if (model.id == 0)
            {
                tbl_department deparment = new tbl_department();
                deparment.department_name = model.department_name.ToUpper();
                _departmentservice.CreateDepartment(deparment);
            }
            else
            {
                var findDep = _departmentservice.findDepartment(model.id);
                if (findDep == null)
                {
                    Warning("Department cannot be found",true);
                    return View("EditDepartment");
                }
                findDep.department_name = model.department_name;
                var success =_departmentservice.UpdateDepartment(findDep);
                if (success == true)
                {
                    Success("Department updated successfully!", true);
                    return RedirectToAction("departments");
                }
            }
           
            Success("Department created successfully");

            return RedirectToAction("createdepartment");
        }

        [HttpGet]
        public ActionResult departments()
        {

            IList<tbl_department> departnment = _departmentservice.Departments();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_department, DepartmentVM>();
            });

            IMapper mapper = config.CreateMapper();
            var departments = mapper.Map<IList<tbl_department>, IList<DepartmentVM>>(departnment);
            return View("Departments",departments.ToList());
        }
        [HttpGet]
        [Route("editdepartment")]
        public ActionResult editdepartment(int? id)
        {
            if (id == null)
            {
                Warning("Id is not valid", true);
                return RedirectToAction("departments");
            }
           
            var department = _departmentservice.findDepartment(id);
            if (department == null)
            {
               Warning("The selected separtment cannot be found", true);
                return RedirectToAction("departments");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_department, DepartmentVM>();


            });

            IMapper mapper = config.CreateMapper();
            var departments = mapper.Map<tbl_department, DepartmentVM>(department);
            //ViewBag.country_id = new SelectList(_cotext.tbl_country, "id", "country_name", org.country_id);
            return View("EditDepartment",departments);
        }

        public ActionResult removeDepartment(int id)
        {

            var isDepartmentDelete = _departmentservice.DeleteDepartment(id);
            if (isDepartmentDelete == false)
            {
                Information("Department does not exist", true);
                return RedirectToAction("departments");
            }
            else
            {
                Success("Department Deleted Successfully", true);

            }
            return RedirectToAction("departments", "Department", new { area = "Admin" });
        }

    }
}