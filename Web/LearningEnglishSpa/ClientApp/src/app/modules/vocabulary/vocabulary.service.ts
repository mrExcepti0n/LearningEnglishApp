import { Injectable } from "@angular/core";
import { UserWord } from "./models/word.model";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { DataService } from "../shared/services/data.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { Vocabulary } from "./models/vocabulary.model";

@Injectable()
export class VocabularyService {

  private vocabularyUrl: string = '';
  words: UserWord[];

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


  getVocabularies(): Observable<Vocabulary[]> {
    let url = this.vocabularyUrl;
    return this.service.get(url).pipe<Vocabulary[]>(tap((response: any) => response));
  }

  getVocabulary(id: number): Observable<Vocabulary> {
    let url = `${this.vocabularyUrl}/${id}`;
    return this.service.get(url).pipe<Vocabulary>(tap((response: any) => response));
  }


  getUserWords(userVocabularyId?: number, mask?: string): Observable<UserWord[]> {
    let url = this.vocabularyUrl + '/words';
    let params : string[] = [];
    if (userVocabularyId) {
      params.push('vocabularyId=' + userVocabularyId);
    }
    if (mask) {
      params.push('mask=' + mask);
    }
    if (params.length > 0) {
      url += '?' + params.join('&');
    }

    return this.service.get(url).pipe<UserWord[]>(tap((response: any) => {
      return response;
    }));
  }

  addWord(word: string, translation: string, userVocabularyId?: number): Observable<Response> {
    let url = this.vocabularyUrl + '/words';
    return this.service.post(url, { Word: word, Translation: translation, UserVocabularyId : userVocabularyId });
  }

  getTranslations(word: string): Observable<string[]> {
    let url = `${this.vocabularyUrl}/${word}/translations`;
    return this.service.get(url).pipe<string[]>(tap((response: any) => {
      console.log(response);
      return response;
    }));
  }

}
