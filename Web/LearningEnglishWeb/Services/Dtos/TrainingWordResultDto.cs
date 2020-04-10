using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Dtos
{
    public class TrainingWordResultDto
    {
        public int UserWordId { get; set; }

        public bool IsRightAnswer { get; set; }

        public string UserAnswer { get; set; }
    }
}
