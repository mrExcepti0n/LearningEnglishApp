import { NgModule, ModuleWithProviders } from "@angular/core";
import { VocabularyComponent } from "./vocabulary.component";
import { VocabularyService } from "./vocabulary.service";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ClickOutsideModule } from 'ng-click-outside';
import { SharedModule } from "../shared/shared.module";
import { UserWordsComponent } from "./userWords.component";
import { NguCarouselModule } from '@ngu/carousel';
import { RouterModule } from "@angular/router";
import { VocabularyGalleryComponent } from "./vocabularyGallery.component";


const routing = RouterModule.forChild([
  {
    path: 'vocabulary', component: VocabularyGalleryComponent
  },
  {
    path: 'vocabulary/:id', component: VocabularyComponent,
  }
])



@NgModule({
  imports: [CommonModule, FormsModule, ClickOutsideModule, SharedModule, NguCarouselModule, routing],
  declarations: [VocabularyGalleryComponent,VocabularyComponent, UserWordsComponent],
  providers: [VocabularyService]
})
export class VocabularyModule {
  static forRoot(): ModuleWithProviders<VocabularyModule> {
    return {
      ngModule: VocabularyModule,
      providers: [
        // Providers
        VocabularyService
      ]
    };
  }  
}
