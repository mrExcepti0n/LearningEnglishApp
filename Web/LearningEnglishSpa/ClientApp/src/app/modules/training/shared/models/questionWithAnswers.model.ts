import { Question } from "./question.model";
import { Answer } from "./answer.model";

export class QuestionWithAnswers extends Question {

  public answers: Answer[];

  checkAnswer(userAnswer: string) {
    if (userAnswer === null) {
      this.skippedQuestion = true;
    }
    else {
      let answer: Answer = this.answers.find(a => a.translation == userAnswer);
      if (answer) {
        answer.userSelect = true;
        return answer.isRight;
      }
    }
    return false;
  }
}
