import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services/blog.service';
import {Advertisement} from '../entities/advertisement';

@Component({
  selector: 'app-advertisement-admin',
  templateUrl: './advertisement-admin.component.html',
  styleUrls: ['./advertisement-admin.component.css']
})
export class AdvertisementAdminComponent implements OnInit {
  @Input() Advertiser:string;
  @Input() Title:string;
  @Input() Content:string;
  advertisement:Advertisement;
  constructor(private loginService : LoginService) { }

  addAdvertisement()
  {
    if(this.Advertiser == null || this.Title == null || this.Content == null)
    {
      alert("请完整添加所有信息");
      return;
    }
    if(this.Title.length > 20)
    {
      alert("广告名称过长");
    }
    this.advertisement = {
      ID:0,
      Advertiser : this.Advertiser,
      Title : this.Title,
      Content:this.Content,
      UserId:this.loginService.userId
    }
    this.loginService.addAdvertisement(this.advertisement).subscribe();
    alert("广告添加成功");
  }
  ngOnInit() {
  }

}
