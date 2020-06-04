import { ConfigurationService } from "../../shared/services/configuration.service";
import { TrainingService } from "../shared/services/trainig.service";
import { ActivatedRoute } from "@angular/router";
import { CollectWordTraining } from "./collectWordTraining.model";
import { TrainingComponentBase } from "../shared/components/trainingComponentBase";
import { Component } from "@angular/core";
import { CollectWordQuestion } from "./collectWordQuestion.model";
import { WordImageService } from "../../shared/services/wordImage.service";
import { AudioPlayer } from "../../shared/audioPlayer";

@Component({
  templateUrl: 'collectWordTraining.component.html'
})
export class CollectWordTrainingComponent extends TrainingComponentBase<CollectWordTraining, CollectWordQuestion>
{
  protected loadDataInternal() {
    this.training = new CollectWordTraining(this.trainingDataService, this.isReverse);
  }


  constructor(configurationService: ConfigurationService, trainingDataService: TrainingService, wordImageService: WordImageService, audioPlayer: AudioPlayer, route: ActivatedRoute) {
    super(configurationService, trainingDataService, wordImageService, audioPlayer, route);
  }  


  public putLetter(letterIndex: number) {

    let letter = this.currentQuestion.letters[letterIndex];
    this.currentQuestion.userLetters.push(letter);
    this.currentQuestion.letters.splice(letterIndex, 1);

    if (this.currentQuestion.letters.length <= 0) {
      this.checkAnswer(this.currentQuestion.userLetters.reduce((pv, cv) => pv + cv, ''));
    }
  }

  public removeLetter(letterIndex: number) {
    let letter = this.currentQuestion.userAnswer[letterIndex];
    this.currentQuestion.letters.push(letter);
    this.currentQuestion.userLetters.splice(letterIndex, 1);
  }
}

