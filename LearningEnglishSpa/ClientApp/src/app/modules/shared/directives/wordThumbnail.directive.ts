import { Directive, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { WordImageService } from '../services/wordImage.service';

@Directive({
  selector: 'img[wordsm]'
})
export class WordThumbnailDirective implements OnChanges {

  ngOnChanges(changes: SimpleChanges): void {
    this.wordImageService.getThumbnailUrl(this.wordsm).subscribe(src => this.el.nativeElement.src = src);
  }

  @Input()
  wordsm: string;

  constructor(private el: ElementRef, private wordImageService: WordImageService) {
  }
}
