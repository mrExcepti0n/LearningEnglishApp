import { Component, OnInit } from "@angular/core";
import { VocabularyService } from "./vocabulary.service";
import { ConfigurationService } from "../modules/shared/services/configuration.service";
import { Word } from "./models/word.model";

@Component({
  templateUrl: "vocabulary.component.html"
})
export class VocabularyComponent implements OnInit {
  words: Word[];

  wordMask: string = '';

  wordMaskTranslation: string[] = [];
  hideTranslationArea: boolean = true;


  constructor(private service: VocabularyService, private configurationService: ConfigurationService) {
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
    this.service.getUserWords().subscribe(result => { this.words = result });
  }

  addWord(word, translation) {
    this.onClickedOutside();
    this.service.addWord(word, translation).subscribe(res => { this.wordMask = ''; this.getWords() });
  }

  getTranslations() {
    this.service.getTranslations(this.wordMask).subscribe(res => { this.wordMaskTranslation = res; this.hideTranslationArea = false; });
  }

  onClickedOutside() {
    this.hideTranslationArea = true;
    this.wordMaskTranslation = [];
  }
}
