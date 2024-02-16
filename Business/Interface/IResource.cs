using DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IResource
    {
         List<Resource> GetResources();

        Resource GetResource(int id);
        void AddResources(Resource addResourceRequest);
        void UpdateResource(Resource resource);
        Resource DeleteResource(int id);
        bool CheckResource(int id);
    }
}
