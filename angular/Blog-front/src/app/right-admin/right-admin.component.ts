import { Component, OnInit } from '@angular/core';
import {LoginService } from '../services/blog.service';
@Component({
  selector: 'app-right-admin',
  templateUrl: './right-admin.component.html',
  styleUrls: ['./right-admin.component.css']
})
export class RightAdminComponent implements OnInit {
  allArticleMessage = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getAllArticleMessage(this.loginService.userId).subscribe(value => {this.allArticleMessage = value;});
    console.log(this.allArticleMessage);
  }

  deleteArticle(article:any){
    this.loginService.deleteArticle(article['articleId']).subscribe();
    alert("你已经删除《" + article['articleName'] + "》文章");
  }

}
