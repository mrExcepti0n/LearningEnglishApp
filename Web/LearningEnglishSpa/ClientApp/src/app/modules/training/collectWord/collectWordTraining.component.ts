import { ConfigurationService } from "../../shared/services/configuration.service";
import { TrainingDataService } from "../shared/services/trainigData.service";
import { ActivatedRoute } from "@angular/router";
import { CollectWordTraining } from "./collectWordTraining.model";
import { Question } from "../shared/models/question.model";
import { TrainingComponentBase } from "../shared/components/trainingComponentBase";
import { Component } from "@angular/core";
import { CollectWordQuestion } from "./collectWordQuestion.model";
import { WordImageService } from "../../shared/services/wordImage.service";
import { SpeechService } from "../../shared/services/speech.service";
import { AudioPlayer } from "../../shared/audioPlayer";

@Component({
  templateUrl: 'collectWordTraining.component.html'
})
export class CollectWordTrainingComponent extends TrainingComponentBase<CollectWordTraining, CollectWordQuestion>
{
  


  protected loadDataInternal() {
    this.training = new CollectWordTraining(this.trainingDataService, this.isReverse);
  }

  public showAnswer: boolean = false;


  constructor(configurationService: ConfigurationService, trainingDataService: TrainingDataService, wordImageService: WordImageService, audioPlayer: AudioPlayer, route: ActivatedRoute) {
    super(configurationService, trainingDataService, wordImageService, audioPlayer, route);
  }  


  public putLetter(letterIndex: number) {

    let letter = this.currentQuestion.letters[letterIndex];
    this.currentQuestion.userAnswer.push(letter);
    this.currentQuestion.letters.splice(letterIndex, 1);

    if (this.currentQuestion.letters.length <= 0) {
      this.checkAnswer(this.currentQuestion.userAnswer.reduce((pv, cv) => pv + cv, ''));
    }
  }

  public removeLetter(letterIndex: number) {
    let letter = this.currentQuestion.userAnswer[letterIndex];
    this.currentQuestion.letters.push(letter);
    this.currentQuestion.userAnswer.splice(letterIndex, 1);
  }
}

