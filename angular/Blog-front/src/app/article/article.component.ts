import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {LoginService } from '../services/blog.service';
import { timeout, delay } from 'q';
import {EditorConfig} from '../editor/model/editor-config';

declare var editormd: any;
@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  article = null;
  @Input() createTime = null;
  classificationId = null;
  classification = null;

  conf = new EditorConfig();
  constructor(
    private route : ActivatedRoute,
    private loginService : LoginService) { 

  }

  ngOnInit() {
    this.getArticle();
  }

     getArticle(){
    const id = +this.route.snapshot.paramMap.get("id");
    this.loginService.getArticle(id).subscribe(value => {
      this.article = value;
      this.conf.markdown = value['Content'];
      this.classificationId = value['ClassificationId'];
      editormd.markdownToHTML('detailmarkdown',this.conf);
    });
  } 

}
