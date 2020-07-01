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

  getAll(): Observable<Array<Cursusinstantie>> {
    return this.http.get<Array<Cursusinstantie>>(this.url);
  }

  // get(id: string) {
  //   return this.http.get(`${this.url}/${id}`);
  // }

  postFile(fileToUpload: File) {
    const data: FormData = new FormData();
    data.append('fileKey', fileToUpload, fileToUpload.name);

    return this.http.post(this.url, data);
  }

  // remove(id: number) {
  //   return this.http.delete(`${this.url}/${id.toString()}`);
  // }
}