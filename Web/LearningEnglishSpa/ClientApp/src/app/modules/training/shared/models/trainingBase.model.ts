import { TrainingSummarizing } from "./trainingSummarizing.model";
import { TrainingService } from "../services/trainig.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { UserWord } from "../../../vocabulary/models/word.model";
import { IQuestion } from "./iquestion.model";
import { TrainingType } from "./trainingType.model";
import { TrainingSaveResult } from "./trainingSaveResult.model";

export abstract class TrainingBase {

  private questions: IQuestion[];
  public constructor(private trainingDataService: TrainingService, private trainingType: TrainingType, private _isReverse: boolean) {
   
  }

  public newGame(): Observable<IQuestion> {

    this.questions = [];
    this.currentQuestionNumber = 0;
    this._rightAnsweredQuestions = 0;
    return this.trainingDataService.getRequiringStudyWords(this.trainingType, this._isReverse)
      .pipe(
        map(words => this._isReverse ? this.reverseWords(words) : words),
        map(words => {
          this.questions = this.generateQuestions(words);
          return this.getCurrentQuestion();
      }));
  } 

  public get isReverse() {
    return this._isReverse;
  }


  reverseWords(words: UserWord[]): UserWord[] {
    words.forEach(word => { let tmp = word.word; word.word = word.translation; word.translation = tmp; });
    return words;
  }

  protected abstract generateQuestions(words: UserWord[]);

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

  private results: TrainingSummarizing = null;

  getResult(): TrainingSummarizing {
    if (!this.results) {
      const trainingResult = new TrainingSaveResult(this.trainingType, this.isReverse, this.questions);
      this.trainingDataService.saveTrainingResult(trainingResult).subscribe(res => console.log(res));
      this.results = new TrainingSummarizing(this.questions.length, this._rightAnsweredQuestions);
    }
    return this.results;
  }

  private checkAnswerInternal(answer: string): boolean {
    let question = this.getCurrentQuestion();
    return question.checkAnswer(answer);
  }

  public checkAnswer(answer: string): boolean {
    let isRight = this.checkAnswerInternal(answer);

    if (isRight) {
      this._rightAnsweredQuestions++;
    }
    return isRight;
  }


  protected shuffleWords(words: any[]) {
    for (let i = words.length - 1; i >= 1; i--) {
      let j = this.randomInteger(0, i);
      let temp = words[j];
      words[j] = words[i];
      words[i] = temp;
    }
  }

  private randomInteger(min: number, max: number): number {
    // получить случайное число от (min-0.5) до (max+0.5)
    let rand = min - 0.5 + Math.random() * (max - min + 1);
    return Math.round(rand);
  }
}
