import { Component, OnInit } from "@angular/core";
import { VocabularyService } from "./vocabulary.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { Vocabulary } from "./models/vocabulary.model";
import { NguCarouselConfig } from '@ngu/carousel';
import { Observable } from "rxjs";

@Component({
  templateUrl: 'vocabularyGallery.component.html',
  styleUrls: ['vocabularyGallery.component.css']
})
export class VocabularyGalleryComponent implements OnInit {
  public vocabularies$: Observable<Vocabulary[]>;

  public vocabularies: Vocabulary[] = [];
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
  loadData(): void {
    this.getVocabularies();
  }

  getVocabularies(): void {

    this.service.getVocabularies().subscribe(res => this.vocabularies = res);
  }

  public carouselTileConfig: NguCarouselConfig = {
    grid: { xs: 1, sm: 2, md: 3, lg: 3, all: 0 },
    speed: 500,
    point: {
      visible: true
    },
    touch: true,
    loop: true,
    interval: { timing: 5000 },
    animation: 'lazy'
  };

}
