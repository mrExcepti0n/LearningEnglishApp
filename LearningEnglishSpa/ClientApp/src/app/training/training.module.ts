import { NgModule } from "@angular/core";
import { TrainingComponent } from "./training.component";
import { RouterModule } from "@angular/router";
import { ChooseTrainingComponent } from "./chooseTraining/chooseTraining.component";
import { ChooseTranslateTrainingComponent } from "./chooseTrasnlate/chooseTranslateTraining.component";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";


let routing = RouterModule.forChild([
  {
    path: 'training', component: TrainingComponent,
    children: [
      {path: '', component: ChooseTrainingComponent},
      {path: 'chooseTranslate', component: ChooseTranslateTrainingComponent}
    ]
  },
  {
    path: '**', redirectTo: 'training'
  }
])

@NgModule({
  imports: [CommonModule, FormsModule, routing],
  declarations: [TrainingComponent, ChooseTrainingComponent, ChooseTranslateTrainingComponent]
})
export class TrainingModule {
  
}
