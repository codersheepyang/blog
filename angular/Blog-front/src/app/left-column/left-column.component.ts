import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
@Component({
  selector: 'app-left-column',
  templateUrl: './left-column.component.html',
  styleUrls: ['./left-column.component.css']
})
export class LeftColumnComponent implements OnInit {
  allClassification = null;
  allArticleMessage = null;
  articleCounts:number = 0;
  commentCounts:number = 0;
  browseNumber:number = 0;
  hotArticles = null;
  updateTime = null;
  constructor(private loginService : LoginService ) { }

  ngOnInit() {

    this.loginService.getArticleByUpdateTime(this.loginService.userId).subscribe(value => {
      this.updateTime = value;
        let i = 0;
        while(value[i] != undefined){
          let temp : string = value[i]['ArticleName'];
          if(temp.length > 8){
            value[i]['ArticleName'] = temp.substring(0,8) + "...";
          }
          i++;
        }
      });

    this.loginService.getHotArticle(this.loginService.userId).subscribe(value => {
      this.hotArticles = value;
        let i = 0;
        while(value[i] != undefined){
          let temp : string = value[i]['ArticleName'];
          if(temp.length > 8){
            value[i]['ArticleName'] = temp.substring(0,8) + "...";
          }
          i++;
        }
      });
    //获得文章信息
    this.loginService.getAllArticleMessage(this.loginService.userId).subscribe(value => {this.allArticleMessage = value;});
    //获得所有分类
    this.loginService.getAllClassification(this.loginService.userId).subscribe(value => {this.allClassification = value});
    //获得文章总数
    this.loginService.getAllArticleMessage(this.loginService.userId).subscribe(value => {for(let i in value){
      this.articleCounts++;
    }});
    this.loginService.getComments(this.loginService.userId).subscribe(value => {
      for(let i in value){
        this.commentCounts++;
      }
    });
    //获得浏览总数
    this.loginService.getBrowseNumber().subscribe(value => this.browseNumber = value);
  }
}
