import { Injectable } from "@angular/core";
import { DataService } from "../../../shared/services/data.service";
import { ConfigurationService } from "../../../shared/services/configuration.service";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { UserWord } from "../../../vocabulary/models/word.model";
import { TrainingType } from "../models/trainingType.model";
import { TrainingSaveResult } from "../models/trainingSaveResult.model";
import { TrainingWordRatio } from "../models/trainingWordRatio.model";
import { TrainingAvailableWords } from "../models/trainingAvailableWords.model";

@Injectable()
export class TrainingService {

  private serviceUrl: string = '';

  constructor(private service: DataService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady) {
      this.serviceUrl = this.configurationService.serverSettings.trainingUrl;
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.serviceUrl = this.configurationService.serverSettings.trainingUrl;
      });
    }
  }


  public getRequiringStudyWords(trainingType: TrainingType, isReverseTraining: boolean): Observable<UserWord[]> {
    let url = `${this.serviceUrl}/RequiringStudyWords?trainingType=${trainingType}&isReverseTraining=${isReverseTraining}`;
    return this.service.get(url).pipe<UserWord[]>(tap((response: any) => {
      return response;
    }));
  }


  public getTrainingWords(userSelectedWords: number[]): Observable<UserWord[]> {
    let parameters = userSelectedWords.map(el => 'userSelectedWords=' + el).join('&');
    let url = `${this.serviceUrl}/TrainingWords?${parameters}"`;
    return this.service.getResult<UserWord[]>(url);
  }


  public saveTrainingResult(trainingResult: TrainingSaveResult): Observable<Response>{
    let url = this.serviceUrl;
    return this.service.post(url, trainingResult);
  }

  public getTrainingWordsRatio(userWordsId: number[]): Observable<TrainingWordRatio[]> {
    let parameters = userWordsId.map(el => 'userWordIds=' + el).join('&');
    let url = `${this.serviceUrl}/TrainingWordsRatio?${parameters}`;
    return this.service.getResult<TrainingWordRatio[]>(url);
  }

  public getAvailibleTrainingWordsCount(): Observable<TrainingAvailableWords[]> {
    let url = `${this.serviceUrl}/AvailibleTrainingWordsCount`;
    return this.service.get(url).pipe<TrainingAvailableWords[]>(tap((response: any) => {
      return response;
    }));
  }
}
