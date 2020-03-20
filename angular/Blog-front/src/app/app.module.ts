import { BrowserModule} from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BlogLoginComponent } from './blog-login/blog-login.component';
import { FormsModule } from '@angular/forms';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ArticleListComponent } from './article-list/article-list.component';
import { AdminComponent } from './admin/admin.component';
import { AboutMeComponent } from './about-me/about-me.component';
import { AppRoutingModule } from './app-routing.module';
import {MatCardModule, MatDividerModule, MatIconModule, MatSelectModule,MatListModule,MatGridListModule,MatInputModule,MatButtonModule} from '@angular/material';
import { RigthColumnComponent } from './rigth-column/rigth-column.component';
import { LeftColumnComponent } from './left-column/left-column.component';
import { ArticleComponent } from './article/article.component';
import { HotArticlesComponent } from './hot-articles/hot-articles.component';
import { CommentComponent } from './comment/comment.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { LeftAdminComponent } from './left-admin/left-admin.component';
import { RightAdminComponent } from './right-admin/right-admin.component';
import { ClassificationComponent } from './classification/classification.component';
import { CommentAdminComponent } from './comment-admin/comment-admin.component';
import { WriteArticleComponent } from './write-article/write-article.component';
import {EditorMdDirective} from './editor/editor-md.directive';
import { AdvertisementsComponent } from './advertisements/advertisements.component';
import { PhotosComponent } from './photos/photos.component';
import { AdvertisementAdminComponent } from './advertisement-admin/advertisement-admin.component';
import { BlogRegisterComponent } from './blog-register/blog-register.component';
import { PersonalMessageComponent } from './personal-message/personal-message.component';
import { HomePageComponent } from './home-page/home-page.component';


@NgModule({
  declarations: [
    AppComponent,
    BlogLoginComponent,
    NavBarComponent,
    ArticleListComponent,
    AdminComponent,
    AboutMeComponent,
    RigthColumnComponent,
    LeftColumnComponent,
    ArticleComponent,
    HotArticlesComponent,
    CommentComponent,
    LeftAdminComponent,
    RightAdminComponent,
    ClassificationComponent,
    CommentAdminComponent,
    WriteArticleComponent,
    EditorMdDirective,
    AdvertisementsComponent,
    PhotosComponent,
    AdvertisementAdminComponent,
    BlogRegisterComponent,
    PersonalMessageComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    MatIconModule,
    MatListModule,
    MatDividerModule,
    MatCardModule,
    MatGridListModule, 
    MatInputModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
