import { Question } from "./question.model";
import { TrainingBase } from "../../models/trainingBase.model";

export class Training extends TrainingBase {

  constructor(questions: Question[]) {
    super(questions);
  }

  checkAnswer(answer: string) {
    let question = this.getCurrentQuestion();

    if (question.translation == answer) {
      this._rightAnsweredQuestions++;
    }
    return question.translation;
  }
}
