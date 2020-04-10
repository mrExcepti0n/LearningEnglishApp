import { TrainingResult } from "./trainingResult.model";
import { TrainingDataService } from "../services/trainigData.service";
import { Subject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Word } from "../../../vocabulary/models/word.model";
import { IQuestion } from "./iquestion.model";
import { Injectable } from "@angular/core";
import { WordImageService } from "../../../shared/services/wordImage.service";
import { SafeUrl } from "@angular/platform-browser";

export abstract class TrainingBase {

  private questions: IQuestion[];
  public constructor(private trainingDataService: TrainingDataService, private _isReverse: boolean) {
   
  }

  public newGame(): Observable<IQuestion> {

    this.questions = [];
    this.currentQuestionNumber = 0;
    this._rightAnsweredQuestions = 0;
    return this.trainingDataService.getRequiringStudyWords()
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


  reverseWords(words: Word[]): Word[] {
    words.forEach(word => { let tmp = word.name; word.name = word.translation; word.translation = tmp; });
    return words;
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
