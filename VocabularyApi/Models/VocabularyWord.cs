using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class VocabularyWord
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public string Trasncription { get; set; }

        public byte[] Image { get; set; }

        public byte[] AudioRecord { get; set; }

        public LanguageEnum Language { get; set; }

        public ICollection<WordTranslation> WordTranslations { get; set; }
    }
}
