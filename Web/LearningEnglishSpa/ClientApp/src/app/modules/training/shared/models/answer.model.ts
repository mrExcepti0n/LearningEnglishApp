import { Question } from "../../shared/models/question.model";

export class Answer {
  constructor(question: Question, translation: string) {
    this.translation = translation;
    this.userSelect = false;
    this.question = question;
  }

  translation: string;

  get isRight(): boolean {
    return this.question.translation == this.translation;
  }

  userSelect: boolean;
  question: Question;
}
