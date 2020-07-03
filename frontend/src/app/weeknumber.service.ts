import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WeeknumberService {

  constructor() { }
}

export function getWeek(value: Date) {
  var date = new Date(value.getTime());
  date.setHours(0, 0, 0, 0);
  // Zet de dag naar donderdag zodat altijd de goede week wordt gevonden
  date.setDate(date.getDate() + 3 - (date.getDay() + 6) % 7);
  // 4 januari valt altijd in week 1
  var week1 = new Date(date.getFullYear(), 0, 4);
  return 1 + Math.round(((date.getTime() - week1.getTime()) / 86400000 - 3 + (week1.getDay() +6) % 7) / 7);
}

export function getYear(value: Date) {
  return new Date().getFullYear();
}
