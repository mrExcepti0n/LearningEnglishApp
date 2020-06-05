using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class Student
    {
        public Guid Id { get; protected set; }

        public IReadOnlyCollection<UserVocabularyWord> UserVocabularyWords { get; protected set; }
    }
}
