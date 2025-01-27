import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './pages/notfound/notfound.component';
import { InviteComponent } from './pages/invite/invite.component';
import { HttpClientModule } from '@angular/common/http';

const routes: Routes = [
  { path: 'invite/:invite', component: InviteComponent },
  { path: 'not-found', component: NotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    HttpClientModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
