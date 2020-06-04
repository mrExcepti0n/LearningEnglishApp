import { IQuestion } from "./iquestion.model";

export class Question implements IQuestion {
  userWordId: number;
  number: string;
  word: string;
  translation: string;


  checkAnswer(answer: string): boolean {
    this.userAnswer = answer;
    return this.isRightAnswer();
  }
  isRightAnswer(): boolean {
    return this.userAnswer === this.translation;
  }
  userAnswer: string;
}
