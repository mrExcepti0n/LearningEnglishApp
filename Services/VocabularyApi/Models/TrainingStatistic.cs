using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class TrainingStatistic
    {
        public int Id { get; set; }
        
        public TrainingTypeEnum TrainingType { get; set; }
        public bool IsReverseTraining { get; set; }

        public int UserVocabularyWordId { get; set; }

        public UserVocabularyWord UserVocabularyWord { get; set; }

        public DateTime? LastWrongAnswerDate { get; set; }

        public DateTime? LastRightAnswerDate { get; set; }

        public int WrongAnswerCount { get; set; }
        public int RightAnswerCount { get; set; }


        public bool NeedToRepeat()
        {
            return !(LastRightAnswerDate.HasValue && LastRightAnswerDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString());
        }
    }
}
