import { NgModule, ModuleWithProviders } from "@angular/core";
import { VocabularyComponent } from "./vocabulary.component";
import { VocabularyService } from "./vocabulary.service";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ClickOutsideModule } from 'ng-click-outside';

@NgModule({
  imports: [CommonModule, FormsModule, ClickOutsideModule],
  declarations: [VocabularyComponent],
  providers: [VocabularyService]
})
export class VocabularyModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: VocabularyModule,
      providers: [
        // Providers
        VocabularyService
      ]
    };
  }  
}
