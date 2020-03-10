import { Component, OnInit, Input } from '@angular/core';
import {LoginService } from '../services/blog.service';
import {Comment} from '../entities/comment';
// import { Comment, Comment } from '@angular/compiler';
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() commentId = null;
  comments = null;
  commentCounts = 0;
  @Input() content:string = null;
  @Input() email:string;
  @Input() name:string;
  comment:Comment;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getCommentsByArticleId(this.commentId).subscribe(value => {
      this.comments = value;
      for(let i in value){
        this.commentCounts++;
      }});
  }
  addContent():void{
    console.log("name:" + this.name + "email:" + this.email + "content:" + this.content);
    if(this.content == null || this.content == "" || this.content == undefined)
    {
      alert("内容不能为空哟~");
    }else if(this.content.length >= 20){
      alert("字数过多~");
    }else{
      this.comment = {
        ArticleId : this.commentId,
        CommentName : this.name,
        Content : this.content,
        FirstComment : 1,
        MailBox:this.email
      };
      this.loginService.addComment(this.comment).subscribe();
      alert("感谢你，评论添加成功！");
    }
  }
}
