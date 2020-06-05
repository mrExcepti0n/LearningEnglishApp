using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Core;

namespace LearningEnglishWeb.Areas.Training.Services.Helpers
{
    public static class Api
    {
        public static class Training
        {

            public static string GetRequiringStudyWords(string baseUrl, TrainingTypeEnum trainingType, bool isReverseTraining)
            {
                return $"{baseUrl}/RequiringStudyWords?trainingType={trainingType}&isReverseTraining={isReverseTraining}";
            }

            internal static string GetAvailableTrainingWordsCount(string baseUrl)
            {
                return $"{baseUrl}/AvailableTrainingWordsCount";
            }

            internal static string GetTrainingWords(string baseUrl, IEnumerable<int> userSelectedWords)
            {
                var parameters = userSelectedWords.Select(uw => $"userSelectedWords={uw}");
                return $"{baseUrl}/TrainingWords?" + String.Join('&', parameters);
            }

            internal static string GetTrainingWordsRatio(string baseUrl, ICollection<int> userWordIs)
            {
                var urlBuilder = new StringBuilder($"{baseUrl}/TrainingWordsRatio");

                if (userWordIs.Any())
                {
                    var parameters = string.Join('&', userWordIs.Select(uw => $"userWordIds={uw}"));
                    urlBuilder.Append('?');
                    urlBuilder.Append(parameters);
                }

                return urlBuilder.ToString();
            }


            internal static string SaveTrainingResult(string baseUrl)
            {
                return baseUrl;
            }
        }
    }
}
