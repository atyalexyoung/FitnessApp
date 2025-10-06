import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home';
import { ExerciseDetails } from './features/exercise-details/exercise-details';

export const routes: Routes = [
    {
        path:'',
        component: HomeComponent,
        title:'Home Page'
    },
    {
        path:'exercise-details',
        component: ExerciseDetails,
        title:'Exercise Details Page'
    }
];
