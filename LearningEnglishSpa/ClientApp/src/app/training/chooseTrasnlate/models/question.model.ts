import { IQuestion } from "../../models/question.model";
import { Answer } from "./answer.model";

export class Question implements IQuestion {


  answers: Answer[];
  number: string;
  word: string ;
  skippedQuestion: boolean = false;
  get translation(): string {
    return this.answers.find(a => a.isRight).translation;
  };

  checkAnswer(userAnswer: string) {
    if (userAnswer === null) {
      this.skippedQuestion = true;
    }
    else
    {
      let answer: Answer = this.answers.find(a => a.translation == userAnswer);
      if (answer) {
        answer.userSelect = true;
        return answer.isRight;
      }
    }
    return false;
  }

}
