using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public abstract class QuestionBase
    {
        public int Number { get; set; }

        public string Word { get; set; }


        public abstract string Translation { get; }
    }
}
