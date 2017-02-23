using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
   public interface IBiodataRepositoryService
    {
        IQueryable<tbl_member_biodata> getBiodatas();
        bool ValidateExtension(string extension);
        void CreateMemberBiodata(tbl_member_biodata OrganizationEntity);

        bool UpdateMemberBiodata(tbl_member_biodata OrganizationEntity);

        bool DeleteMemberBiodata(int id);

        List<tbl_member_biodata> Members();

        tbl_member_biodata findMember(int? id);
    }
}
