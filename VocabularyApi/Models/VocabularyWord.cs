using Data.Core;
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

        //public byte[] Image { get; set; }

        public byte[] AudioRecord { get; set; }

        public LanguageEnum Language { get; set; }

        public ICollection<WordTranslation> WordTranslations { get; set; }

        public int? ImageId { get; set; }

        public int? ThumbnailId { get; set; }

        public WordImage Image { get; set; }

        public WordImage Thumbnail { get; set; }
    }
}
