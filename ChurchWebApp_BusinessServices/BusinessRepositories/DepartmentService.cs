using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchWebApp_DAL;
using ChurchWebApp_DAL.GenericRepository;
using System.Transactions;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public class DepartmentService : IDepartmentService
    {
        private IGenericRepository<tbl_department> _departmentrepository;

        public IQueryable<tbl_department> getDepartment()
        {
            var departments = _departmentrepository.getRecords();
            return departments;
        }
        public DepartmentService(IGenericRepository<tbl_department> departmentrepository)
        {
            _departmentrepository = departmentrepository;
        }

        public void CreateDepartment(tbl_department department)
        {
            using (var scope = new TransactionScope())
            {
                _departmentrepository.insert(department);
                _departmentrepository.CommitChanges();
                scope.Complete();
            }
        }

       

        public bool DeleteDepartment(int id)
        {
            var success = false;
           
                using (var scope = new TransactionScope())
                {

                var department = _departmentrepository.getById(id);
                if (department!= null)
                    {
                        _departmentrepository.delete(department);
                        _departmentrepository.CommitChanges();
                        scope.Complete();
                        success = true;
                    }

                }

           
            return success;
        }

        public tbl_department findDepartment(int? id)
        {
            return _departmentrepository.getById(id);

        }

        public List<tbl_department> Departments()
        {
            return _departmentrepository.getAllRecords().ToList();
        }

      

        public bool UpdateDepartment(tbl_department department)
        {
            var success = false;
            if (department != null)
            {
                using (var scope = new TransactionScope())
                {
                    _departmentrepository.Update(department);
                    _departmentrepository.CommitChanges();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

       
    }
}
