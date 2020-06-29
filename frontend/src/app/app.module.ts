import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { CursusoverzichtComponent } from './cursusoverzicht/cursusoverzicht.component';

@NgModule({
  declarations: [
    AppComponent,
    CursusoverzichtComponent
  ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
