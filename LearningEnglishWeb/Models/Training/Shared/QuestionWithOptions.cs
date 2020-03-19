using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.Shared
{
    public class QuestionWithOptions : Question
    {
        public IEnumerable<Answer> Options { get; set; }


        public override bool CheckAnswer(string answer)
        {
            var option = Options.FirstOrDefault(opt => opt.Option == answer);
            if (option != null)
            {
                option.UserSelect = true;                
            }

            return base.CheckAnswer(answer);
        }

    }
}
