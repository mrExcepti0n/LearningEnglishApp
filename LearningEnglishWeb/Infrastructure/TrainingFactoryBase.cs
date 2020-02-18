using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public abstract class TrainingFactoryBase<T>
    {
        public abstract T GetTraining();
    }
}
