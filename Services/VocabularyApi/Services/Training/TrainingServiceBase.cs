using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Dtos.Training;

namespace VocabularyApi.Services.Training
{
    public class TrainingServiceBase<T> where T : QuestionDto
    {
        private Random _random;

        protected TrainingServiceBase()
        {
            _random = new Random();
        }

        protected W[] ShuffleWords<W>(W[] words)
        {
            for (int i = words.Length - 1; i >= 1; i--)
            {
                int j = _random.Next(i + 1);
                var temp = words[j];
                words[j] = words[i];
                words[i] = temp;
            }

            return words;
        }
    }
}
