import { Component, OnInit } from '@angular/core';
import {LoginService} from '../services/blog.service';
import { delay } from 'q';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  token = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
  }

}
