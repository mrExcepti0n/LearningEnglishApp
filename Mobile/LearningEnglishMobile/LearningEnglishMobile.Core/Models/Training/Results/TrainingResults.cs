using Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Models.Training.Results
{
    public class TrainingResults
    {
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }
        public List<TrainingWordResult> TrainingWordResults { get; set; }
    }
}
