import { Component, OnInit } from '@angular/core';
import CursusService from '../cursus.service';
import { Cursusinstantie } from '../models/cursusinstantie';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-cursus-list',
  templateUrl: './cursus-list.component.html',
  styleUrls: ['./cursus-list.component.css']
})
export class CursusListComponent implements OnInit {

  columnsToDisplay = ['startdatum', 'duur', 'titel'];
  cursusinstanties: Array<Cursusinstantie>;

  week: number = 27;
  jaar: number = 2020;

  constructor(private cursusService: CursusService, private route: ActivatedRoute, private router: Router) { }

  getCursussen() {
    this.cursusService.getAll(this.week, this.jaar).subscribe(data => {
      this.cursusinstanties = data;
    });
  }

  changedWeekNumber() {
    this.router.navigate(['/overzicht', this.jaar, this.week]);
    this.getCursussen();
  }

  increaseWeekNumber() {
    if (this.week + 1 > 53) {
      this.week = 1;
      this.jaar += 1;
    }
    else {
      this.week += 1;
    }
    this.router.navigate(['/overzicht', this.jaar, this.week]);
    this.getCursussen();
  }

  decreaseWeekNumber() {
    if (this.week - 1 < 1) {
      this.week = 53
      this.jaar -= 1;
    }
    else {
      this.week -= 1;
    }

    this.router.navigate(['/overzicht', this.jaar, this.week]);
    this.getCursussen();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      var urlJaar = Number(params.get("jaar"));
      var urlWeek = Number(params.get("week"));
      
      if (urlJaar > 0) {
        this.jaar = urlJaar;
      }

      if (urlWeek > 0) {
        this.week = urlWeek;
      }
    });

    this.getCursussen();
  }
}