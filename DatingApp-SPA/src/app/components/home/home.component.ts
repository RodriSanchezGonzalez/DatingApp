import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode: boolean;
  values: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.registerMode = false;
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
}
