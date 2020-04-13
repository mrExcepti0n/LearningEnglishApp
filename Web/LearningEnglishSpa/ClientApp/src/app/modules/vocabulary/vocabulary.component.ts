import { Component, OnInit } from "@angular/core";
import { VocabularyService } from "./vocabulary.service";
import { ConfigurationService } from "../shared/services/configuration.service";
import { Vocabulary } from "./models/vocabulary.model";
import { NguCarouselConfig } from '@ngu/carousel';

@Component({
  templateUrl: 'vocabulary.component.html',
  styleUrls: ['vocabulary.component.css']
})
export class VocabularyComponent implements OnInit {

  public vocabularies: Vocabulary[];


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
    this.service.getVocabularies().subscribe(result => { this.vocabularies = result });
  }
  public carouselTileItems: number[] = [1, 2, 3, 4, 5];
  public carouselTileConfig: NguCarouselConfig = {
    grid: { xs: 1, sm: 1, md: 1, lg: 5, all: 0 },
    speed: 250,
    point: {
      visible: true
    },
    touch: true,
    loop: true,
    interval: { timing: 5000 },
    animation: 'lazy'
  };

}
