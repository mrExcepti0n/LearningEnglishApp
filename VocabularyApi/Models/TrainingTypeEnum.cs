using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public enum TrainingTypeEnum
    {
        TranslateWord = 1,
        ReverseTranslateWord = 2,
        ChooseTranslate = 3,
        ReverseChooseTranslate = 4,
        CollectWord = 5,
        ReverseCollectWord = 6
    }
}
