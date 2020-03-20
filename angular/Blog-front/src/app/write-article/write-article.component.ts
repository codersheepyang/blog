import { Component, OnInit, Input } from '@angular/core';
import {EditorConfig} from '../editor/model/editor-config';
import {LoginService } from '../services/blog.service';
import {Article} from '../entities/article';
import { MatSelectChange, MatOption } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import {UpdateArticle} from '../entities/updateArticle';
import {Router} from '@angular/router';
@Component({
  selector: 'app-write-article',
  templateUrl: './write-article.component.html',
  styleUrls: ['./write-article.component.css']
})
export class WriteArticleComponent implements OnInit {
  title = null;
  conf = new EditorConfig();
  content = null;
  createUser = null;
  classifications = null;
  classificationId = null;
  remind = null;
  article : Article;
  placeHolder = "选择你的分类";
  addOrUpdate = "确认添加文章";
  tag = "选择标签";
  status:boolean= false;
  update : UpdateArticle;
  articleId = null;
  tagId = null;
  tags = null;
  @Input() updateArticleMessage = null;
  constructor(
    private loginService :LoginService,
    private route : ActivatedRoute,
    private router : Router) { }
  
  ngOnInit(): void {
    const articleId = +this.route.snapshot.paramMap.get("id");
    if(articleId != 0){
      this.getUpdateArticle(articleId);
    }
    this.status = true;
      this.loginService.getAllTags().subscribe(value => {
        this.tags = value;
      });
      this.loginService.getClassificaitons(this.loginService.userId).subscribe(value => this.classifications = value);
  }

   getUpdateArticle(articleId : number){
    this.loginService.getArticleById(articleId).subscribe(value => {
      this.updateArticleMessage = value;
      this.content = value['Content'];
      this.title = value['ArticleName'];
      this.createUser = value['InUser'];
      this.articleId = value['Id'];
      this.classificationId = value['ClassificationId'];
      this.tagId = value['TagId'];
      this.loginService.getClassification(value['ClassificationId']).subscribe(value =>{
        this.placeHolder = "目前分类: " + value["ClassificationName"];
      });
      this.loginService.getTag(value['TagId']).subscribe(value => {
        this.tag = "目前标签:" + value['TagName'];
      });

    });
      this.addOrUpdate = "确认修改文章";
      this.status = true;
  }
  addArticle(){
    if(this.createUser == null || this.createUser == ""){
      this.remind = "创建用户不能为空";
      return;
    }
    if(this.title == null || this.title == ""){
      this.remind = "文章必须有标题";
      return;
    }
    if(this.classificationId == null || this.classificationId == 0 || this.classificationId == ""){
      this.remind = "必须选择分类";
      return;
    }
    if(this.content == null || this.content == "" || this.content == undefined){
      this.remind = "文章必须有内容！";
      return;
    }
    if(this.tagId == null || this.tagId == "" || this.tagId == undefined){
      this.remind = "文章必须有标签！";
      return;
    }
    this.article = {
      ArticleName: this.title,
      InUser : this.createUser,
      ClassificationId : this.classificationId,
      Content : this.content,
      UserId:this.loginService.userId,
      TagId:this.tagId
    }
    this.loginService.addArticle(this.article).subscribe();
    alert("文章添加成功~");
    this.router.navigateByUrl("/admin");

  }

  addOrUpdateArticle(){
    if(this.updateArticleMessage == null){
      this.addArticle();
    }else{
      this.updateArticle();
    }
  }

  updateArticle(){
    if(this.createUser == null || this.createUser == ""){
      this.remind = "修改的创建名不能为空";
      return;
    }
    if(this.title == null || this.title == ""){
      this.remind = "修改的文章必须有标题";
      return;
    }
    if(this.content == null || this.content == "" || this.content == undefined){
      this.remind = "修改的文章必须有内容！";
      return;
    }
    if(this.tagId == null || this.tagId == "" || this.tagId == undefined){
      this.remind = "修改的文章必须有标签！";
      return;
    }
    this.update = {
      ArticleName : this.title,
      Id : this.articleId,
      ClassificationId : this.classificationId,
      Content : this.content,
      UserId:this.loginService.userId,
      TagId:this.tagId
    }
    this.loginService.updateArticle(this.update).subscribe();
    alert("文章修改成功!");
    this.router.navigateByUrl("/admin");
    
  }

  selectedClassificaiton(event: MatSelectChange) {
    const selectedData = {
        text: (event.source.selected as MatOption).viewValue,
        value: event.source.value
    }
    this.classificationId = selectedData.value;
}
selectedTag(event: MatSelectChange) {
  const selectedData = {
      text: (event.source.selected as MatOption).viewValue,
      value: event.source.value
  }
  this.tagId = selectedData.value;
}


  // 同步属性内容
  syncModel(str): void {
    this.content = str;
  }
}
