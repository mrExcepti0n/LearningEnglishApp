using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class VocabularyWord
    {
        protected VocabularyWord()
        {

        }
        public VocabularyWord(string word, LanguageEnum fromLanguage, LanguageEnum toLanguage, List<string> translations)
        {
            Word = word;
            Language = fromLanguage;
            WordTranslations = translations.Select(tr => new WordTranslation(tr,toLanguage, this)).ToList();
        }

        public int Id { get; protected set; }

        public string Word { get; protected set; }

        public string Transcription { get; protected set; }

        public byte[] AudioRecord { get; protected set; }

        public LanguageEnum Language { get; protected set; }

        public ICollection<WordTranslation> WordTranslations { get; protected set; }

        public int? ImageId { get; protected set; }

        public int? ThumbnailId { get; protected set; }

        public WordImage Image { get; protected set; }

        public WordImage Thumbnail { get; protected set; }

        public void UpdateTranslations(LanguageEnum language, List<string> translations)
        {
            var newTranslations = translations.Except(WordTranslations.Where(wt => wt.Language == language)
                                                                                        .Select(wt => wt.Translation));

            foreach (var newTranslation in newTranslations)
            {
                WordTranslations.Add(new WordTranslation { Language = language, Translation = newTranslation });
            }
        }


        public void SaveImages(byte[] getImage, byte[] getThumbnail)
        {
            Image = new WordImage { Image = getImage, IsThumbnail = false };
            Thumbnail = new WordImage { Image = getThumbnail, IsThumbnail = true };
        }
    }
}
