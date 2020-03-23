import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { AboutMe } from '../entities/aboutMe';
import {Consumer} from '../entities/Consumer';
import { Observable, of, throwError } from 'rxjs';
import { User } from '../entities/user';
import {PersonalMessage} from '../entities/personalMessage'
import { Comment } from '../entities/comment';
import {Classification} from '../entities/classification';
import {Advertisement} from '../entities/advertisement';
import {Article} from '../entities/article';
import {GetLoginUser} from '../entities/getLoginUser'
import {UpdateArticle} from '../entities/updateArticle';
const headers: HttpHeaders = new HttpHeaders({
  'Content-Type': 'application/json'
});
@Injectable({
  providedIn: 'root'
})
export class LoginService {


  public user = null;
  public userId;
  public articleId;
  private commonAddress:string = 'https://localhost:44372/api';
  private articleAddress: string = `${this.commonAddress}/article`;
  private managementAddress: string = `${this.commonAddress}/management`;
  private aboutMeAddress:string = `${this.commonAddress}/thatMe`;
  private consumerUrl = `${this.aboutMeAddress}/consumer`;
  private advertisementUrl = `${this.aboutMeAddress}/advertisement`;
  private advertisementsUrl = `${this.aboutMeAddress}/advertisements`;
  private aboutMeUrl = `${this.articleAddress}/aboutMe`;
  private UserUrl = `${this.commonAddress}/user`;
  private RegisterUrl = `${this.UserUrl}/register`;
  private loginUrl = `${this.UserUrl}/login`;
  private allClssificationUrl = `${this.articleAddress}/allClassification`;
  private allArticleMessageUrl = `${this.managementAddress}/allArticleMessage`;
  private personalMessageUrl = `${this.managementAddress}/personalMessage`;
  private commentsUrl = `${this.managementAddress}/comments`;
  private browseNumberUrl = `${this.managementAddress}/browseNumbers`;
  private classificationUrl = `${this.managementAddress}/classification`;
  private commentUrl = `${this.articleAddress}/comment`;
  private classificationsUrl = `${this.managementAddress}/classifications`;
  private hotArticleUrl = `${this.articleAddress}/articleByReadCounts`;
  private articlesByUpdateTimeUrl = `${this.articleAddress}/articleByUpdateTime`;
  private classifcationsUrl = `${this.articleAddress}/classifications`;
  private tagsUrl = `${this.articleAddress}/tags`;
  private articlesByTagIdUrl = `${this.articleAddress}/articlesByTagId`;
  private tagUrl = `${this.managementAddress}/tag`;

  
  constructor(private http: HttpClient) { }

  updateCommentStatus(comment:any):Observable<any>{
    const url = `${this.managementAddress}/comment`;
    return this.http.put(url,comment,{headers});
  }

  getArticlesByTagId(tagId:Number):Observable<any>{
    const url = `${this.articlesByTagIdUrl}/${tagId}`;
    return this.http.get<any>(url);
  }
  getAllTags():Observable<any>{
    return this.http.get<any>(this.tagsUrl);
  }
  getAllClassifications():Observable<any>{
    return this.http.get<any>(this.classifcationsUrl);
  }

  getPersonalMessage(userId:Number):Observable<any>{
    const url = `${this.personalMessageUrl}/${userId}`;
    return this.http.get<any>(url);
  }
  getAllArticleMessage(userId:Number): Observable<any> {
    const url = `${this.allArticleMessageUrl}/${userId}`;
    return this.http.get<any>(url);
  }
  getArticleByUpdateTime(userId:Number):Observable<any>{
    const url = `${this.articlesByUpdateTimeUrl}/${userId}`;
    return this.http.get(url);
  }
  AddPersonalMessage(personalMessage : PersonalMessage):Observable<any>{
    return this.http.post(this.personalMessageUrl,personalMessage,{headers});
  }
  
