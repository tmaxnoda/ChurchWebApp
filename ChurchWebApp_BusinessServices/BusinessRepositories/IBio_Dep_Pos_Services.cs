using ChurchWebApp_DAL;
using ChurchWebApp_DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public interface IBio_Dep_Pos_Services
    {
        List<tbl_department_member_position> bio_dep_pos();
        void Createbio_dep_pos(tbl_department_member_position position);
        bool Updatebio_dep_pos(tbl_department_member_position position);
        tbl_department_member_position findbio_dep_pos(int? id);
        List<tbl_department_member_position> list_bio_dep_pos();
        bool Deletebio_dep_pos(int id);

    }
}
