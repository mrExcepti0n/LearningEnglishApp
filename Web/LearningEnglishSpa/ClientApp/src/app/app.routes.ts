import { Routes, RouterModule } from "@angular/router";
import { WordSetComponent } from "./modules/wordset/wordset.component";
import { VocabularyComponent } from "./modules/vocabulary/vocabulary.component";
import { HomeComponent } from "./modules/home/home.component";
import { TrainingComponent } from "./modules/training/training.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'vocabulary', component: VocabularyComponent },
  { path: 'training', component: TrainingComponent },
  { path: 'wordset', component: WordSetComponent }
]

export const routing = RouterModule.forRoot(routes);
