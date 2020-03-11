import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { Identity } from './components/identity.component';
import { SecurityService } from './services/security.service';
import { ConfigurationService } from './services/configuration.service';
import { StorageService } from './services/storage.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    Identity
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [SecurityService,
    ConfigurationService,
    StorageService,],
  bootstrap: [AppComponent]
})
export class AppModule { }
