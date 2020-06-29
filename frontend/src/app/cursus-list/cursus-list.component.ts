import { Component, OnInit } from '@angular/core';
import CursusService from '../cursus.service';
import { Cursus } from '../models/cursus';
import { Cursusinstantie } from '../models/cursusinstantie';

@Component({
  selector: 'app-cursus-list',
  templateUrl: './cursus-list.component.html',
  styleUrls: ['./cursus-list.component.css']
})
export class CursusListComponent implements OnInit {

  columnsToDisplay = ['startdatum', 'duur', 'titel'];

  cursussen: Array<Cursus>;
  cursusinstanties: Array<Cursusinstantie>;

  constructor(private cursusService: CursusService) { }

  ngOnInit() {
    this.cursusService.getCursussen().subscribe(data => {
      this.cursussen = data;
    });

    this.cursusService.getCursusinstanties().subscribe(data => {
      this.cursusinstanties = data;
    });
  }
}