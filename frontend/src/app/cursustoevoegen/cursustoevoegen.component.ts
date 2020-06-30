import { Component, OnInit } from '@angular/core';
import CursusService from '../cursus.service';

@Component({
  selector: 'app-cursustoevoegen',
  templateUrl: './cursustoevoegen.component.html',
  styleUrls: ['./cursustoevoegen.component.css']
})
export class CursustoevoegenComponent implements OnInit {

  fileToUpload: File = null;

  public response;

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
      this.response = data
      }, error => {
        console.log(error);
    });
  }

  ngOnInit(): void {
  }

}
