using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class QuestionResult
    {
        public string Word { get; set; }

        public List<AnswerResult> Answers { get; set; }
    }
}
