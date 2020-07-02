import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursusListComponent } from './cursus-list/cursus-list.component';
import { CursustoevoegenComponent } from './cursustoevoegen/cursustoevoegen.component'
import { PageNotFoundComponent } from './page-not-found/page-not-found.component'

const routes: Routes = [
  { path: 'overzicht', component: CursusListComponent },
  { path: 'overzicht/:jaar/:week', component: CursusListComponent },
  { path: 'toevoegen', component: CursustoevoegenComponent },
  { path: '', redirectTo: '/overzicht', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }