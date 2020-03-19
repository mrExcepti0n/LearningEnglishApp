import { Injectable } from "@angular/core";
import { SpeechService } from "./services/speech.service";
import { LanguageEnum } from "./models/language.enum";

@Injectable()
export class AudioPlayer {

  private _audioPlayer = new Audio();
  constructor(private speechService: SpeechService) {
  }   

  playWordAudio(word: string, language: LanguageEnum = LanguageEnum.English) {
    this.speechService.getAudioUrl(word, language).subscribe(blob => this.playWordAudioInternal(blob));
  }

  private playWordAudioInternal(audioBlob: string) {
    this._audioPlayer.src = '';
    this._audioPlayer.src = audioBlob;
    this._audioPlayer.play();
  }
}
