import { Injectable } from "@angular/core";
import { DataService } from "../../../shared/services/data.service";
import { ConfigurationService } from "../../../shared/services/configuration.service";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { UserWord } from "../../../vocabulary/models/word.model";

@Injectable()
export class TrainingDataService {

  private serviceUrl: string = '';

  constructor(private service: DataService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady) {
      this.serviceUrl = this.configurationService.serverSettings.vocabularyUrl;
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.serviceUrl = this.configurationService.serverSettings.vocabularyUrl;
      });
    }
  }


  public getRequiringStudyWords(): Observable<UserWord[]> {

    return this.service.get(this.serviceUrl + '/RequiringStudyWords').pipe<UserWord[]>(tap((response: any) => {
      return response;
    }));
    /*return [{ name: 'dog', translation: "собака" }, { name: 'cat', translation: "кошка" }, { name: 'girl', translation: "девушка" },
    { name: 'man', translation: "мужчина" }, { name: 'fox', translation: "лиса" }];*/
  }
}
