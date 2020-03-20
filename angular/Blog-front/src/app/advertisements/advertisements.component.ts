import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/blog.service';

@Component({
  selector: 'app-advertisements',
  templateUrl: './advertisements.component.html',
  styleUrls: ['./advertisements.component.css']
})
export class AdvertisementsComponent implements OnInit {

  allAdvertisements = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getAllAdvertisements(this.loginService.userId).subscribe(value => this.allAdvertisements = value);
  }

}
