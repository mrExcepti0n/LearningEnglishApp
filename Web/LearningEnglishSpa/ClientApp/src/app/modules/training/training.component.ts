import { Component } from "@angular/core";
import { TrainingService } from "./shared/services/trainig.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { TrainingAvailableWords } from "./shared/models/trainingAvailableWords.model";
import { TrainingType } from "./shared/models/trainingType.model";

@Component({
  templateUrl: 'training.component.html',
  styleUrls: ['training.component.css']
})
export class TrainingComponent {

  private trainingAvailableWords: TrainingAvailableWords[] = [];

  constructor(private trainingService: TrainingService, private configurationService: ConfigurationService) {  
  }

  ngOnInit() {
    if (this.configurationService.isReady) {
      this.loadData();
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(() => this.loadData());
    }
  }

  private loadData() {
    this.trainingService.getAvailibleTrainingWordsCount().subscribe(res => this.trainingAvailableWords = res);
  }

  private GetTrainingInfo(trainingType: TrainingType, isReverse: boolean): TrainingAvailableWords {
    let aw = this.trainingAvailableWords.find(el => el.trainingType === trainingType && el.isReverseTraining === isReverse);
    return aw;
  }

  public get trainingInfoTW() {
    return this.GetTrainingInfo(TrainingType.TranslateWord, false);
  }

  public get trainingInfoReverseTW() {
    return this.GetTrainingInfo(TrainingType.TranslateWord, true);
  }

  public get trainingInfoCT() {
    return this.GetTrainingInfo(TrainingType.ChooseTranslate, false);
  }

  public get trainingInfoReverseCT() {
    return this.GetTrainingInfo(TrainingType.ChooseTranslate, true);
  }

  public get trainingInfoCW() {
    return this.GetTrainingInfo(TrainingType.CollectWord, false);
  }

  public get trainingInfoReverseCW() {
    return this.GetTrainingInfo(TrainingType.CollectWord, true);
  }
}
