import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home';
import { ExerciseDetails } from './features/exercise-details/exercise-details';
import { ComingSoon } from './shared/components/coming-soon/coming-soon';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Home Page'
    },
    {
        path: 'exercise-details/:id',
        component: ExerciseDetails,
        title: 'Exercise Details Page'
    },
    {
        path: 'coming-soon',
        component: ComingSoon,
        title: 'Coming Soon Page'
    }
];
