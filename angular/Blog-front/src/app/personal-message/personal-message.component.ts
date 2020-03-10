import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services/blog.service';
import { PersonalMessage } from '../entities/personalMessage';
import {Router} from '@angular/router';
@Component({
  selector: 'app-personal-message',
  templateUrl: './personal-message.component.html',
  styleUrls: ['./personal-message.component.css']
})
export class PersonalMessageComponent implements OnInit {
  @Input() selfIntroduction : string;
  @Input() name:string;
  @Input() email:string;
  @Input() company:string;
  @Input() location:string;
  message = '添加';
  operation = '添加个人信息'
  personalMessage:PersonalMessage;
  constructor(private loginService : LoginService,private router : Router) { }


  ngOnInit() {
  }
  addPersonalMessage()
  {
    this.personalMessage = 
    {
      Email : this.email,
      Company : this.company,
      Location : this.location,
      Name : this.name,
      ID:this.loginService.userId,
      SelfIntroduction : this.selfIntroduction
    }
    this.loginService.AddPersonalMessage(this.personalMessage).subscribe();
      alert("个人信息添加成功");
      this.message = '修改';
      this.operation = '修改个人信息';
      this.router.navigateByUrl("/personalMessage");
  }

}

