import { NgModule, ModuleWithProviders } from "@angular/core";
import { TrainingComponent } from "./training.component";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { ChooseTrainingComponent } from "./chooseTraining.component";
import { ChooseTranslateTrainingComponent } from "./chooseTranslate/chooseTranslateTraining.component";
import { TrainingResultComponent } from "./shared/components/trainingResult.component";
import { SharedModule } from "../shared/shared.module";
import { TrainingDataService } from "./shared/services/trainigData.service";
import { CollectWordTrainingComponent } from "./collectWord/collectWordTraining.component";

let routing = RouterModule.forChild([
  {
    path: 'training', component: TrainingComponent,
    children: [
      { path: '', component: ChooseTrainingComponent },
      { path: 'chooseTranslate', data: { isReverse: false }, component: ChooseTranslateTrainingComponent },
      { path: 'chooseTranslate/isReverse', data: { isReverse: true }, component: ChooseTranslateTrainingComponent },
      { path: 'collectWord', data: { isReverse: false }, component: CollectWordTrainingComponent },
      { path: 'collectWord/isReverse', data: { isReverse: true }, component: CollectWordTrainingComponent },
    ]
  }
])

@NgModule({
  imports: [CommonModule, FormsModule, SharedModule,  routing],
  declarations: [TrainingComponent, ChooseTrainingComponent, ChooseTranslateTrainingComponent, CollectWordTrainingComponent, TrainingResultComponent],
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
