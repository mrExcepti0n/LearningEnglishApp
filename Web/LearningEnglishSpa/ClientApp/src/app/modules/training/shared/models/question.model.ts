import { IQuestion } from "./iquestion.model";

export class Question implements IQuestion {
  number: string;
  word: string;
  translation: string;
  skippedQuestion: boolean;
}
