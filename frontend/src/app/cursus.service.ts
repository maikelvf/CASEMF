import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cursus } from './models/cursus';
import { Cursusinstantie } from './models/cursusinstantie';

@Injectable({
  providedIn: 'root'
})
export default class CursusService {
  public api = 'http://localhost:8080/api';
  public cursus_api = `${this.api}/cursus`;
  public cursusinstantie_api = `${this.api}/cursusinstantie`

  constructor(private http: HttpClient) {}

  getCursussen(): Observable<Array<Cursus>> {
    return this.http.get<Array<Cursus>>(this.cursus_api);
  }

  getCursusinstanties(): Observable<Array<Cursusinstantie>> {
    return this.http.get<Array<Cursusinstantie>>(this.cursusinstantie_api);
  }

  get(id: string) {
    return this.http.get(`${this.cursus_api}/${id}`);
  }

  save(cursus: Cursus): Observable<Cursus> {
    let result: Observable<Cursus>;
    if (cursus.Id) {
      result = this.http.put<Cursus>(
        `${this.cursus_api}/${cursus.Id}`,
        cursus
      );
    } else {
      result = this.http.post<Cursus>(this.cursus_api, cursus);
    }
    return result;
  }

  remove(id: number) {
    return this.http.delete(`${this.cursus_api}/${id.toString()}`);
  }
}