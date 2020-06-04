import { NgModule, ModuleWithProviders } from "@angular/core";
import { TrainingComponent } from "./training.component";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { ChooseTranslateTrainingComponent } from "./chooseTranslate/chooseTranslateTraining.component";
import { TrainingResultComponent } from "./shared/components/trainingResult.component";
import { SharedModule } from "../shared/shared.module";
import { TrainingService } from "./shared/services/trainig.service";
import { CollectWordTrainingComponent } from "./collectWord/collectWordTraining.component";

const
  routing = RouterModule.forChild([
  {
    path: 'training', component: TrainingComponent
  },
  {
    path: 'training/chooseTranslate', data: { isReverse: false }, component: ChooseTranslateTrainingComponent
  },
  {
    path: 'training/chooseTranslate/isReverse', data: { isReverse: true }, component: ChooseTranslateTrainingComponent
  },
  {
    path: 'training/collectWord', data: { isReverse: false }, component: CollectWordTrainingComponent
  },
  {
    path: 'training/collectWord/isReverse', data: { isReverse: true }, component: CollectWordTrainingComponent
  },
])

@NgModule({
  imports: [CommonModule, FormsModule, SharedModule,  routing],
  declarations: [TrainingComponent,  ChooseTranslateTrainingComponent, CollectWordTrainingComponent, TrainingResultComponent],
  providers: [TrainingService]
})
export class TrainingModule {
  static forRoot(): ModuleWithProviders<TrainingModule> {
    return {
      ngModule: TrainingModule,
      providers: [
        // Providers
        TrainingService
      ]
    };
  }
}
