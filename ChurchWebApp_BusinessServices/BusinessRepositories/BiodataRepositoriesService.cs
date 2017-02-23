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
    public class BiodataRepositoriesService : IBiodataRepositoryService
    {
        private IGenericRepository<tbl_member_biodata> _memberbiodatarepository;

        public BiodataRepositoriesService(IGenericRepository<tbl_member_biodata> memberbiodatarepository)
        {
            _memberbiodatarepository = memberbiodatarepository;
        }

        public void CreateMemberBiodata(tbl_member_biodata member)
        {
            using (var scope = new TransactionScope())
            {
                _memberbiodatarepository.insert(member);
                _memberbiodatarepository.CommitChanges();
                scope.Complete();
            }
        }

        public bool DeleteMemberBiodata(int id)
        {
            var success = false;
            
                using (var scope = new TransactionScope())
                {

                var organization = _memberbiodatarepository.getById(id);
                if (organization != null)
                    {
                        _memberbiodatarepository.delete(organization);
                        _memberbiodatarepository.CommitChanges();
                        scope.Complete();
                        success = true;
                    }

                }

       
            return success;
        }

        public tbl_member_biodata findMember(int? id)
        {
            return _memberbiodatarepository.getById(id);

        }

        public IQueryable<tbl_member_biodata> getBiodatas()
        {
            var biodatas = _memberbiodatarepository.getRecords();
            return biodatas;
        }

        public List<tbl_member_biodata> Members()
        {
            return _memberbiodatarepository.getAllRecords().ToList();
        }

        public bool UpdateMemberBiodata(tbl_member_biodata members)
        {
            var success = false;
            if (members != null)
            {
                using (var scope = new TransactionScope())
                {
                    _memberbiodatarepository.Update(members);
                    _memberbiodatarepository.CommitChanges();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;

                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }
    }
}
