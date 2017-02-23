using AutoMapper;
using ChurchWebApp_BusinessServices.BusinessRepositories;
using ChurchWebApp_DAL;
using ChurchWebApp_Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchWebApp_Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("memberbiodata")]
    public class MemberBiodataController : AlertController
    {

        IBiodataRepositoryService _biodataservice;
        public MemberBiodataController(IBiodataRepositoryService biodataservice)
        {
            _biodataservice = biodataservice;

        }
        // GET: Admin/Member_Biodata
        [HttpGet]
        [Route("creatememberbiodata")]
        public ActionResult creatememberbiodata()
        {
            return View("CreateBiodata");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("savebiodata")]
        public ActionResult saveBiodata(HttpPostedFileBase pics, Member_BiodataVM data)
        {
            
            var image = pics;
            byte[] imageData = null;
            try
            {
                if (image == null)
                {
                    FileInfo _file = new FileInfo(Server.MapPath("~") + "\\customview\\html\\images\\gallery\\image2.jpg");
                    FileStream _fileSream = new FileStream(_file.FullName, FileMode.Open);
                    imageData = new byte[_fileSream.Length];
                    data.photo = imageData;



                }
                else
                {
                    //Declare byte array for holding images to bytlike file


                    //geth extention
                    var filename = Path.GetExtension(image.FileName);

                    //validate extension
                    if (!_biodataservice.ValidateExtension(filename))
                    {
                        Information("upload image with Extention .jpg|.png|.jpeg");
                        return RedirectToAction("creatememberbiodata", "MemberBiodata", new { area = "Admin" });
                    }

                    // read image to binary files
                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(image.ContentLength);
                        var headerImage = new HeaderImage()
                        {
                            ImageData = imageData,
                            ImageName = image.FileName,
                            IsActive = true

                        };
                        //var data = Convert.ToBase64String(imageData);
                        data.photo = imageData;
                    }
                }//end of else

                if (data.id == 0)
                {
                   

                    tbl_member_biodata memberBiodata = new tbl_member_biodata();
                    memberBiodata.first_name = data.first_name.ToUpper();
                    memberBiodata.last_name = data.last_name.ToUpper();
                    memberBiodata.middle_name = data.middle_name.ToUpper();
                    memberBiodata.dob = data.dob;
                    memberBiodata.phone_number = data.phone_number;
                    memberBiodata.house_address = data.house_address.ToUpper();
                    memberBiodata.marital_status = data.marital_status.ToUpper();
                    memberBiodata.house_fellowship_id = data.house_fellowship_id;
                    memberBiodata.date_of_initial_attendant = data.date_of_initial_attendant;
                    memberBiodata.gender = data.gender.ToUpper();
                    memberBiodata.photo = data.photo;
                    _biodataservice.CreateMemberBiodata(memberBiodata);

                }
                else
                {
                    var tblbiodata = _biodataservice.findMember(data.id);
                    if (tblbiodata == null)
                    {
                        Danger("Member biodata cannot be found", true);
                        return RedirectToAction("membersbiodatas");
                    }

                    tblbiodata.first_name = data.first_name.ToUpper();
                    tblbiodata.last_name = data.last_name.ToUpper();
                    tblbiodata.middle_name = data.middle_name.ToUpper();
                    tblbiodata.dob = data.dob;
                    tblbiodata.phone_number = data.phone_number;
                    tblbiodata.house_address = data.house_address.ToUpper();
                    tblbiodata.marital_status = data.marital_status.ToUpper();
                    tblbiodata.house_fellowship_id = data.house_fellowship_id;
                    tblbiodata.date_of_initial_attendant = data.date_of_initial_attendant;
                    tblbiodata.gender = data.gender.ToUpper();
                    tblbiodata.photo = data.photo;
                    _biodataservice.UpdateMemberBiodata(tblbiodata);
                    Success("Member Biodata updateed successfully", true);
                    return RedirectToAction("membersbiodatas", "MemberBiodata", new { area = "Admin" });
                }




            }

            catch (DbEntityValidationException e)
            {
                Exception raise = e;

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var valerror in eve.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", eve.Entry.Entity.ToString(),
                            valerror.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);

                    }

                }

                throw raise;

            }
            Success("Member Biodata created successfully", true);
            return RedirectToAction("creatememberbiodata", "MemberBiodata",new {area="Admin"});

        }

        


        [Route("membersbiodatas")]
        public ActionResult membersbiodatas()
        {

            IList<tbl_member_biodata> members = _biodataservice.Members();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_member_biodata, Member_BiodataVM>();
            });

            IMapper mapper = config.CreateMapper();
            var memberBio = mapper.Map<IList<tbl_member_biodata>, IList<Member_BiodataVM>>(members);
            return View("Biodatas", memberBio.ToList());
        }


        [HttpGet]
        [Route("editmemberbiodata/id")]
        public ActionResult editmemberbiodata(int? id)
        {
            if (id == null)
            {
                Information("Invalid member", true);
                return View("EditBiodata");
            }

            var findMember = _biodataservice.findMember(id);
            if (findMember == null)
            {
                Danger("Member cannot be  found", true);
                return View("EditBiodata");
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_member_biodata, Member_BiodataVM>();


            });

            IMapper mapper = config.CreateMapper();
            var memberBio = mapper.Map<tbl_member_biodata, Member_BiodataVM>(findMember);
            return View("EditBiodata",memberBio);
        }

        //[HttpPost]
        //[Route("editmemberbiodata/id")]
        //public ActionResult editmemberbiodata(HttpPostedFileBase pics, Member_BiodataVM member)
        //{
        //    var findMember = _biodataservice.findMember(member.id);
        //    if (findMember == null)
        //    {
        //        Information("Invalid member", true);
        //        return View("editmemberbiodata");
        //    }
        //    var image = pics;
        //    try
        //    {
        //        if (image == null)
        //        {
        //            Information("Please provide image", true);
        //            RedirectToAction("creatememberbiodata");
        //        }
        //        else
        //        {
        //            //Declare byte array for holding images to bytlike file
        //            byte[] imageData = null;

        //            //geth extention
        //            var filename = Path.GetExtension(image.FileName);

        //            //validate extension
        //            if (!_biodataservice.ValidateExtension(filename))
        //            {
        //                Information("upload image with Extention .jpg|.png|.jpeg");
        //                return RedirectToAction("Index");
        //            }

        //            // read image to binary files
        //            using (var binaryReader = new BinaryReader(image.InputStream))
        //            {
        //                imageData = binaryReader.ReadBytes(image.ContentLength);
        //                var headerImage = new HeaderImage()
        //                {
        //                    ImageData = imageData,
        //                    ImageName = image.FileName,
        //                    IsActive = true

        //                };
        //                //var data = Convert.ToBase64String(imageData);
        //                member.photo = imageData;
        //            }
        //        }//end of el


        //        findMember.first_name = member.first_name.ToUpper();
        //        findMember.last_name = member.last_name.ToUpper();
        //        findMember.middle_name = member.middle_name.ToUpper();
        //        findMember.dob = member.dob;
        //        findMember.phone_number = member.phone_number;
        //        findMember.house_address = member.house_address.ToUpper();
        //        findMember.marital_status = member.marital_status.ToUpper();
        //        findMember.house_fellowship_id = member.house_fellowship_id;
        //        findMember.date_of_initial_attendant = member.date_of_initial_attendant;
        //        findMember.gender = member.gender.ToUpper();
        //        findMember.photo = member.photo;
        //        var success = _biodataservice.UpdateMemberBiodata(findMember);
        //        if (success == true)
        //        {
        //            Success("Biodata updated successfully", true);
        //        }
        //        else
        //        {
        //            Danger("Invalid biodata update ", true);
        //        }

        //        return RedirectToAction("membersbiodatas");
        //    }
        //    catch (DbEntityValidationException e)
        //    {

        //        Exception raise = e;

        //        var outputLines = new List<string>();
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            foreach (var valerror in eve.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}", eve.Entry.Entity.ToString(),
        //                    valerror.ErrorMessage);
        //                raise = new InvalidOperationException(message, raise);

        //            }

        //        }

        //        throw raise;
        //    }
        //}


        public ActionResult removeBiodata(int id)
        {
            if(id == null)
            {
                Information("Invalid Member Selection", true);
                return View("Membersbiodatas");
            }

            var findId = _biodataservice.findMember(id);
            if(findId == null)
            {
                Information("Member selected cannot be found", true);
                return View("Membersbiodatas");
            }
            var isBiodataDelete = _biodataservice.DeleteMemberBiodata(findId.id);
            if (isBiodataDelete == false)
            {
                Danger("Member biodata does not exist", true);
                return RedirectToAction("membersbiodatas");
            }
            else
            {
                Success("Member biodata Successfully", true);

            }
            return RedirectToAction("membersbiodatas", "MemberBiodata", new { area = "Admin" });
        }
    }
}