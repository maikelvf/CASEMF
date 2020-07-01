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
  errorMessage: String;

  constructor(private cursusService : CursusService) { }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFile() {
    if (this.fileToUpload == null) {
      this.errorMessage = 'Kies een bestand om te uploaden!';
      return;
    }

    this.resetMessages();

    this.cursusService.postFile(this.fileToUpload).subscribe(data => {
       var response = data.toString().split('.');
       this.succesResponse = response[0];
       this.duplicateCursusResponse = response[1];
       this.duplicateInstantieResponse = response[2];
      }, error => {
        this.errorMessage = error.error;
    });
  }

  resetMessages(): void {
    this.succesResponse = '';
    this.duplicateCursusResponse = '';
    this.duplicateInstantieResponse = '';
    this.errorMessage = '';
  }

  ngOnInit(): void {
  }
}
