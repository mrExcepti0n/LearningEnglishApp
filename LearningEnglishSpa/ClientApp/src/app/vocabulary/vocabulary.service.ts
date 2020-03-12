import { Injectable } from "@angular/core";
import { Word } from "./models/word.model";
import { Observable } from "rxjs";
import { DataService } from "../modules/shared/services/data.service";
import { ConfigurationService } from "../modules/shared/services/configuration.service";
import { tap } from "rxjs/operators";

@Injectable()
export class VocabularyService {

  private vocabularyUrl: string = '';
  words: Word[];

  constructor(private service: DataService, private configurationService: ConfigurationService) {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.vocabularyUrl = this.configurationService.serverSettings.vocabularyUrl;
      });
  }

  getUserWords(): Observable<Word[]> {
    return this.service.get(this.vocabularyUrl).pipe<Word[]>(tap((response: any) => {
      return response;
    }));
  }

  getTranslations(word: string): Observable<string[]> {
    let url = `${this.vocabularyUrl}/${word}/translations`;
    return this.service.get(url).pipe<string[]>(tap((response: any) => {
      console.log(response);
      return response;
    }));
  }

}
