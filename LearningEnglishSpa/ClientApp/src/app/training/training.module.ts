import { NgModule, ModuleWithProviders } from "@angular/core";
import { TrainingComponent } from "./training.component";
import { RouterModule } from "@angular/router";
import { ChooseTrainingComponent } from "./chooseTraining/chooseTraining.component";
import { ChooseTranslateTrainingComponent } from "./chooseTrasnlate/chooseTranslateTraining.component";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { TrainingResultComponent } from "./trainingResultComponent/trainingResult.component";
import { TrainingDataService } from "./services/trainigData.service";


let routing = RouterModule.forChild([
  {
    path: 'training', component: TrainingComponent,
    children: [
      {path: '', component: ChooseTrainingComponent},
      {path: 'chooseTranslate', component: ChooseTranslateTrainingComponent}
    ]
  }
])

@NgModule({
  imports: [CommonModule, FormsModule, routing],
  declarations: [TrainingComponent, ChooseTrainingComponent, ChooseTranslateTrainingComponent, TrainingResultComponent],
  providers: [TrainingDataService]
})
export class TrainingModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: TrainingModule,
      providers: [
        // Providers
        TrainingDataService
      ]
    };
  }
}
