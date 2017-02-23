using ChurchWebApp_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
   public interface IPositionService
    {
        IQueryable<tbl_position> findPositionWhere(Expression<Func<tbl_position, bool>> predicate);
        IQueryable<tbl_position> getPositions();
        void CreatePosition(tbl_position OrganizationEntity);

        bool UpdatePosition(tbl_position OrganizationEntity);

        bool DeletePosition(int id);

        List<tbl_position> Positions();

        tbl_position findPosition(int? id);
    }
}
