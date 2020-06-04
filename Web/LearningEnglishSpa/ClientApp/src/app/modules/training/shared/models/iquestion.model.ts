export interface IQuestion {
  userWordId: number;
  number: string;
  word: string;
  translation: string;

  checkAnswer(answer: string): boolean;
  isRightAnswer(): boolean;
  userAnswer: string;
}
