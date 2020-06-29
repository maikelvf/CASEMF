import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';

import { AppComponent } from './app.component';
import { CursusoverzichtComponent } from './cursusoverzicht/cursusoverzicht.component';

@NgModule({
  declarations: [
    AppComponent,
    CursusoverzichtComponent
  ],
  imports: [
    BrowserModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
