import { OnInit } from "@angular/core";
import { ConfigurationService } from "../../../shared/services/configuration.service";
import { TrainingDataService } from "../services/trainigData.service";
import { ActivatedRoute } from "@angular/router";
import { TrainingBase } from "../models/trainingBase.model";
import { IQuestion } from "../models/iquestion.model";
import { WordImageService } from "../../../shared/services/wordImage.service";
import { SafeUrl } from "@angular/platform-browser";
import { SpeechService } from "../../../shared/services/speech.service";
import { AudioPlayer } from "../../../shared/audioPlayer";
import { LanguageEnum } from "../../../shared/models/language.enum";

export abstract class TrainingComponentBase<T extends TrainingBase, Q extends IQuestion> implements OnInit  {


  protected isReverse: boolean = true;
  protected training: T;
  public showAnswer: boolean = false;




  public currentQuestion: Q = null;

  private _currentQuestionUrl: SafeUrl = null;


  get currentWordImageUrl(): SafeUrl {
    return this._currentQuestionUrl;
  }


  get wordImageStyles() {
    if (!this.isReverse && !this.showAnswer) {
      return {'filter' : 'blur(8px)'};
    }
    return {};
  }


  constructor(private configurationService: ConfigurationService, protected trainingDataService: TrainingDataService,
    protected wordImageService: WordImageService, private audioPlayer: AudioPlayer, route: ActivatedRoute) {
    route.data.subscribe(data => this.isReverse = data.isReverse);
  }


  ngOnInit(): void {
    if (this.configurationService.isReady) {
      this.loadData();
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(() => this.loadData());
    }
  }

  loadData() {
    this.loadDataInternal();
    this.newGame();
  }

  protected abstract loadDataInternal();


  public newGame() {
    this.training.newGame().subscribe(res => { this.currentQuestion = res as Q; this.loadImageSrc(); });
  }

  private loadImageSrc() {
    this._currentQuestionUrl = null;
    if (this.currentQuestion != null) {
      this.wordImageService.getImageSaveUrl(this.isReverse ? this.currentQuestion.translation : this.currentQuestion.word).subscribe(res => this._currentQuestionUrl = res);
    }
  }


  public checkAnswer(answer?: string) {
    this.training.checkAnswer(answer);
    this.showAnswer = true;
  }

  public nextQuestion() {
    this.showAnswer = false;
    this.currentQuestion = this.training.getNextQuestion() as Q;
    this.loadImageSrc();
  }


  playWordAudio() {
    let currentWord: string;
    let language: LanguageEnum;

    if (this.training.isReverse && this.showAnswer) {
      currentWord = this.currentQuestion.translation;
      language = LanguageEnum.English;
    } else {
      currentWord = this.currentQuestion.word;
      language = this.training.isReverse ? LanguageEnum.Russian : LanguageEnum.English
    }

    this.audioPlayer.playWordAudio(currentWord, language);
  }

}
