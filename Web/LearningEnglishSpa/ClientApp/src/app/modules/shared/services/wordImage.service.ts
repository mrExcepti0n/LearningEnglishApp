import { DataService } from "./data.service";
import { ConfigurationService } from "./configuration.service";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";


@Injectable()
export class WordImageService {

  private baseUrl: string;
  constructor(private dataService: DataService, configurationService: ConfigurationService, private domSanitizer: DomSanitizer) {
    if (configurationService.isReady) {
      this.baseUrl = configurationService.serverSettings.wordImageUrl;
    } else {
      configurationService.settingsLoaded$.subscribe(() => this.baseUrl = configurationService.serverSettings.wordImageUrl);
    }
  }

  private getImageBlob(word: string): Observable<Blob> {
    return this.dataService.getBlob(`${this.baseUrl}/${word}`);    
  }

  public getImageUrl(word: string): Observable<string> {
    return this.getImageBlob(word).pipe(map(blob => URL.createObjectURL(blob)));
  }


  private getThumbnailBlob(word: string): Observable<Blob> {
    return this.dataService.getBlob(`${this.baseUrl}/${word}/thumbnail`);
  }

  public getThumbnailUrl(word: string): Observable<string> {
    return this.getThumbnailBlob(word).pipe(map(blob => URL.createObjectURL(blob)));
  }


  public getImageSaveUrl(word: string): Observable<SafeUrl> {
    return this.getImageUrl(word).pipe(map(url => this.domSanitizer.bypassSecurityTrustUrl(url)));
  }

  private getBase64String(blob: Blob) {
    var reader = new FileReader();
    reader.readAsDataURL(blob);
    reader.onloadend = function () {
      var base64data = reader.result;
      console.log(base64data);
    }
  }
}
