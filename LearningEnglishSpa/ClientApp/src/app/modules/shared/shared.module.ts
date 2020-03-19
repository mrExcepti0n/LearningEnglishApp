import { NgModule, ModuleWithProviders } from "@angular/core";
import { IdentityComponent } from "./components/identity/identity.component";
import { SecurityService } from "./services/security.service";
import { StorageService } from "./services/storage.service";
import { ConfigurationService } from "./services/configuration.service";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { DataService } from "./services/data.service";
import { WordImageService } from "./services/wordImage.service";
import { WordImageDirective } from "./directives/wordImage.directive";
import { WordThumbnailDirective } from "./directives/wordThumbnail.directive";
import { SpeechService } from "./services/speech.service";
import { AudioPlayer } from "./audioPlayer";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  declarations: [
    IdentityComponent,
    WordImageDirective,
    WordThumbnailDirective
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    IdentityComponent,
    WordImageDirective,
    WordThumbnailDirective
   ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        DataService,
        SecurityService,
        ConfigurationService,
        StorageService,
        WordImageService,
        SpeechService,
        AudioPlayer
      ]
    };
  }

}
