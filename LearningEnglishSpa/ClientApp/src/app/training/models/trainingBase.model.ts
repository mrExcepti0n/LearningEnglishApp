import { IQuiestion } from "./question.model";

export abstract class TrainingBase {

  public constructor(private questions: IQuiestion[]) {

  }

  private currentQuestionNumber: number = 0;


  protected _rightAnsweredQuestions: number;
  public get RightAnsweredQuestions(): number {
    return this._rightAnsweredQuestions;
  };


  getNextQuestion(): IQuiestion {
    if (this.currentQuestionNumber + 1 < this.questions.length) {
      return this.questions[this.currentQuestionNumber++];
    }
    else {
      return null;
    }
  }

  getCurrentQuestion(): IQuiestion {
    return this.questions[this.currentQuestionNumber];
  }

  getResult() {

  }


}
