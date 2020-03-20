import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
import {AboutMe } from '../entities/aboutMe';
@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.css']
})
export class AboutMeComponent implements OnInit {
  consumer = null;
  constructor(private loginService :LoginService) { }

  ngOnInit() {
    this.loginService.getConsumer(this.loginService.userId).subscribe(value => this.consumer = value);
}
}
