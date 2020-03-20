import { NumberValueAccessor } from '@angular/forms';

export interface Article{
    ArticleName:string;
    Content:string;
    InUser:string;
    ClassificationId:number;
    UserId:number;
    TagId:number;
}