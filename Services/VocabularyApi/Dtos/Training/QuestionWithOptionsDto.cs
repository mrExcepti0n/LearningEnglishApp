using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Dtos.Training
{
    public class QuestionWithOptionsDto : QuestionDto
    {
        public QuestionWithOptionsDto(UserVocabularyWord userWord, int number, List<string> options) : base(userWord, number)
        {
            Options = options;
        }
        public List<string> Options { get; set; }
    }
}
