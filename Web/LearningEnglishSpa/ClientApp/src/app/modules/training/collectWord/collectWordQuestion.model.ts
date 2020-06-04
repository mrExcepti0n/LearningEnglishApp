import { Question } from "../shared/models/question.model";

export class CollectWordQuestion extends Question {
  letters: string[];
  userLetters: string[] = [];
}
