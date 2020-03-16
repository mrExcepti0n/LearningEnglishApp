import { Component, Input, Output, EventEmitter } from "@angular/core";
import { TrainingResult } from "../models/trainingResult.model";

@Component({
  selector: 'app-training-result',
  templateUrl: 'trainingResult.component.html'
})
export class TrainingResultComponent {
  constructor() {

  }

  @Input()
  results: TrainingResult;
  @Output()
  newGameEvent = new EventEmitter<boolean>();

  newGame() {
    this.newGameEvent.emit(true);
  }
}
