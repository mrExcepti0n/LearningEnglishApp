using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos.Training
{
    public class TrainingWordResultDto
    {
        public int UserWordId { get; set; }

        public bool IsRightAnswer { get; set; }

        public string UserAnswer { get; set; }
    }
}
