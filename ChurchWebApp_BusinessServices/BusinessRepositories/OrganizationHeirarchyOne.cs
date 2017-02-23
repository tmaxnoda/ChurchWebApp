using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchWebApp_DAL;
using ChurchWebApp_DAL.UnitOfWork;
using ChurchWebApp_DAL.GenericRepository;

namespace ChurchWebApp_BusinessServices.BusinessRepositories
{
    public class OrganizationHeirarchyOne : IOrganizationHeirarchyOne
    {
        private IUnityOfWork _unitOfWork;
        private IGenericRepository<tbl_member_biodata> _OrganizationHeirarchyOnerepository;
        //private IGenericRepository<Genre> genreRepository;

        public OrganizationHeirarchyOne(IUnityOfWork unitOfWork,
                   IGenericRepository<tbl_member_biodata> OrganizationHeirarchyOnerepository)
        {
            this._unitOfWork = unitOfWork;
            _OrganizationHeirarchyOnerepository = OrganizationHeirarchyOnerepository;
        }
        public IEnumerable<tbl_member_biodata> getLeaders()
        {
            var heirarchyone = _OrganizationHeirarchyOnerepository.getAllRecords();
            return heirarchyone;
        }
    }
}
