import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/blog.service';
import {Router} from '@angular/router';
@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {

  constructor(private loginService:LoginService,private router:Router) { }

  ngOnInit() {
    console.log("userid:",this.loginService.userId)
    if(this.loginService.userId == undefined)
    {
      this.router.navigateByUrl('/login');
      return;
    }
  }

}
