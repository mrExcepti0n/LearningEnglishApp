export class TrainingSummarizing {
  constructor(totalQuestion: number, rightAnswers: number) {
    this.totalQuestions = totalQuestion;
    this.rightAnswers = rightAnswers;
  }
  public totalQuestions: number;
  public rightAnswers: number;   
  summary = (): string => this.rightAnswers / this.totalQuestions > 0.6 ? 'Хорошо' : 'Плохо';
}
