using DataAccess.Data;
using DataAccess.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;

namespace Business.Service
{
    public class ResourceService:IResource
    {
        private TrainingsApiDBContext dbContext;

        public ResourceService(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public List<Resource> GetResources()
        {
            try
            {
                return dbContext.Resources1.ToList();
            }
            catch
            {
                throw;
            }
        }
        public Resource GetResource(int id)
        {
            try
            {
                Resource? resource = dbContext.Resources1.Find(id);
                if (resource != null)
                {
                    return resource;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddResources(Resource addResourceRequest)
        {
            try
            {
                dbContext.Resources1.Add(addResourceRequest);
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateResource(Resource resource)
        {
            try
            {
                dbContext.Entry(resource).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Resource DeleteResource(int id)
        {
            try
            {
                Resource? resource = dbContext.Resources1.Find(id);

                if (resource != null)
                {
                    dbContext.Resources1.Remove(resource);
                    dbContext.SaveChanges();
                    return resource;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        
        public bool CheckResource(int id)
        {
            return dbContext.Resources1.Any(e => e.Id == id);
        }
    }

}
