using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.ManagementService
{
    public interface IBaseManagementService
    {
        public void GetAll() { }
        public void GetById() { }
        public void Save() { }
        public void Delete() { }
    }
}
