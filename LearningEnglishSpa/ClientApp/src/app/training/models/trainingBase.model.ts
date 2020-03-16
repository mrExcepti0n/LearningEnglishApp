import { IQuestion } from "./question.model";
import { TrainingResult } from "./trainingResult.model";
import { Word } from "../../vocabulary/models/word.model";
import { TrainingDataService } from "../services/trainigData.service";
import { Subject, Observable } from "rxjs";
import { map } from "rxjs/operators";

export abstract class TrainingBase {

  private questions: IQuestion[];
  public constructor(private trainingDataService: TrainingDataService) {
   
  }

  public newGame(): Observable<IQuestion> {

    this.questions = [];
    this.currentQuestionNumber = 0;
    this._rightAnsweredQuestions = 0;
    return this.trainingDataService.getRequiringStudyWords()
      .pipe(map(words => {
        this.questions = this.generateQuestions(words);
        return this.getCurrentQuestion();
      }));
  } 

  protected abstract generateQuestions(words: Word[]);

  private currentQuestionNumber: number = 0;

  protected _rightAnsweredQuestions: number = 0;
  public get RightAnsweredQuestions(): number {
    return this._rightAnsweredQuestions;
  };

  getNextQuestion(): IQuestion {
    if (this.currentQuestionNumber + 1 < this.questions.length) {
      return this.questions[++this.currentQuestionNumber];
    }
    else {
      return null;
    }
  }

  getCurrentQuestion(): IQuestion {
    return this.questions[this.currentQuestionNumber];
  }

  getResult(): TrainingResult {
    return new TrainingResult(this.questions.length, this._rightAnsweredQuestions);
  }
 
  protected abstract checkAnswerInternal(answer: string): boolean;

  public checkAnswer(answer: string): boolean {
    var isRight = this.checkAnswerInternal(answer);

    if (isRight) {
      this._rightAnsweredQuestions++;
    }
    return isRight;
  }
}
