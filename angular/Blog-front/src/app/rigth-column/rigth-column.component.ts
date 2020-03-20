import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-rigth-column',
  templateUrl: './rigth-column.component.html',
  styleUrls: ['./rigth-column.component.css']
})
export class RigthColumnComponent implements OnInit {
  allArticleMessage = null;
  tagId = null
  constructor(private loginService :LoginService,private route : ActivatedRoute) { }

  ngOnInit() {
    this.tagId = +this.route.snapshot.paramMap.get("id");
    if(this.tagId != 0)
    {
      this.getAllArticlesByTagId(this.tagId);
      return;
    }
    this.loginService.getAllArticleMessage(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }

  showArticlesByTime(){
    this.loginService.getArticleByUpdateTime(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }

  showArticlesByReadCounts(){
    this.loginService.getHotArticle(this.loginService.userId).subscribe(value => this.allArticleMessage = value);
  }

  getAllArticlesByTagId(id:Number){
    this.loginService.getArticlesByTagId(id).subscribe(value => {
      console.log("ar",value)
      this.allArticleMessage = value;
    });
  } 
}
