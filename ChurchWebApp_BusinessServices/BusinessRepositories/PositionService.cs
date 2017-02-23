using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchWebApp_DAL;
using ChurchWebApp_DAL.GenericRepository;
using System.Transactions;
using System.Linq.Expressions;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public class PositionService : IPositionService
    {
        private IGenericRepository<tbl_position> _positionrepository;

        public PositionService(IGenericRepository<tbl_position> positionrepository)
        {
            _positionrepository = positionrepository;
        }

       
        public void CreatePosition(tbl_position position)
        {
            using (var scope = new TransactionScope())
            {
                _positionrepository.insert(position);
                _positionrepository.CommitChanges();
                scope.Complete();
            }
        }

       

        public bool DeletePosition(int id)
        {
            var success = false;
           
                using (var scope = new TransactionScope())
                {

                var position = _positionrepository.getById(id);
                if (position != null)
                    {
                        _positionrepository.delete(position);
                        _positionrepository.CommitChanges();
                        scope.Complete();
                        success = true;
                    }

                }

          
            return success;
        }

        public tbl_position findPosition(int? id)
        {
            return _positionrepository.getById(id);

        }

        public IQueryable<tbl_position> findPositionWhere(Expression<Func<tbl_position, bool>> predicate)
        {
            return _positionrepository.FindBy(predicate);

        }

        public IQueryable<tbl_position> getPositions()
        {
            var positions = _positionrepository.getRecords();
            return positions;
        }
        public List<tbl_position> Positions()
        {
            return _positionrepository.getAllRecords().ToList();
        }

      

        public bool UpdatePosition(tbl_position members)
        {
            var success = false;
            if (members != null)
            {
                using (var scope = new TransactionScope())
                {
                    _positionrepository.Update(members);
                    _positionrepository.CommitChanges();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

       
    }
}
