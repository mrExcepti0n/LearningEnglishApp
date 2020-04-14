import { Component, OnInit } from "@angular/core";
import { VocabularyService } from "./vocabulary.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { Vocabulary } from "./models/vocabulary.model";
import { ActivatedRoute } from "@angular/router";

@Component({
  templateUrl: 'vocabulary.component.html'
})
export class VocabularyComponent implements OnInit
{
  public vocabulary: Vocabulary;

  public vocabularyId: number;

  constructor(private service: VocabularyService, private configurationService: ConfigurationService, activateRoute: ActivatedRoute) {
    this.vocabularyId = activateRoute.snapshot.params['id'];
  }

  ngOnInit() {
    if (this.configurationService.isReady) {
      this.loadData();
    }
    else {
      this.configurationService.settingsLoaded$.subscribe(() => this.loadData());
    }
  }

  loadData(): void {
    this.getVocabulary();
  }

  getVocabulary(): void {
    this.service.getVocabulary(this.vocabularyId).subscribe(res => this.vocabulary = res );
  }
}
