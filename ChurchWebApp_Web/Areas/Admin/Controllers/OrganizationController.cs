using AutoMapper;
using ChurchWebApp_BusinessServices.BusinessRepositories;
using ChurchWebApp_DAL;
using ChurchWebApp_Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChurchWebApp_Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("organization")]
    //[Route("organization/{action}/{id?}")]
    //[RouteArea("Admin")]
    //[Route("organization/{action}/{id?}")]

    public class OrganizationController : AlertController
    {
        IOrganizationRepositoryService _organizationservice;
        ICountryRepositoryService _countryservice;

        //private ChurchAnalytics _analytics = new ChurchAnalytics();
        public OrganizationController(IOrganizationRepositoryService orgservice,
            ICountryRepositoryService countryservice)
        {
            _organizationservice = orgservice;
            _countryservice = countryservice;
        }

        /**
         * Jason method for fetching all available countries*/
        public JsonResult searchForCountry(string q)
        {
            var searchData = _countryservice.getcountries().Where(x => x.country_name.StartsWith(q.ToLower()))

            .Select(f => new
            {
                countries = f.country_name,
                Id = f.id
            }).ToList();

            return Json(searchData, JsonRequestBehavior.AllowGet);
        }

        /**
         * Index method for create organization view*/
        // GET: Admin/Organization
        [HttpGet]
        [Route("createneworganization")]
        public ActionResult createNewOrganization()
        {
            return View("OrganizationForm");
        }



        /**
         * Organizations method for displaying list of organizations*/
        [Route("organizations")]
        public ActionResult organizations()
        {

            IList<tbl_organization> orgs = _organizationservice.Organizations();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_organization, OrganizationRegistrationVM>();
            });

            IMapper mapper = config.CreateMapper();
            var Organization = mapper.Map<IList<tbl_organization>, IList<OrganizationRegistrationVM>>(orgs);
            //var organization =
            //    _organizationservice.Organizations().Select(Mapper.Map<tbl_organization, OrganizationRegistrationVM>);
            return View("organizations", Organization.ToList());
        }



        /**
        * Method for posting organization created*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("saveorganization")]
        public ActionResult saveOrganization(OrganizationRegistrationVM org)
        {
            //if (!ModelState.IsValid)
            //{
            //    Information("Model state is Invalid", true);
            //    return View(org);
            //}
            try
            {
                if (org.id == 0)
                {
                    tbl_organization organization = new tbl_organization();
                    organization.organization_name = org.organization_name.ToUpper();
                    organization.organization_address = org.organization_address.ToUpper();
                    organization.orgdiscription = org.orgdiscription.ToUpper();
                    organization.country_id = org.country_id;
                    organization.organization_state = org.organization_state.ToUpper();
                    organization.organization_city = org.organization_city.ToUpper();
                    _organizationservice.CreateOrganization(organization);
                }
                else
                {

                    var tblorg = _organizationservice.GetById(org.id);
                    if (tblorg == null)
                    {
                        Danger("Organization Cannot be found", true);
                        return RedirectToAction("organizations");
                    }

                    tblorg.country_id = org.country_id;
                    tblorg.organization_name = org.organization_name;
                    tblorg.organization_state = org.organization_state;
                    tblorg.organization_city = org.organization_city;
                    tblorg.organization_address = org.organization_address;
                    tblorg.orgdiscription = org.orgdiscription;
                    _organizationservice.UpdateOrganization(tblorg);

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
            Success("Organization created successfully", true);
            return RedirectToAction("organizations");

        }
        /**
        * Method for editing organization view*/
        [HttpGet]
        [Route("editorganization/id")]
        public ActionResult editorganization(int? id)
        {
            if (id == null)
            {
                Information("Invalid organization", true);
                return RedirectToAction("editorganization");
            }

            tbl_organization org = new tbl_organization();
            org = _organizationservice.findOrganization(id);
            if (org == null)
            {
                Danger("Organization cannot be found", true);
                return RedirectToAction("organizations");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_organization, OrganizationRegistrationVM>();


            });

            IMapper mapper = config.CreateMapper();
            var Organization = mapper.Map<tbl_organization, OrganizationRegistrationVM>(org);
            //ViewBag.country_id = new SelectList(_cotext.tbl_country, "id", "country_name", org.country_id);
            return View("editorganization", Organization);
        }

        public ActionResult removeOrganization(int id)
        {
            var isOrgsDelete = _organizationservice.DeleteOrganization(id);
            if (isOrgsDelete == false)
            {
                Danger("Organization does not exist",true);
                return RedirectToAction("organizations");
            }
            else
            {
                Success("Organization Deleted Successfully",true);
               
            }
            return RedirectToAction("organizations","Organization",new {area="Admin"});
        }

        /**
        * Method for editing posting edited organization*/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("editorganization/id")]
        //public ActionResult editorganization(OrganizationRegistrationVM org)
        //{
        //    var success = true;
        //    tbl_organization tblorg =_organizationservice.GetById(org.id);

        //    tblorg.country_id = org.CountryId;
        //    tblorg.organization_name = org.organization_name;
        //    tblorg.organization_state = org.organization_state;
        //    tblorg.organization_city = org.organization_city;
        //    tblorg.organization_address = org.organization_address;
        //    tblorg.orgdiscription = org.organization_description;
        //    var organizations = _organizationservice.UpdateOrganization(tblorg);

        //    if (organizations == success)
        //    {
        //        Success("Organization Updated successfully", true);
        //    }

        //    else
        //    {
        //        Danger("Invalid organization ", true);
        //    }            
        //    return RedirectToAction("organizations");
        //}



    }
}