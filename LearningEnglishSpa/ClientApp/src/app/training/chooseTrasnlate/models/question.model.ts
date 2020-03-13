import { IQuiestion } from "../../models/question.model";
import { Answer } from "./answer.model";

export class Question implements IQuiestion {

  answers: Answer[];
  number: string;
  word: string;
  get translation(): string {
    return this.answers.find(a => a.isRight).translation;
  };


}
