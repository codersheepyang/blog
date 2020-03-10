import { Component, OnInit } from '@angular/core';
import { User } from '../entities/user';
import {LoginService } from '../services/blog.service';
import {Router} from '@angular/router';
@Component({
  selector: 'app-blog-register',
  templateUrl: './blog-register.component.html',
  styleUrls: ['./blog-register.component.css']
})
export class BlogRegisterComponent implements OnInit {
  message = "欢迎你";
  registerUser : User;
  constructor(private loginService : LoginService,private router : Router) { }

  ngOnInit() {
  }

  register(username:string,password:string){
    if(username.length == 0 || password.length == 0){
      this.message = "账号或密码为空！";
      return;
    }
    this.registerUser = {
      username : username,
      password : password
    }
    this.loginService.AddUser(this.registerUser).subscribe(value => {
        if(value != null)
        {
          console.log(value);
          if((JSON.stringify(value)).includes('1'))
          {
            alert("注册成功");
            this.router.navigateByUrl("/login");
          }
          else if((JSON.stringify(value)).includes('0'))
          {
            this.message = '用户名已存在';
          }
        }
    });
  }

}
