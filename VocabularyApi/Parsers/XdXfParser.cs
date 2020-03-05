using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VocabularyApi.Parsers
{
    public class XdXfParser
    {
        public XdXfParser()
        {
        }
    

        public async Task<Dictionary<string, List<string>>> Parse(Stream stream)
        {
            var xmlDocument = await XDocument.LoadAsync(stream, LoadOptions.PreserveWhitespace, CancellationToken.None);

            var result = new Dictionary<string, List<string>>();

            var xdxfDictionary = xmlDocument.Element("xdxf");

            if (xdxfDictionary == null)
            {
                return result;
            }

            foreach (var word in xdxfDictionary.Elements("ar"))
            {
                if (word.Nodes().Count() != 2)
                {
                    continue;
                }

                var englishWord = word.Element("k").Value.ToLower();
                var russianWord = word.LastNode.ToString().Trim('\n', '\r', ' ');

                var match = Regex.Match(russianWord, "(?<=\")(.*)(?=\")");
                russianWord = match.Value.ToLower();

                if (!result.ContainsKey(englishWord)) {
                    result.Add(englishWord, new List<string> {});
                }

                result[englishWord].Add(russianWord);        
            }

            return result;
        }
    }
}
