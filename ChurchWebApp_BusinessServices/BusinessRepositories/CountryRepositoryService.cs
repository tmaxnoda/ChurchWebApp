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
    public class CountryRepositoryService : ICountryRepositoryService
    {
        private IUnityOfWork _unitOfWork;
        private IGenericRepository<tbl_country> _countryrepository;
        //private IGenericRepository<Genre> genreRepository;

        public CountryRepositoryService(IUnityOfWork unitOfWork,
                   IGenericRepository<tbl_country> countryrepository)
        {
            this._unitOfWork = unitOfWork;
            _countryrepository = countryrepository;
        }
        public IQueryable<tbl_country> getcountries()
        {
            var countries = _countryrepository.getRecords();
            return countries;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }
    }
}
