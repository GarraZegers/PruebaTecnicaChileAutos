import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path:'',
        redirectTo: 'episodes',
        pathMatch:'full'
    },
    {
        path: 'episodes',
        loadChildren: () => 
            import('./features/episodes/episode.routes').then((m) => m.routes)
    },
    {
    path: 'characters',
    loadChildren: () => import('./features/characters/character.routes').then(m => m.routes)
  },
  {
    path: 'locations',
    loadChildren: () => import('./features/locations/location.routes').then(m => m.LOCATION_ROUTES)
  }
    
];
