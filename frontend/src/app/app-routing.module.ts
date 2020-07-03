import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursusListComponent } from './cursus-list/cursus-list.component';
import { CursustoevoegenComponent } from './cursustoevoegen/cursustoevoegen.component'
import { PageNotFoundComponent } from './page-not-found/page-not-found.component'
import { getWeek, getYear } from './weeknumber.service';

const routes: Routes = [
  { path: 'overzicht', redirectTo: `overzicht/${getYear(new Date())}/${getWeek(new Date())}`, pathMatch: 'full'},
  { path: 'overzicht/:jaar/:week', component: CursusListComponent },
  { path: 'toevoegen', component: CursustoevoegenComponent },
  { path: '', redirectTo: '/overzicht/2020/28', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}