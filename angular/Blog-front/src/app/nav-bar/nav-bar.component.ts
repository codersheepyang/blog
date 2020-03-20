import { Component, OnInit } from '@angular/core';
import {LoginService} from '../services/blog.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  title = '首页';
  head = '../../assets/img/head.png';
  status = "未登录";
  constructor(private loginService : LoginService) { 
  }

  ngOnInit() {
      this.loginService.getAboutMe();
  }

}