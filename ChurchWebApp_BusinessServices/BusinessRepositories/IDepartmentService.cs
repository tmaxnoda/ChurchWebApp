using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
   public interface IDepartmentService
    {
        IQueryable<tbl_department> getDepartment();
        void CreateDepartment(tbl_department OrganizationEntity);

        bool UpdateDepartment(tbl_department OrganizationEntity);

        bool DeleteDepartment(int id);

        List<tbl_department> Departments();

        tbl_department findDepartment(int? id);
    }
}
