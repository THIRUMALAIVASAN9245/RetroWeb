import { Routes, RouterModule } from '@angular/router';

import { RetrospectiveMeetingListComponent } from './retrospective-tool/retrospective-meeting-list/retrospective-meeting-list.component';
import { RetrospectiveMeetingDetailsComponent } from './retrospective-tool/retrospective-live/retrospective-meeting-details.component';
import { PageNotFound } from './common/pageNotFound.component';

const routes: Routes = [  
  { path: 'retrospective-meeting/:retroInfoId', component: RetrospectiveMeetingDetailsComponent  },
  { path: 'retrospective-meeting-details', component: RetrospectiveMeetingListComponent },
  { path: '**', redirectTo: 'retrospective-meeting-details', pathMatch: 'full' },
  { path: 'page-not-found', component: PageNotFound }
];

export const appRouterModule = RouterModule.forRoot(routes, { useHash: true });