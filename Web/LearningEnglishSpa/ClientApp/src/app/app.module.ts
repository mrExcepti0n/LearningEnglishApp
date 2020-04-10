import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { routing } from './app.routes';
import { AppComponent } from './app.component';
import { SharedModule } from './modules/shared/shared.module';
import { WordSetModule } from './modules/wordset/wordset.module';
import { HomeComponent } from './modules/home/home.component';
import { VocabularyModule } from './modules/vocabulary/vocabulary.module';
import { TrainingModule } from './modules/training/training.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    routing,
    HttpClientModule,
    SharedModule.forRoot(),
    VocabularyModule,
    TrainingModule,
    WordSetModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
