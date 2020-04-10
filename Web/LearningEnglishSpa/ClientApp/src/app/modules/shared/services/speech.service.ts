import { DataService } from "./data.service";
import { ConfigurationService } from "./configuration.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { LanguageEnum } from "../models/language.enum";


@Injectable()
export class SpeechService {
  private baseUrl: string;

  constructor(private dataService: DataService, configurationService: ConfigurationService) {
    if (configurationService.isReady) {
      this.baseUrl = configurationService.serverSettings.speechUrl;
    } else {
      configurationService.settingsLoaded$.subscribe(() => this.baseUrl = configurationService.serverSettings.speechUrl);
    }
  }

  private getAudioBlob(word: string, languageEnum: LanguageEnum): Observable<Blob> {
    return this.dataService.getBlobWithoutAuth(`${this.baseUrl}/${word}?language=${languageEnum}`);
  }

  public getAudioUrl(word: string, languageEnum: LanguageEnum = LanguageEnum.English): Observable<string> {
    return this.getAudioBlob(word, languageEnum).pipe(map(blob => URL.createObjectURL(blob)));
  }
}
