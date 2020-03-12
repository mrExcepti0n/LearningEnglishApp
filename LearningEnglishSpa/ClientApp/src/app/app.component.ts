import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SecurityService } from './modules/shared/services/security.service';
import { ConfigurationService } from './modules/shared/services/configuration.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  Authenticated: boolean = false;
  subscription: Subscription;
  title = 'app';

  constructor(private securityService: SecurityService, private configurationService: ConfigurationService) {
    this.Authenticated = this.securityService.IsAuthorized;
  }

  ngOnInit(): void {
    console.log('app on init');
    this.subscription = this.securityService.authenticationChallenge$.subscribe(res => this.Authenticated = res);

    //Get configuration from server environment variables:
    console.log('configuration');
    this.configurationService.load();    
  }
}
