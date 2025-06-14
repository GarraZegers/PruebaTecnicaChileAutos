import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'episodes',
        loadChildren: () => 
            import('./features/episodes/episode.routes').then((m) => m.routes)
        
    },
    {
        path:'',
        redirectTo: 'episodes',
        pathMatch:'full'
    }
];
