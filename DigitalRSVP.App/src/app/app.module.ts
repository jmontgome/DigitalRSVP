import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './pages/notfound/notfound.component';
import { InviteComponent } from './pages/invite/invite.component';
import { CompleteComponent } from './pages/complete/complete.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    InviteComponent,
    CompleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
