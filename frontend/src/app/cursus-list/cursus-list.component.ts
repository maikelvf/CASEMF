import { Component, OnInit } from '@angular/core';
import CursusService from '../cursus.service';
import { Cursusinstantie } from '../models/cursusinstantie';

@Component({
  selector: 'app-cursus-list',
  templateUrl: './cursus-list.component.html',
  styleUrls: ['./cursus-list.component.css']
})
export class CursusListComponent implements OnInit {

  columnsToDisplay = ['startdatum', 'duur', 'titel'];

  cursusinstanties: Array<Cursusinstantie>;

  // // Huidige week is 27
  // weeknummer: number = 27;

  constructor(private cursusService: CursusService) { }

  ngOnInit() {
    this.cursusService.getAll(27).subscribe(data => {
      this.cursusinstanties = data;
    });
  }
}