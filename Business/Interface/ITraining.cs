using DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ITraining
    {
        List<Trainings> GetTrainings();

        Trainings GetTraining(int id);
        void AddTraining(Trainings addResourceRequest);
        void UpdateTraining(Trainings resource);
        Trainings DeleteTraining(int id);
        bool CheckTraining(int id);
    }
}
