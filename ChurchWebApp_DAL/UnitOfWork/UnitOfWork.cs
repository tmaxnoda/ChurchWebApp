using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_DAL.UnitOfWork
{
    public class UnitOfWork : IUnityOfWork, IDisposable
    {
        //protected string ConnectionString;
        private ChurchAnalytics context;
        public ChurchAnalytics DbContext
        {
            get
            {
                if (context == null)
                {
                    context = new ChurchAnalytics();
                }
                return context;
            }
        }

        public void Save()
        {

            //bool saveFailed;
            //do
            //{
            //    saveFailed = false;
            //    try
            //    {
            //        context.SaveChanges();
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        saveFailed = true;

            //        // Get the current entity values and the values in the database 
            //        var entry = ex.Entries.Single();
            //        var currentValues = entry.CurrentValues;
            //        var databaseValues = entry.GetDatabaseValues();

            //        // Choose an initial set of resolved values. In this case we 
            //        // make the default be the values currently in the database. 
            //        var resolvedValues = databaseValues.Clone();

            //        // Have the user choose what the resolved values should be 
            //        HaveUserResolveConcurrency(currentValues, databaseValues, resolvedValues);

            //        // Update the original values with the database values and 
            //        // the current values with whatever the user choose. 
            //        entry.OriginalValues.SetValues(databaseValues);
            //        entry.CurrentValues.SetValues(resolvedValues);
            //    }

            //    } while (saveFailed);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors: ", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(" Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
        }
private void HaveUserResolveConcurrency(DbPropertyValues currentValues,
                                        DbPropertyValues databaseValues,
                                        DbPropertyValues resolvedValues)
        {
            // Show the current, database, and resolved values to the user and have 
            // them edit the resolved values to get the correct resolution. 
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
