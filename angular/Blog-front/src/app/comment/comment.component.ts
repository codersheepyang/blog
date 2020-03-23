import { Component, OnInit, Input } from '@angular/core';
import {LoginService } from '../services/blog.service';
import {Comment} from '../entities/comment';
// import { Comment, Comment } from '@angular/compiler';
import {Router} from '@angular/router';
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
  commentTitle = "评论";
  reply = null;
  constructor(private loginService : LoginService,private router : Router) { }

  ngOnInit() {
    this.loginService.getCommentsByArticleId(this.commentId).subscribe(value => {
      this.comments = value;
      for(let i in value){
        this.commentCounts++;
      }});
  }

  addReply(comment:any)
  {
    this.commentTitle = '回复';
    this.reply = comment;
    
  }
  addContent():void{
    if(this.loginService.userId == undefined)
    {
      this.loginService.articleId =this.commentId;
      console.log("commentId:",this.commentId);
      alert("请登录再评论");
      this.router.navigateByUrl('/login');
      return;
    }
    if(this.content == null || this.content == "" || this.content == undefined)
    {
      alert("内容不能为空哟~");
    }else if(this.content.length >= 20){
      alert("字数过多~");
    }else{
      if(this.commentTitle == '评论')
      {
        this.comment = {
          ArticleId : this.commentId,
          CommentName : this.name,
          Content : this.content,
          FirstComment : 1,
          MailBox:this.email,
          UserId:this.loginService.userId,
          Status:'未读'
        };
        this.loginService.addComment(this.comment).subscribe();
      }else
      {
        var obj = {
          Content : this.content,
          CommentId : this.reply['commentId'],
          UserId : this.loginService.userId,
          ReplyName : this.reply['commentName']
        }
        console.log("commentId",this.reply.commentId);
        this.loginService.addReply(obj).subscribe();
      }
      alert("感谢你，评论添加成功！");
      }
    }
  }

