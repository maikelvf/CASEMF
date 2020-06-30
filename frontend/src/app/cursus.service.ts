import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Cursus } from './models/cursus';
import { Cursusinstantie } from './models/cursusinstantie';

@Injectable({
  providedIn: 'root'
})
export default class CursusService {
  private api = 'http://localhost:8080/api';
  private cursus_api = `${this.api}/cursus`;
  private cursusinstantie_api = `${this.api}/cursusinstantie`
  private cursustoevoegen_api = `${this.api}/toevoegen`

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

  postFile(fileToUpload: File): Observable<boolean> {
    const url = this.cursustoevoegen_api;
    const data: FormData = new FormData();

    data.append('fileKey', fileToUpload, fileToUpload.name);

    return this.http.post(url, data)
      .pipe(
        map((response: any) => response),
        catchError(<T>(error: any, result?: T) => {
          console.log(error);
          alert(error.statusText);
          return of(result as T);
        }));
        
  }

  remove(id: number) {
    return this.http.delete(`${this.cursus_api}/${id.toString()}`);
  }
}