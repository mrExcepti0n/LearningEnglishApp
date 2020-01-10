using LearningEnglishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public static class TrainingFactory
    {
        private static Training _training = new Training(new List<Question> {
                new Question { Word = "Fox", RightAnswer = "Лиса"},
                new Question { Word = "Dog", RightAnswer = "Собака"},
                new Question { Word = "Cat", RightAnswer = "Кошка"},
            });


        public static Training GetTraining() => _training;
    }
}
