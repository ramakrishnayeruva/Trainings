using Business.Interface;
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
     public class TrainingService:ITraining
    {
        private TrainingsApiDBContext dbContext;

        public TrainingService(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public List<Trainings> GetTrainings()
        {
            try
            {
                return dbContext.Trainings.ToList();
            }
            catch
            {
                throw;
            }
        }
        public Trainings GetTraining(int id)
        {
            try
            {
                Trainings? training = dbContext.Trainings.Find(id);
                if (training != null)
                {
                    return training;
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

        public void AddTraining(Trainings addResourceRequest)
        {
            try
            {
                dbContext.Trainings.Add(addResourceRequest);
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateTraining(Trainings training)
        {
            try
            {
                dbContext.Entry(training).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Trainings DeleteTraining(int id)
        {
            try
            {
                Trainings? training = dbContext.Trainings.Find(id);

                if (training != null)
                {
                    dbContext.Trainings.Remove(training);
                    dbContext.SaveChanges();
                    return training;
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

        public bool CheckTraining(int id)
        {
            return dbContext.Trainings.Any(e => e.Id == id);
        }
    }
}
