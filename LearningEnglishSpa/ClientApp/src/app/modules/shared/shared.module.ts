import { NgModule, ModuleWithProviders } from "@angular/core";
import { IdentityComponent } from "./components/identity/identity.component";
import { SecurityService } from "./services/security.service";
import { StorageService } from "./services/storage.service";
import { ConfigurationService } from "./services/configuration.service";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { DataService } from "./services/data.service";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  declarations: [
    IdentityComponent,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    IdentityComponent
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
        StorageService
      ]
    };
  }

}
