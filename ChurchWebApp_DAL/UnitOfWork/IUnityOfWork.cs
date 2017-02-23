using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWebApp_DAL.UnitOfWork
{
    public interface IUnityOfWork
    {
        ChurchAnalytics DbContext { get; }
        void Dispose();
        void Save();
    }
}
