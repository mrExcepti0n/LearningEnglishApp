import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { VocabularyComponent } from "./vocabulary/vocabulary.component";
import { TrainingComponent } from "./training/training.component";
import { WordSetComponent } from "./wordset/wordset.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'vocabulary', component: VocabularyComponent },
  { path: 'training', component: TrainingComponent },
  { path: 'wordset', component: WordSetComponent }
]

export const routing = RouterModule.forRoot(routes);
