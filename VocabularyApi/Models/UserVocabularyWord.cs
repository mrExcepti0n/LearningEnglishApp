using Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Models
{
    public class UserVocabularyWord
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public int UserVocabularyId { get; set; }
        public UserVocabulary UserVocabulary { get; set; }

        public ICollection<TrainingStatistic> TrainingStatistics { get; set; }


        public decimal GetKnowledgeRatio(TrainingTypeEnum languageEnum)
        {
            var trainingStatistic = TrainingStatistics.SingleOrDefault(ts => ts.TrainingType == languageEnum);
            if (trainingStatistic == null)
            {
                return 0;
            }

            return trainingStatistic.RightAnswerCount - trainingStatistic.WrongAnswerCount;
        }

        public bool NeedToRepeat(TrainingTypeEnum languageEnum)
        {
            var trainingStatistic = TrainingStatistics.SingleOrDefault(ts => ts.TrainingType == languageEnum);
            if (trainingStatistic != null && trainingStatistic.LastRightAnswerDate.HasValue && trainingStatistic.LastRightAnswerDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                return true;
            }
            return false;
        }
    }
}
