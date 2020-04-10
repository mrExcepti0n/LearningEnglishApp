using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Dtos
{
    public class TrainingWordRatioDto
    {
        public int UserWordId { get; set; }

        public byte TrainingRatio { get; set; }
    }
}
