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

        private TrainingStatistic GetTrainingStatistic(TrainingTypeEnum trainingType, bool isReverseTraining) => TrainingStatistics.SingleOrDefault(ts => ts.TrainingType == trainingType && ts.IsReverseTraining == isReverseTraining);


        public decimal GetKnowledgeRatio(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            var trainingStatistic = GetTrainingStatistic(trainingType, isReverseTraining);
            if (trainingStatistic == null)
            {
                return 0;
            }

            return trainingStatistic.RightAnswerCount - trainingStatistic.WrongAnswerCount;
        }

        public int GetKnowledgeRatio()
        {
            var succesfullTrainings = TrainingStatistics.Count(ts => !ts.NeedToRepeat());

            var totalTrainings = Enum.GetValues(typeof(TrainingTypeEnum)).Length * 2;

            return succesfullTrainings *100 / totalTrainings ;
        }

        public bool NeedToRepeat(TrainingTypeEnum trainingType, bool isReverseTraining)
        {
            var trainingStatistic = GetTrainingStatistic(trainingType, isReverseTraining);
            if (trainingStatistic != null && !trainingStatistic.NeedToRepeat())
            {
                return false;
            }
            return true;
        }

        public void SetTrainingResult(TrainingTypeEnum trainingType, bool isReverseTraining, bool isRight, string userOption)
        {
            var trainingStatistic = GetTrainingStatistic(trainingType, isReverseTraining);
            if (trainingStatistic == null)
            {
                trainingStatistic = new TrainingStatistic { TrainingType = trainingType, IsReverseTraining = isReverseTraining, UserVocabularyWordId = Id };
                TrainingStatistics.Add(trainingStatistic);
            }

            if (isRight)
            {
                trainingStatistic.RightAnswerCount++;
                trainingStatistic.LastRightAnswerDate = DateTime.Now;
            } else
            {
                trainingStatistic.WrongAnswerCount++;
                trainingStatistic.LastWrongAnswerDate = DateTime.Now;
            }
        }
    }
}
