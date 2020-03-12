import { NgModule, ModuleWithProviders } from "@angular/core";
import { VocabularyComponent } from "./vocabulary.component";
import { VocabularyService } from "./vocabulary.service";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@NgModule({
  imports: [CommonModule, FormsModule],
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
