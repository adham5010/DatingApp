import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ValuesComponent',
  templateUrl: './ValuesComponent.component.html',
  styleUrls: ['./ValuesComponent.component.css']
})
export class ValuesComponentComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.PostValue();
    this.getAllValues();
  }

  getAllValues() {
    this.http.get("http://localhost:5000/api/values").subscribe((data) => { console.log(data) }, (data) => { console.log(data) });
  }

  PostValue() {
    this.http.post("http://localhost:5000/api/values", { "value": "sayed" }).subscribe((data) => { console.log(data) }, (data) => { console.log(data) });
  }
}
