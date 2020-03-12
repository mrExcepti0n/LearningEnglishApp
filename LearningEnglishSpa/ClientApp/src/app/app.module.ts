import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { routing } from './app.routes';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from './modules/shared/shared.module';
import { VocabularyModule } from './vocabulary/vocabulary.module';
import { TrainingModule } from './training/training.module';
import { WordSetModule } from './wordset/wordset.module';

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
