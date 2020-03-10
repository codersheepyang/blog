import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
@Component({
  selector: 'app-rigth-column',
  templateUrl: './rigth-column.component.html',
  styleUrls: ['./rigth-column.component.css']
})
export class RigthColumnComponent implements OnInit {
  allArticleMessage = null;
  constructor(private loginService :LoginService) { }

  ngOnInit() {
    this.loginService.getAllArticleMessage(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }

  showArticlesByTime(){
    this.loginService.getArticleByUpdateTime(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }

  showArticlesByReadCounts(){
    this.loginService.getHotArticle(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }
}
