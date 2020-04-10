import { Word } from "../../../vocabulary/models/word.model";
import { TrainingBase } from "../../shared/models/trainingBase.model";
import { TrainingDataService } from "../../shared/services/trainigData.service";
import { IQuestion } from "../../shared/models/iquestion.model";
import { QuestionWithAnswers } from "../../shared/models/questionWithAnswers.model";
import { Answer } from "../../shared/models/answer.model";
import { WordImageService } from "../../../shared/services/wordImage.service";

export class ChooseTranslateTraining extends TrainingBase {

  protected checkAnswerInternal(answer: string): boolean {
    let question = this.getCurrentQuestion() as QuestionWithAnswers;
    return question.checkAnswer(answer);
  }

  constructor(trainingDataService: TrainingDataService, isReverse: boolean) { 
    super(trainingDataService, isReverse);
  }

  protected generateQuestions(words: Word[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new QuestionWithAnswers();
      question.number = (i + 1).toString();
      question.word = words[i].name;
      question.translation = words[i].translation;

      let answer = this.getAnswers(question, words, i, 4);      
      question.answers = answer;
      results.push(question);
    }
    return results;
  }


  protected getAnswers(question: QuestionWithAnswers, words: Word[], currentIndex: number, size: number): Answer[] {

    let rightAnswer = new Answer(question, words[currentIndex].translation);
    let otherWords: Word[] = words.filter((w, i) => i != currentIndex);
    this.shuffleWords(otherWords);
    let answers: Answer[] = otherWords.slice(0, size).map(w => new Answer(question, w.translation));
    answers.push(rightAnswer);
    this.shuffleWords(answers);
    return answers;
  } 
}
