using LearningEnglishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Infrastructure
{
    public static class TrainingFactory
    {
        private static TranslateWordTraining _translateTraining = new TranslateWordTraining(new List<TranslateWordQuestion> {
                new TranslateWordQuestion {Number = 1, Word = "Fox", RightTranslation = "Лиса"},
                new TranslateWordQuestion {Number = 2, Word = "Dog", RightTranslation = "Собака"},
                new TranslateWordQuestion { Number = 3,Word = "Cat", RightTranslation = "Кошка"},
            });

        private static ChooseTranslateTraining _chooseTranslateTraining = new ChooseTranslateTraining(new List<ChooseTranslateQuestion> {
            new ChooseTranslateQuestion {Number = 1, Word = "Boy", TranslationAnswers = new List<ChooseTranslateAnswer> {
                new ChooseTranslateAnswer { Translation = "Мальчик", IsRight = true},
                new ChooseTranslateAnswer { Translation = "Девочка", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Ручка", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Карандаш", IsRight = false},
            } },
            new ChooseTranslateQuestion { Number=2, Word = "Girl", TranslationAnswers = new List<ChooseTranslateAnswer> {
                new ChooseTranslateAnswer { Translation = "Мальчик", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Девочка", IsRight = true},
                new ChooseTranslateAnswer { Translation = "Ручка", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Карандаш", IsRight = false},
            } },
             new ChooseTranslateQuestion { Number =3, Word = "Pen", TranslationAnswers = new List<ChooseTranslateAnswer> {
                new ChooseTranslateAnswer { Translation = "Мальчик", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Девочка", IsRight = false},
                new ChooseTranslateAnswer { Translation = "Ручка", IsRight = true},
                new ChooseTranslateAnswer { Translation = "Карандаш", IsRight = false},
            } }
        });


        private static CollectWordTraining _collectWordTraining = new CollectWordTraining(new List<TranslateWordQuestion> {
            new TranslateWordQuestion {Number = 1, Word = "xof", RightTranslation = "fox"},
            new TranslateWordQuestion {Number = 2, Word = "gdo", RightTranslation = "dog"},
            new TranslateWordQuestion { Number = 3,Word = "atc", RightTranslation = "cat"}
        });

        public static TranslateWordTraining GetTranslateTraining() => _translateTraining;


        public static ChooseTranslateTraining GetChooseTranlsateTraining() => _chooseTranslateTraining;


        public static CollectWordTraining GetCollectWordTraining() => _collectWordTraining;
    }
}
