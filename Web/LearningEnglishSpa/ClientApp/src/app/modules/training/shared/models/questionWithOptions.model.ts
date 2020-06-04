import { Question } from "./question.model";

export class QuestionWithOptions extends Question {
  public options: string[];
}
