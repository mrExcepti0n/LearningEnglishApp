using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Models
{
    public class UserWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Transcription { get; set; }
        public string Translation { get; set; }       

        public string ImageSrc { get; set; }

        public byte KnowledgeRatio { get; private set; }

        public void SetKnowledgeRatio(byte ratio)
        {
            byte minRatio = 10;
            KnowledgeRatio = ratio < minRatio ? minRatio : ratio;
        }


        public string KnowledgeRatioClassName => KnowledgeRatio < 25 ? "bg-danger" : KnowledgeRatio < 75 ? "bg-warning" : "bg-success";
    }
}
