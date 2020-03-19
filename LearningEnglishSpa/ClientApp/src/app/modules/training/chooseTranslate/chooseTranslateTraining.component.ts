import { Component, Input, OnInit } from "@angular/core";
import { TrainingDataService } from "../shared/services/trainigData.service";
import { ActivatedRoute } from "@angular/router";
import { ConfigurationService } from "../../shared/services/configuration.service";
import { ChooseTranslateTraining } from "./models/chooseTranslateTraining.model";
import { QuestionWithAnswers } from "../shared/models/questionWithAnswers.model";
import { TrainingComponentBase } from "../shared/components/trainingComponentBase";
import { WordImageService } from "../../shared/services/wordImage.service";
import { SpeechService } from "../../shared/services/speech.service";
import { AudioPlayer } from "../../shared/audioPlayer";

@Component({
  templateUrl: 'chooseTranslateTraining.component.html'
})
export class ChooseTranslateTrainingComponent extends TrainingComponentBase<ChooseTranslateTraining, QuestionWithAnswers>
{

  protected loadDataInternal() {
    this.training = new ChooseTranslateTraining(this.trainingDataService, this.isReverse);
  }
  public showAnswer: boolean = false;

  constructor(configurationService: ConfigurationService, trainingDataService: TrainingDataService, wordImageService: WordImageService, audioPlayer: AudioPlayer, route: ActivatedRoute) {
    super(configurationService, trainingDataService, wordImageService, audioPlayer, route);
  }

}
