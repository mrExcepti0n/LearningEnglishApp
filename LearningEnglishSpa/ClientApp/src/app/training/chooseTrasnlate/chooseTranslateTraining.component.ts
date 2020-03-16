import { Component } from "@angular/core";
import { Training } from "./models/training.model";
import { Question } from "./models/question.model";
import { Answer } from "./models/answer.model";
import { TrainingDataService } from "../services/trainigData.service";

@Component({
  templateUrl: 'chooseTranslateTraining.component.html'
})
export class ChooseTranslateTrainingComponent {

  public training: Training;

  public showAnswer: boolean = false;

  constructor(private _trainingDataService: TrainingDataService) {
    this.training = new Training(this._trainingDataService);
    this.newGame();
  }

  public currentQuestion: Question;

  public checkAnswer(answer?: string) {
    this.training.checkAnswer(answer);
    this.showAnswer = true;
  }

  public nextQuestion() {
    this.showAnswer = false;
    this.currentQuestion = this.training.getNextQuestion() as Question;
  }


  public newGame() {
    this.training.newGame().subscribe(res => this.currentQuestion = res as Question);
  }
}