  updateArticle(update : UpdateArticle):Observable<any>{
    const url = `${this.managementAddress}/article`;
    return this.http.put(url,update,{headers});
  }

  getArticleById(articleId:number):Observable<any>{
    const url = `${this.managementAddress}/article/${articleId}`;
    return this.http.get<any>(url);
  }

  getConsumer(userId:NumberConstructor):Observable<any>{
    const url = `${this.consumerUrl}/${userId}`;
    return this.http.get<any>(url);
  }

  addArticle(article:Article):Observable<any>{
   const url = `${this.managementAddress}/article`;
   return this.http.post(url,article,{headers});
 }

  getHotArticle(userId:Number):Observable<any>{
   const url = `${this.hotArticleUrl}/${userId}`;
    return this.http.get(url);
 }

 getAllAdvertisements(userId:Number):Observable<any>{
  const url = `${this.advertisementsUrl}/${userId}`;
  return this.http.get(url);
 }

  updateClassification(classification:Classification):Observable<any>{
    const url = `${this.managementAddress}/classification`;
    return this.http.put(url,classification,{headers});
  }

  addClassification(classification:Classification):Observable<any>{
    const url = `${this.managementAddress}/classification`;
    console.log(url + "= > " + classification);
    return this.http.post(url,classification,{headers});
  }

  addAdvertisement(advertisement:Advertisement):Observable<any>{
    return this.http.post(this.advertisementUrl,advertisement,{headers});
  }

  deleteAdvertisement(id:number):Observable<any>{
    const url = `${this.advertisementUrl}/${id}`;
    return this.http.delete(url,{headers});
  }

  deleteArticle(id:number):Observable<any>{
    const url =  `${this.managementAddress}/article/${id}`;
    console.log(url);
    return this.http.delete(url,{headers});
  }
  
  deleteClassification(id:number):Observable<any>{
    const url = `${this.managementAddress}/classification/${id}`;
    console.log(url);
    return this.http.delete(url,{headers});
  }

  deleteComment(id:number):Observable<any>{
    const url = `${this.managementAddress}/comment/${id}`;
    return this.http.delete(url,{headers});
  }

  getClassificaitons(userId:Number): Observable<any> {
    const url = `${this.classificationsUrl}//${userId}`;
    return this.http.get<any>(url);
  }

 

  addComment(comment: Comment): Observable<any> {
    return this.http.post<any>(this.commentUrl, comment, { headers });
  }

  getClassification(id: number): Observable<any> {
    return this.http.get<any>(`${this.classificationUrl}/${id}`);
  }

  getTag(id: number): Observable<any> {
    return this.http.get<any>(`${this.tagUrl}/${id}`);
  }

  getBrowseNumber(userId:number): Observable<any> {
    const url = `${this.browseNumberUrl}/${userId}`;
    return this.http.get<any>(url);
  }

  getCommentsByArticleId(id: number): Observable<any> {
    const url = `${this.articleAddress}/allCommentsByArticleId/${id}`;
    return this.http.get<any>(url);
  }

  getArticle(id: number): Observable<any> {
    const url = `${this.articleAddress}/article/${id}`;
    return this.http.get<any>(url);
  }

  getComments(userId:number): Observable<any> {
    const url = `${this.commentsUrl}/${userId}`;
    return this.http.get<any>(url);
  }



  getAllClassification(userId:number): Observable<any> {
    const url = `${this.allClssificationUrl}/${userId}`;
    return this.http.get<any>(url);
  }

  getAboutMe(): Observable<AboutMe> {
    return this.http.get<AboutMe>(this.aboutMeUrl);
  }

  checkLogin(login: User): Observable<any> {
    return this.http.post<GetLoginUser>(this.loginUrl, login, { headers });
  }

  AddUser(user: User): Observable<any> {
    return this.http.post<any>(this.RegisterUrl, user, { headers });
  }

  private handleError(error: HttpErrorResponse) {
    console.log("---------------错误日志---------------------");
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    console.log("---------------错误日志---------------------");
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
