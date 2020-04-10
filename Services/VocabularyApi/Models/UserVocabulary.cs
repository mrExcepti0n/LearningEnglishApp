using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class UserVocabulary
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public Guid UserId { get; set; }

        public int? WordSetId { get; set; }


        public bool IsDefault { get; set; }

        public ICollection<UserVocabularyWord> Words { get; set; } = new HashSet<UserVocabularyWord>();
        public WordSet WordSet { get; set; }
    }
}
