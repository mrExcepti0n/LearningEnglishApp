using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models.Training.Shared
{
    public class QuestionWithOptions : Question
    {
        public IEnumerable<string> Options { get; set; }


        //public override bool CheckAnswer(string answer)
        //{
        //    if (Options.Any(opt => opt.UserSelect))
        //    {
        //        throw new MethodAccessException("Попытка повторно ответить на вопрос");
        //    }

        //    var option = Options.FirstOrDefault(opt => opt.Option == answer);
        //    if (option != null)
        //    {
        //        option.UserSelect = true;                
        //    }

        //    return base.CheckAnswer(answer);
        //}

    }
}
