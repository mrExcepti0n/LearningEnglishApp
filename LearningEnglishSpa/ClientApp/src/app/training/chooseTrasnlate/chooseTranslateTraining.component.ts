import { Component } from "@angular/core";
import { Training } from "./models/training.model";
import { Question } from "./models/question.model";
import { Answer } from "./models/answer.model";

@Component({
  templateUrl: 'chooseTranslateTraining.component.html'
})
export class ChooseTranslateTrainingComponent {

  private training: Training;

  constructor() {

    let firstQuestion = new Question();
    firstQuestion.number = "1";
    firstQuestion.word = "dog";
    firstQuestion.answers = [new Answer("девочка", false), new Answer("собака", true), new Answer("парень", false), new Answer("кошка", false)];

    let secondQuestion = new Question();
    firstQuestion.number = "2";
    firstQuestion.word = "cat";
    firstQuestion.answers = [new Answer("девочка", false), new Answer("собака", false), new Answer("парень", false), new Answer("кошка", true)];

    this.training = new Training([firstQuestion, secondQuestion])
  }

  public currentQuestion = () => this.training.getCurrentQuestion() as Question; 

}
