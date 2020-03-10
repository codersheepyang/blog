import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
@Component({
  selector: 'app-hot-articles',
  templateUrl: './hot-articles.component.html',
  styleUrls: ['./hot-articles.component.css']
})
export class HotArticlesComponent implements OnInit {
  hotArticle = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getHotArticle(this.loginService.userId).subscribe(value => this.hotArticle = value);
  }



}
