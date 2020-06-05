using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningEnglishWeb.Services.Helpers
{
    public static class Api
    {
        public static class Vocabulary
        {
            public static string GetWords(string baseUrl, string mask, int? vocabularyId)
            {     
                var url = $"{baseUrl}/words";

                List<string> parameters = new List<string>();
                if (vocabularyId != null)
                {
                    parameters.Add($"vocabularyId={vocabularyId.Value}");
                }

                if (!string.IsNullOrWhiteSpace(mask))
                {
                    parameters.Add($"mask={mask}");
                }

                if (parameters.Any())
                {
                    url += "?" + string.Join('&', parameters);
                }
               

                return url;
            }

            public static string GetTranslation(string baseUrl, string word)
            {
                return $"{baseUrl}/{word}/translations";
            }

            public static string AddWord(string baseUrl)
            {
                return $"{baseUrl}/words";
            }

            public static string RemoveWord(string baseUrl, int wordId)
            {
                var url = $"{baseUrl}/words/{wordId}";
                return url;
            }

            internal static string GetVocabularies(string baseUrl)
            {
                return baseUrl;
            }

            internal static string GetVocabulary(string baseUrl, int id)
            {
                return $"{baseUrl}/{id}";
            }


            public static string LoadDictionary(string baseUrl)
            {
                return baseUrl + "/Load";
            }

        }


        public static class WordImage
        {
            public static string GetWordImage(string baseUrl, string word)
            {
                return $"{baseUrl}/{word}";
            }
            public static string GetThumbnail(string baseUrl, string word)
            {
                return $"{baseUrl}/{word}/thumbnail";
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
