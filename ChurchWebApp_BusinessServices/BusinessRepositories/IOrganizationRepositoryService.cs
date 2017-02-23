using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public interface IOrganizationRepositoryService
    {
        tbl_organization GetById(int id);

        void CreateOrganization(tbl_organization OrganizationEntity);

        bool UpdateOrganization(tbl_organization OrganizationEntity);

        bool DeleteOrganization(int OrganizationId);

        List<tbl_organization> Organizations();

        tbl_organization findOrganization(int? id);
    }
}
