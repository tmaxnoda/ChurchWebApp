using ChurchWebApp_DAL;
using ChurchWebApp_DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public class Bio_Dep_Pos_Services: IBio_Dep_Pos_Services
    {
        private IGenericRepository<tbl_department_member_position> _bio_dep_pos_repository;

        public Bio_Dep_Pos_Services(IGenericRepository<tbl_department_member_position> bio_dep_pos_repository)
        {
            _bio_dep_pos_repository = bio_dep_pos_repository;
        }

        public List<tbl_department_member_position> bio_dep_pos()
        {

            return _bio_dep_pos_repository.Include(new string[] {"tbl_department", "tbl_member_biodata","tbl_position"}).ToList();
        }
        public void Createbio_dep_pos(tbl_department_member_position position)
        {
            using (var scope = new TransactionScope())
            {
                _bio_dep_pos_repository.insert(position);
                _bio_dep_pos_repository.CommitChanges();
                scope.Complete();
            }
        }

       

       

        public bool Deletebio_dep_pos(int id)
        {
            var success = false;
            var mem_dep_id = _bio_dep_pos_repository.getById(id);
         
                using (var scope = new TransactionScope())
                {

                    //var organization = _memberbiodatarepository.getById(memberId);
                    if (mem_dep_id != null)
                    {
                        _bio_dep_pos_repository.delete(mem_dep_id);
                        _bio_dep_pos_repository.CommitChanges();
                        scope.Complete();
                        success = true;
                    }

                }

           
            return success;
        }

        public tbl_department_member_position findbio_dep_pos(int? id)
        {
            return _bio_dep_pos_repository.getById(id);

        }


        public List<tbl_department_member_position> list_bio_dep_pos()
        {
            return _bio_dep_pos_repository.getAllRecords().ToList();
        }



        public bool Updatebio_dep_pos(tbl_department_member_position members)
        {
            var success = false;
            if (members != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bio_dep_pos_repository.Update(members);
                    _bio_dep_pos_repository.CommitChanges();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }
    }
}
