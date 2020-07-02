import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Cursusinstantie } from './models/cursusinstantie';

@Injectable({
  providedIn: 'root'
})
export default class CursusService {
  private url = 'http://localhost:8080/api/cursusinstantie';

  constructor(private http: HttpClient) {}

  getAll(weeknummer): Observable<Array<Cursusinstantie>> {
    return this.http.get<Array<Cursusinstantie>>(`${this.url}?weeknummer=${weeknummer}`);
  }

  postFile(fileToUpload: File) {
    const data: FormData = new FormData();
    data.append('fileKey', fileToUpload, fileToUpload.name);

    return this.http.post(this.url, data);
  }
}