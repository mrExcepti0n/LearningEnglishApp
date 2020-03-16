import { Question } from "./question.model";
import { TrainingBase } from "../../models/trainingBase.model";
import { TrainingDataService } from "../../services/trainigData.service";
import { Word } from "../../../vocabulary/models/word.model";
import { IQuestion } from "../../models/question.model";
import { Answer } from "./answer.model";

export class Training extends TrainingBase {

  protected checkAnswerInternal(answer: string): boolean {
    let question = this.getCurrentQuestion() as Question;
    return question.checkAnswer(answer);
  }

  constructor(trainingDataService: TrainingDataService) { 
    super(trainingDataService);
  }

  protected generateQuestions(words: Word[]): IQuestion[] {

    let results: IQuestion[] = [];
    for (let i = 0; i < words.length; i++) {

      let question = new Question();
      question.number = (i + 1).toString();
      question.word = words[i].name;

      let answer = this.getAnswers(question, words, i, 4);
      
      question.answers = answer;
      results.push(question);
    }
    return results;
  }


  protected getAnswers(question: Question, words: Word[], currentIndex: number, size: number): Answer[] {

    let rightAnswer = new Answer(question, words[currentIndex].translation, true);
    let otherWords: Word[] = words.filter((w, i) => i != currentIndex);
    this.shuffleWords(otherWords);
    let answers: Answer[] = otherWords.slice(0, size).map(w => new Answer(question, w.translation, false));
    answers.push(rightAnswer);
    this.shuffleWords(answers);
    return answers;
  }

  protected shuffleWords(words: any[]) {
    for (let i = words.length - 1; i >= 1; i--) {
      let j = this.randomInteger(0, i);
      let temp = words[j];
      words[j] = words[i];
      words[i] = temp;
    }
  }

  protected randomInteger(min: number, max: number): number {
    // получить случайное число от (min-0.5) до (max+0.5)
    let rand = min - 0.5 + Math.random() * (max - min + 1);
    return Math.round(rand);
   }
}
