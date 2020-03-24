using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Services.Helpers
{
    public static class Api
    {
        public static class Vocabulary
        {
            public static string GetWords(string baseUrl, string mask)
            {               

                return mask == null? baseUrl : $"{baseUrl}?mask={mask}";
            }

            public static string GetTranslation(string baseUrl, string word)
            {
                return $"{baseUrl}/{word}/translations";
            }

            public static string AddWord(string baseUrl)
            {
                return baseUrl;
            }
            public static string RemoveWord(string baseUrl, string name, string translation)
            {
                return $"{baseUrl}/{name}/{translation}";
            }

            public static string LoadDictionary(string baseUrl)
            {
                return baseUrl + "/Load";
            }

            public static string GetRequiringStudyWords(string baseUrl)
            {
                return baseUrl + "/RequiringStudyWords";
            }
          
        }


        public static class WordImage
        {
            public static string GetWordImage(string baseUrl, string word)
            {
                return $"{baseUrl}/{word}";
            }
        }

        public static class WordSet
        {
            internal static string GetWordSet(string baseUrl, int id)
            {
                return $"{baseUrl}/{id}";
            }

            internal static string AddWordSet(string baseUrl)
            {
                return baseUrl;
            }

            internal static string AddWords(string baseUrl)
            {
                return $"{baseUrl}/UserWordSet";
            }

            internal static string GetWordSets(string baseUrl)
            {
                return baseUrl;
            }
        }
    }
}
