import { Component, OnInit } from '@angular/core';
import { Cursus } from '../models/cursus';

@Component({
  selector: 'app-cursusoverzicht',
  templateUrl: './cursusoverzicht.component.html',
  styleUrls: ['./cursusoverzicht.component.css']
})
export class CursusoverzichtComponent implements OnInit {

  cursussen: Cursus[] = [
    { Startdatum: '01/01/2020', Duur: 2, Titel: "Cursus 1"},
    { Startdatum: '05/05/2020', Duur: 5, Titel: "Cursus 2"}
  ];

  constructor() { }

  ngOnInit(): void {
  }
}
