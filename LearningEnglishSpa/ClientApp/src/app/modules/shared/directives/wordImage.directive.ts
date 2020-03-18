import { Directive, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { WordImageService } from '../services/wordImage.service';

@Directive({
  selector: 'img[word]'
})
export class WordImageDirective implements OnChanges {

  ngOnChanges(changes: SimpleChanges): void {
    this.wordImageService.getImageUrl(this.word).subscribe(src => this.el.nativeElement.src = src);
  }

  @Input()
  word: string;

  constructor(private el: ElementRef, private wordImageService: WordImageService) {  
  }
}
