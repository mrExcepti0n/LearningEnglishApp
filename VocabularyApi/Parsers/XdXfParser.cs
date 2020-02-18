using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VocabularyApi.Parsers
{
    public class XdXfParser
    {
        public XdXfParser()
        {
        }

        public Dictionary<string, List<string>> Parse(byte[] file)
        {
            using (var memoryStream = new MemoryStream(file))
            {
                return Parse(memoryStream);
            }
        }


        public Dictionary<string, List<string>> Parse(Stream stream)
        {
            var xmlDocument = XDocument.Load(stream);

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

                var englishWord = word.Element("k").Value;
                var russianWord = word.LastNode.ToString().Trim('\n', '\r', ' ');

                if (!result.ContainsKey(englishWord)) {
                    result.Add(englishWord, new List<string> { russianWord });
                }

                result[englishWord].Add(russianWord);        
            }

            return result;
        }
    }
}
