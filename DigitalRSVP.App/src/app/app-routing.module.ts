import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './pages/notfound/notfound.component';
import { InviteComponent } from './pages/invite/invite.component';
import { HttpClientModule } from '@angular/common/http';
import { RsvpComponent } from './pages/rsvp/rsvp.component';

const routes: Routes = [
  { path: 'invite/:invite', component: InviteComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'rsvp', component: RsvpComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    HttpClientModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
