using LearningEnglishWeb.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Helpers
{
    public static class HtmlQuestionResultHelper
    {
        public static HtmlString CreateTranslateWordTrainingAnswerResult(this IHtmlHelper html, List<AnswerResult> answerResults)
        {
            var area = new TagBuilder("div");         

            foreach (var answer in answerResults)
            {
                var p = new TagBuilder("p");
                p.InnerHtml.Append(answer.Word);
                area.InnerHtml.AppendHtml(p);
            }

            var button = new TagBuilder("button");
            button.InnerHtml.Append("Следующий вопрос");
            button.AddCssClass("train-btn");

            if (answerResults.All(ar => ar.UserSelect == ar.Right))
            {
                button.Attributes.Add(new KeyValuePair<string, string>("style", "background-color: green;"));
            } else
            {
                button.Attributes.Add(new KeyValuePair<string, string>("style", "background-color: red;"));
            }

            area.InnerHtml.AppendHtml(button);

            var writer = new StringWriter();
            area.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString()); 
        }

    }
}
