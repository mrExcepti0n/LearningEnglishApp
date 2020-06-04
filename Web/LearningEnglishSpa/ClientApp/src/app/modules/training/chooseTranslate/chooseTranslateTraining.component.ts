import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ConfigurationService } from "../../shared/services/configuration.service";
import { ChooseTranslateTraining } from "./models/chooseTranslateTraining.model";
import { TrainingComponentBase } from "../shared/components/trainingComponentBase";
import { WordImageService } from "../../shared/services/wordImage.service";
import { AudioPlayer } from "../../shared/audioPlayer";
import { QuestionWithOptions } from "../shared/models/questionWithOptions.model";
import { TrainingService } from "../shared/services/trainig.service";

@Component({
  templateUrl: 'chooseTranslateTraining.component.html'
})
export class ChooseTranslateTrainingComponent extends TrainingComponentBase<ChooseTranslateTraining, QuestionWithOptions>
{

  protected loadDataInternal() {
    this.training = new ChooseTranslateTraining(this.trainingDataService, this.isReverse);
  }


  constructor(configurationService: ConfigurationService, trainingDataService: TrainingService, wordImageService: WordImageService, audioPlayer: AudioPlayer, route: ActivatedRoute) {
    super(configurationService, trainingDataService, wordImageService, audioPlayer, route);
  }

}
