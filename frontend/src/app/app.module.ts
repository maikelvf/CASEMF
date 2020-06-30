import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';

import { AppComponent } from './app.component';
import { CursusListComponent } from './cursus-list/cursus-list.component';
import { CursustoevoegenComponent } from './cursustoevoegen/cursustoevoegen.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    CursusListComponent,
    CursustoevoegenComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatTableModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
