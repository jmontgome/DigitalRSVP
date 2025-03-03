import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './pages/notfound/notfound.component';
import { InviteComponent } from './pages/invite/invite.component';
import { HttpClientModule } from '@angular/common/http';
import { RsvpComponent } from './pages/rsvp/rsvp.component';
import { CompleteComponent } from './pages/complete/complete.component';
import { ReportComponent } from './pages/report/report.component';

const routes: Routes = [
  { path: 'invite/:invite', component: InviteComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'rsvp', component: RsvpComponent },
  { path: 'complete', component: CompleteComponent },
  { path: 'report', component: ReportComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    HttpClientModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
