using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class UserVocabulary
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }


        public int GetKnowledgeRatio()
        {
            return 100;
        }
    }
}
