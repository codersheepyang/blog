import { Component, OnInit,Input, Type} from '@angular/core';
import {LoginService } from '../services/blog.service';
import { AboutMe } from '../entities/aboutMe';
import { Observable, of} from 'rxjs';
import { User } from '../entities/user';
import {GetLoginUser} from '../entities/getLoginUser';
import {Router} from '@angular/router';
import { Session } from 'protractor';
@Component({
  selector: 'app-blog-login',
  templateUrl: './blog-login.component.html',
  styleUrls: ['./blog-login.component.css']
})
export class BlogLoginComponent implements OnInit {
  login : User;
  user : any;
  message : string = "Welcome back";
  constructor(private loginService : LoginService,private router : Router) { }

  ngOnInit() {
  }

    checkLogin(username:string,password:string){
    if(username.length == 0 || password.length == 0){
      this.message = "账号或密码为空!";
      return;
    }
    this.login = {
      username : username,
      password : password
    }
      this.loginService.checkLogin(this.login).subscribe(value => {
        if(value != null)
        {
          
          this.loginService.userId = Number.parseInt(value); 
          this.user = value;
          this.loginService.userId = value;
          this.router.navigateByUrl("/articleList");
          return;
        }
        this.message = "账号或密码错误";
    });
  }

  
}

