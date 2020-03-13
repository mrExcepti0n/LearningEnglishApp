export class Answer {
  constructor(translation: string, isRight: boolean) {
    this.translation = translation;
    this.isRight = isRight;
  }

  translation: string;
  isRight: boolean;
}
