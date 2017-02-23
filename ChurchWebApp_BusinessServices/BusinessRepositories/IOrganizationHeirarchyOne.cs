using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public interface IOrganizationHeirarchyOne
    {
        IEnumerable<tbl_member_biodata> getLeaders();
    }
}
