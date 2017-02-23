using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchWebApp_DAL;
using ChurchWebApp_DAL.UnitOfWork;
using ChurchWebApp_DAL.GenericRepository;
using System.Transactions;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public class OrganizationRepositoryService : IOrganizationRepositoryService
    {
        private IGenericRepository<tbl_organization> _organizationrepository;
        
        //private IGenericRepository<Genre> genreRepository;

        public OrganizationRepositoryService(IGenericRepository<tbl_organization>organizationrepository)
        {
            _organizationrepository = organizationrepository;          
        }

        public List<tbl_organization> Organizations()
        {

            return _organizationrepository.Include(new string[]{"tbl_country"}).ToList();
        }

        public tbl_organization findOrganization(int? id)
        {
            var Organization = _organizationrepository.getById(id);
            return Organization;
        }
        public void CreateOrganization(tbl_organization OrganizationEntity)
        {
            using (var scope = new TransactionScope())
            {
                _organizationrepository.insert(OrganizationEntity);
                _organizationrepository.CommitChanges();
                scope.Complete();
            }
      }

        public bool DeleteOrganization(int OrganizationId)
        {
            var success = false;
            if (OrganizationId > 0)
            {
                using (var scope = new TransactionScope())
                {

                    var organization = _organizationrepository.getById(OrganizationId);
                    if (organization != null)
                    {
                        _organizationrepository.delete(organization);
                        _organizationrepository.CommitChanges();
                        scope.Complete();
                        success = true;
                    }

                }

            }
            return success;
        }

        public bool UpdateOrganization(tbl_organization OrganizationEntity)
        {
            var success = false;
            if (OrganizationEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                        _organizationrepository.Update(OrganizationEntity);
                        _organizationrepository.CommitChanges();
                        scope.Complete();
                        success = true;
                }
            }

            return success;
        }

        public tbl_organization GetById(int id) => _organizationrepository.getById(id);
    }
}
