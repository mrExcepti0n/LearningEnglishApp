using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class WordImage
    {
        public int Id { get; set; }

        public bool IsThumbnail { get; set; }

        public byte[] Image { get; set; }
    }
}
