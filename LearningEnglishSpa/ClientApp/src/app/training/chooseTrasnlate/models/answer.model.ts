import { Question } from "./question.model";

export class Answer {
  constructor(question: Question, translation: string, isRight: boolean) {
    this.translation = translation;
    this.isRight = isRight;
    this.userSelect = false;
    this.question = question;
  }

  translation: string;
  isRight: boolean;
  userSelect: boolean;
  question: Question;
}
