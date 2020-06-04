import { Component, OnInit, Input } from "@angular/core";
import { VocabularyService } from "./vocabulary.service";
import { UserWord } from "./models/word.model";
import { ConfigurationService } from "../shared/services/configuration.service";
import { AudioPlayer } from "../shared/audioPlayer";
import { LanguageEnum } from "../shared/models/language.enum";
import { TrainingService } from "../training/shared/services/trainig.service";
import { TrainingWordRatio } from "../training/shared/models/trainingWordRatio.model";

@Component({
  templateUrl: 'userWords.component.html',
  selector: 'app-user-words',
  styleUrls: ['userWords.component.css']
})
export class UserWordsComponent implements OnInit {


  @Input()
  userVocabularyId?: number = null;

  words: UserWord[];
  wordMask: string = '';
  wordMaskTranslation: string[] = [];
  hideTranslationArea: boolean = true;

  constructor(private vocabularyService: VocabularyService, private trainingService: TrainingService, private configurationService: ConfigurationService, private audioPlayer: AudioPlayer) {
  }

  ngOnInit() {
    if (this.configurationService.isReady) {
      this.loadData();
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(() => this.loadData());
    }
  }

  loadData() {
    this.getWords();
  }

  getWords() {
    this.vocabularyService.getUserWords(this.userVocabularyId).subscribe(result => { this.words = result; this.loadTrainingRatio() });
  }

  addWord(word, translation) {
    this.onClickedOutside();
    this.vocabularyService.addWord(word, translation, this.userVocabularyId).subscribe(res => { this.wordMask = ''; this.getWords() });
  }

  getTranslations() {
    this.vocabularyService.getTranslations(this.wordMask).subscribe(res => { this.wordMaskTranslation = res; this.hideTranslationArea = false; });
  }

  onClickedOutside() {
    this.hideTranslationArea = true;
    this.wordMaskTranslation = [];
  }


  playWordAudio(word: string) {
    this.audioPlayer.playWordAudio(word, LanguageEnum.English);
  }

  playTranslationAudio(word: string) {
    this.audioPlayer.playWordAudio(word, LanguageEnum.Russian);
  }

  loadTrainingRatio() {
    let userWordsId = this.words.map(uw => uw.id);
    this.trainingService.getTrainingWordsRatio(userWordsId).subscribe(res => this.fillTrainingRatio(res));
  }

  fillTrainingRatio(wordsRatio: TrainingWordRatio[]) {
    this.words.forEach(w => this.setTrainingRatio(w, wordsRatio.find(wr => wr.userWordId === w.id).trainingRatio));   
  }

  private setTrainingRatio(word: UserWord, ratio: number) {
    if (ratio < 8) {
      word.trainingRatio = 8;
    } else {
      word.trainingRatio = ratio;
    }
  }
}
