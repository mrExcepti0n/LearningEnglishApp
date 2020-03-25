using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class UserWord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Transcription { get; set; }
        public string Translation { get; set; }       

        public string ImageSrc { get; set; }

    }
}
