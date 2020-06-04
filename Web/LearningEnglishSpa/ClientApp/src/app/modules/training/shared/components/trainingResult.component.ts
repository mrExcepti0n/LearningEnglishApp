import { Component, Input, Output, EventEmitter } from "@angular/core";
import { TrainingSummarizing } from "../models/trainingSummarizing.model";

@Component({
  selector: 'app-training-result',
  templateUrl: 'trainingResult.component.html'
})
export class TrainingResultComponent {
  constructor() {

  }

  @Input()
  results: TrainingSummarizing;


  @Output()
  newGameEvent = new EventEmitter<boolean>();

  newGame() {
    this.newGameEvent.emit(true);
  }
}
