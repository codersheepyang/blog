import { NgModule } from '@angular/core';
import { Routes, RouterModule, RouteReuseStrategy } from '@angular/router';

import { ArticleListComponent } from '../app/article-list/article-list.component';
import {AdminComponent } from '../app/admin/admin.component';
import {AboutMeComponent } from '../app/about-me/about-me.component';
import {ArticleComponent} from './article/article.component';
import {ClassificationComponent} from '../app/classification/classification.component';
import {RightAdminComponent} from '../app/right-admin/right-admin.component';
import {CommentAdminComponent} from '../app/comment-admin/comment-admin.component';
import { CustomReuseStrategy } from './strategy/CustomReuseStrategy';
import {WriteArticleComponent } from './write-article/write-article.component'; 
import {PhotosComponent} from '../app/photos/photos.component';
import { AdvertisementAdminComponent } from '../app/advertisement-admin/advertisement-admin.component'
import {BlogRegisterComponent} from '../app/blog-register/blog-register.component'
import{BlogLoginComponent} from '../app/blog-login/blog-login.component'
import{PersonalMessageComponent} from '../app/personal-message/personal-message.component'
const routes: Routes = [
    {path:'personalMessage',component:PersonalMessageComponent},
    {path:'',redirectTo:'/articleList',pathMatch:'full'},
    {path:'articleList',component:ArticleListComponent},
    {path:'admin',component:AdminComponent},
    {path:'aboutMe',component:AboutMeComponent},
    {path:'article',component:ArticleComponent},
    {path:'photo',component:PhotosComponent},
    {path:'advertisementAdmin',component:AdvertisementAdminComponent},
    {path:'register',component:BlogRegisterComponent},
    {path:'login',component:BlogLoginComponent},
    //参数化路由
    {path:'article/:id',component:ArticleComponent},
    {path:'classification',component:ClassificationComponent},
    {path:'rightAdmin',component:RightAdminComponent},
    {path:'commentAdmin',component:CommentAdminComponent},
    {path:'writeArticle/:id',component:WriteArticleComponent},
    {path:'writeArticle',component:WriteArticleComponent}

];

@NgModule({
    //{onSameUrlNavigation: 'reload', enableTracing: true}) => 在路由到同一页面重新加载页面
    imports:[RouterModule.forRoot(routes,{onSameUrlNavigation: 'reload', enableTracing: true})],
    exports: [RouterModule],
    providers: [
        {provide: RouteReuseStrategy, useClass: CustomReuseStrategy}
      ]
  })
  export class AppRoutingModule { }