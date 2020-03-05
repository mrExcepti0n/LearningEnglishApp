using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.ViewModels
{
    public class TranslationListModel
    {
        public TranslationListModel(IEnumerable<string> translations)
        {
            Translations = translations;         
        }

        public IEnumerable<string> Translations { get; set; }

       

    }
}
