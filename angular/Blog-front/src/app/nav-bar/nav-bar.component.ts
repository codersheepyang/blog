import { Component, OnInit } from '@angular/core';
import {LoginService} from '../services/blog.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  title = '博客';
  head = 'http://blog.yangk.cc/images/head.png';
  status = "未登录";
  constructor(private loginService : LoginService) { 
  }

  ngOnInit() {
      this.loginService.getAboutMe();
  }

}
