import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  tags = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getAllTags().subscribe(value =>{
        this.tags = value;
    });
  }

}
