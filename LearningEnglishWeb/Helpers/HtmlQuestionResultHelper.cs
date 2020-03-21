using LearningEnglishWeb.Models;
using LearningEnglishWeb.ViewModels.Training;
using LearningEnglishWeb.ViewModels.Training.TranslateWord;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Helpers
{
    public static class HtmlQuestionResultHelper
    {
        public static HtmlString CreateTranslateWordTrainingAnswerResult(this IHtmlHelper html, TranslateWordAnswerViewModel answerResult, string checkAnswerUrl)
        {
            var area = new TagBuilder("div");


            /* var p = new TagBuilder("p");
             p.InnerHtml.Append(answerResult.Word);
             area.InnerHtml.AppendHtml(p);*/

            var answer = new TagBuilder("div");

            var button = new TagBuilder("button");
            button.InnerHtml.Append("Следующий вопрос");
            button.AddCssClass("nextAnswerBtn");

            button.Attributes.Add(new KeyValuePair<string, string>("data-request-url", checkAnswerUrl));


            if (answerResult.UserTranslation == answerResult.RightTranslation)
            {
                answer.InnerHtml.AppendHtml(GetRightAnswerElement(answerResult.UserTranslation));
            }
            else
            {
                var wrongAnswer = new TagBuilder("p");
                wrongAnswer.InnerHtml.Append(answerResult.UserTranslation ?? "ответ отсутсвует");
                wrongAnswer.AddCssClass("wrongAnswer");

                answer.InnerHtml.AppendHtml(wrongAnswer);
                answer.InnerHtml.AppendHtml(GetRightAnswerElement(answerResult.RightTranslation));
            }


            area.InnerHtml.AppendHtml(answer);
            area.InnerHtml.AppendHtml(button);

            var writer = new StringWriter();
            area.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
               
        private static TagBuilder GetRightAnswerElement(string answer)
        {
            var rightAnswer = new TagBuilder("p");
            rightAnswer.InnerHtml.Append(answer);
            rightAnswer.AddCssClass("rightAnswer");

            return rightAnswer;
        }      
    }
}
