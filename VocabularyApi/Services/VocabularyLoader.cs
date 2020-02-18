using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Parsers;

namespace VocabularyApi.Services
{
    public class VocabularyLoader
    {
        private XdXfParser _parser = new XdXfParser();
        private LanguageEnum _fromLanguage = LanguageEnum.English;
        private LanguageEnum _toLanguage = LanguageEnum.Russian;

        private VocabularyContext _context;

        public VocabularyLoader(VocabularyContext context)
        {
            _context = context;
        }

        public void Load(byte[] file)
        {
            var parsedWords = _parser.Parse(file);
            var allwords = _context.VocabularyWords.Where(vw => vw.Language == _fromLanguage)
                                                   .Include(vw => vw.WordTranslations)
                                                   .ToDictionary(vw => vw.Word);

            List<VocabularyWord> newWords = new List<VocabularyWord>();

            foreach(var parsedWord in parsedWords)
            {
                allwords.TryGetValue(parsedWord.Key, out var word);

                if (word == null)
                {
                    var newWord = new VocabularyWord
                    {
                        Language = _fromLanguage,
                        Word = parsedWord.Key,
                        WordTranslations = parsedWord.Value.Select(v => new WordTranslation { Language = _toLanguage, Translation = v }).ToList()
                    };

                    newWords.Add(newWord);
                } 
                else
                {
                   var newTrasnlations = parsedWord.Value.Where(t => !word.WordTranslations.Any(wt => wt.Translation == t)).ToList();
                   foreach (var newTranslation in newTrasnlations)
                   {
                        word.WordTranslations.Add(new WordTranslation { Language = _toLanguage, Translation = newTranslation });
                   }
                }
            }

            if (newWords.Any())
            {
                _context.VocabularyWords.AddRange(newWords);
            }

            _context.SaveChanges();
        }
    }
}
