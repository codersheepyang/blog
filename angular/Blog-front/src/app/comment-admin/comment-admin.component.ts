import { Component, OnInit,Input} from '@angular/core';
import {LoginService } from '../services/blog.service';
import { Alert } from 'selenium-webdriver';
@Component({
  selector: 'app-comment-admin',
  templateUrl: './comment-admin.component.html',
  styleUrls: ['./comment-admin.component.css']
})
export class CommentAdminComponent implements OnInit {
  title = "慎重删除评论哟~";
  comments = null;
  @Input() articleId = null;
  commentCounts = 0;
  allArticles = null;
  
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getComments(this.loginService.userId).subscribe(value => 
    {
      this.comments = value;
       for(let i in value)
       {
          this.commentCounts++;
       } 
    });
    
  }

  deleteComment(comment:any){
    this.loginService.deleteComment(comment['id']).subscribe();
    alert("成功删除评论者 " + comment['commentName'] + " 对文章《" + comment['articleName'] + "》说的话:" + comment['content']);
  }

}
