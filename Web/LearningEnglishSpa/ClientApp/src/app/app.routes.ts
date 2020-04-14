import { Routes, RouterModule } from "@angular/router";
import { WordSetComponent } from "./modules/wordset/wordset.component";
import { HomeComponent } from "./modules/home/home.component";
import { TrainingComponent } from "./modules/training/training.component";
import { VocabularyGalleryComponent } from "./modules/vocabulary/vocabularyGallery.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'training', component: TrainingComponent },
  { path: 'wordset', component: WordSetComponent }
]

export const routing = RouterModule.forRoot(routes);
