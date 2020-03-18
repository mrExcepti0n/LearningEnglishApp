import { Injectable } from "@angular/core";
import { Word } from "./models/word.model";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { DataService } from "../shared/services/data.service";
import { ConfigurationService } from "../shared/services/configuration.service";

@Injectable()
export class VocabularyService {

  private vocabularyUrl: string = '';
  words: Word[];

  constructor(private service: DataService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady) {
      this.vocabularyUrl = this.configurationService.serverSettings.vocabularyUrl;
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.vocabularyUrl = this.configurationService.serverSettings.vocabularyUrl;
      });
    }
  }

  getUserWords(): Observable<Word[]> {
    return this.service.get(this.vocabularyUrl).pipe<Word[]>(tap((response: any) => {
      return response;
    }));
  }

  addWord(word: string, translation: string): Observable<Response> {
    return this.service.post(this.vocabularyUrl, { Name: word, Translation: translation });
  }

  getTranslations(word: string): Observable<string[]> {
    let url = `${this.vocabularyUrl}/${word}/translations`;
    return this.service.get(url).pipe<string[]>(tap((response: any) => {
      console.log(response);
      return response;
    }));
  }

}
