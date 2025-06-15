import { Routes } from '@angular/router';
import { EpisodeListPageComponent } from './pages/episode-list.page/episode-list.page.component';

export const routes: Routes = [
  {
    path: '',
    component: EpisodeListPageComponent,
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./pages/episode-detail/episode-detail.page').then((m) => m.EpisodeDetailPage),
  },
];
