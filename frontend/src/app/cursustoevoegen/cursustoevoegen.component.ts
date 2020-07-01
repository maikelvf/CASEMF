import { Component, OnInit } from '@angular/core';
import CursusService from '../cursus.service';

@Component({
  selector: 'app-cursustoevoegen',
  templateUrl: './cursustoevoegen.component.html',
  styleUrls: ['./cursustoevoegen.component.css']
})
export class CursustoevoegenComponent implements OnInit {

  fileToUpload: File = null;

  succesResponse: String;
  duplicateCursusResponse: String;
  duplicateInstantieResponse: String;

  constructor(private cursusService : CursusService) { }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFile() {
    if (this.fileToUpload == null) {
      alert('Kies een bestand om te uploaden!');
      return;
    }
    this.cursusService.postFile(this.fileToUpload).subscribe(data => {
       var response = data.toString().split('.');
       this.succesResponse = response[0];
       this.duplicateCursusResponse = response[1];
       this.duplicateInstantieResponse = response[2];
      }, error => {
        console.log(error);
    });
  }

  ngOnInit(): void {
  }

}
