using ChurchWebApp_DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_DAL.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region 
        //Private member variables...
        //internal ChurchAnalytics _context = new ChurchAnalytics();
        protected ChurchAnalytics _context;
        //internal DbSet<TEntity> _dbset;
        #endregion

        #region Public Constructor...   
        /// <summary>   
        /// Public Constructor,initializes privately declared local variables. 
        /// </summary>   
        /// <param name="context"></param>   

        public GenericRepository(IUnityOfWork unitOfWork)
        {
            this._context = unitOfWork.DbContext;
            //this._dbset = context.Set<TEntity>();
        }

        #endregion


        #region Public member methods...   
        /// <summary>   
        /// generic Get method for Entities   
        /// </summary> 
        /// <returns></returns> 

        public virtual IEnumerable<TEntity> get()
        {
            IQueryable<TEntity> query = this._context.Set<TEntity>();
            return query.ToList();
        }

        /// <summary>   
        /// Generic get method on the basis of id for Entities. 
        /// </summary>   
        ///  <param name="id"></param>   
        ///  <returns></returns> 

        public virtual TEntity getById(int? id)
        {
            return this._context.Set<TEntity>().Find(id);
        }

        /// <summary> 
        /// generic Insert method for the entities   
        ///  </summary>   
        ///  <param name="entity"></param> 

        public virtual void insert(TEntity entity)
        {
            //_dbset.Add(entity);
            this._context.Set<TEntity>().Add(entity);
        }


        /// <summary> 
        ///  Generic Delete method for the entities   
        ///   </summary> 
        ///  <param name="id"></param> 

        public virtual void delete(TEntity entityToDelete)
        {
            //TEntity entityToDelete = _dbset.Find(id);
            //TEntity entityToDelete = this._context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        /// <summary>   
        /// Generic Delete method for the entities 
        ///  </summary> 
        ///   <param name="entityToDelete"></param> 
        private void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this._context.Set<TEntity>().Attach(entityToDelete);
            }
            this._context.Set<TEntity>().Remove(entityToDelete);
        }


        /// <summary>   
        /// Generic update method for the entities 
        /// </summary>   
        ///  <param name="entityToUpdate"></param> 

        public virtual void Update(TEntity entityToUpdate)
        {
            //_dbset.Attach(entityToUpdate);
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
               _context.Set<TEntity>().Attach(entityToUpdate);
            }

            _context.Entry(entityToUpdate);
        }

        /// <summary>   
        /// generic method to get many record on the basis of a condition.   
        /// </summary> 
        ///  <param name="where"></param> 
        ///  <returns></returns>   

        public virtual IEnumerable<TEntity> getManyWithwhere(Func<TEntity, bool> Where)
        {
            //return _dbset.Where(Where).ToList();
            return this._context.Set<TEntity>().Where(Where).ToList();
        }

        /// <summary> 
        ///  generic method to get many record on the basis 
        ///  fof a condition but query able. 
        ///  <returns></returns>

        public virtual IQueryable<TEntity> getManyWithQuarable(Func<TEntity, bool> where)
        {
            //return _dbset.Where(where).AsQueryable();
            return this._context.Set<TEntity>().Where(where).AsQueryable();

        }


        /// <summary> 
        /// generic get method , fetches data for
        /// the entities on the basis of condition. 
        /// </summary> 
        /// <param name="where"></param>   
        ///  <returns></returns>   

        public TEntity getWithFirstOrDefault(Func<TEntity, Boolean> where)
        {
            //return _dbset.Where(where).FirstOrDefault<TEntity>();
            return this._context.Set<TEntity>().Where(where).FirstOrDefault<TEntity>();
        }

       

        /// <summary>
        /// generic delete method , deletes 
        /// data for the entities on the basis of condition. 
        /// </summary> 
        /// <param name="where"></param> 
        /// <returns></returns>   

        public void deleteBasedOnConditions(Func<TEntity, Boolean> where)
        {
            //IQueryable<TEntity> objects = _dbset.Where<TEntity>(where).AsQueryable();
            IQueryable<TEntity> objects = this._context.Set<TEntity>().Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
            {
                this._context.Set<TEntity>().Remove(obj);
            }
        }

        /// <summary>   
        /// generic method to fetch 
        /// all the records from db  
        /// </summary>   
        /// <returns></returns> 

        public virtual IEnumerable<TEntity> getAllRecords()
        {
            return this._context.Set<TEntity>().ToList();
        }

        public virtual IQueryable<TEntity> getRecords()
        {
            return this._context.Set<TEntity>();
        }


        /// <summary>   
        ///  Include multiple   
        ///   </summary> 
        ///   <param name="predicate"></param> 
        ///   <param name="include"></param>   
        ///   <returns></returns>  
        public IQueryable<TEntity> getWithInclude(System.Linq.Expressions.Expression
            <Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this._context.Set<TEntity>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public IQueryable<TEntity> Include(params string[] include)
        {
            IQueryable<TEntity> query = this._context.Set<TEntity>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query;
        }


        /// <summary> 
        /// Generic method to check if entity exists 
        ///  </summary>   
        ///   <param name="primaryKey"></param>   
        ///   <returns></returns>   
        public bool exits(object primarykey)
        {
            return this._context.Set<TEntity>().Find(primarykey) != null;
        }

        /// <summary>   
        /// Gets a single record 
        /// by the specified criteria (usually the unique identifier) 
        /// </summary> 
        ///  <param name="predicate">Criteria to match on</param> 
        ///   <returns>A single record that matches the specified criteria</returns> 

        public TEntity getSingle(Func<TEntity, bool> predicate)
        {
            return this._context.Set<TEntity>().Single<TEntity>(predicate);
        }

        /// <summary>   
        /// The first record matching the specified criteria 
        ///  </summary>   
        ///   <param name="predicate">Criteria to match on</param>   
        ///  <returns>A single record containing the first 
        ///   record matching the specif ied criteria</returns>  

        public TEntity getFirst(Func<TEntity, bool> predicate)
        {
            return this._context.Set<TEntity>().First<TEntity>(predicate);
        }

       

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this._context.Set<TEntity>().Where(predicate);
            return query;
        }

        public TEntity getWithFirstInDecendingOrder(Func<TEntity, bool> where)
        {
           var id = this._context.Set<TEntity>().OrderByDescending(where).First();
           return id;
        }

        public void CommitChanges() => _context.SaveChanges();
    }

    #endregion
}

