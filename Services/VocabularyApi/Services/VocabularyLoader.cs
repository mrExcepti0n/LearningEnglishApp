using Data.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Infrastructure.DataAccess;
using VocabularyApi.Models;
using VocabularyApi.Parsers;

namespace VocabularyApi.Services
{
    public class VocabularyLoader
    {
        private readonly XdXfParser _parser;
        private LanguageEnum _fromLanguage = LanguageEnum.English;
        private LanguageEnum _toLanguage = LanguageEnum.Russian;

        private readonly VocabularyContext _context;

        public VocabularyLoader(VocabularyContext context, XdXfParser parser)
        {
            _context = context;
            _parser = parser;
        }


        public async Task LoadAsync(Stream file)
        {
            var parsedWords = await _parser.Parse(file);
            var allWords = await GetAllWords();

            foreach(var parsedWord in parsedWords)
            {
                allWords.TryGetValue(parsedWord.Key, out var word);

                if (word == null)
                {
                    var newWord = new VocabularyWord(parsedWord.Key, _fromLanguage, _toLanguage, parsedWord.Value);
                    await _context.VocabularyWords.AddAsync(newWord);
                } 
                else
                {
                    word.UpdateTranslations(_toLanguage, parsedWord.Value);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task<Dictionary<string, VocabularyWord>> GetAllWords()
        {
            return await _context.VocabularyWords.Where(vw => vw.Language == _fromLanguage)
                .Include(vw => vw.WordTranslations)
                .ToDictionaryAsync(vw => vw.Word);
        }
    }
}
