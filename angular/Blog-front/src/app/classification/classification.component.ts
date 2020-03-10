import { Component, OnInit,Input } from '@angular/core';
import {LoginService } from '../services/blog.service';
import {Classification} from '../entities/classification';


@Component({
  selector: 'app-classification',
  templateUrl: './classification.component.html',
  styleUrls: ['./classification.component.css']
})
export class ClassificationComponent implements OnInit {
  title = "请认真对待每一个分类~";
  classifications = null;
  updateClassificationName = null;
  updateClassification = null;
  updatedClassificationName = null;
  @Input() classificationName:string;
  classification : Classification;
  updateStatus = null;
  addStatus = null;
  constructor(private loginService : LoginService) { }

  ngOnInit() {
    this.loginService.getClassificaitons(this.loginService.userId).subscribe(value => this.classifications = value);
  }

  
  update(){
    if(this.updateClassificationName == undefined || this.updatedClassificationName == null || 
      this.updatedClassificationName == ""){
        alert("不能修改为空名");
        return;
      }
      let i = 0;
      while(this.classifications[i] != undefined){
        console.log(this.classifications[0]['Id']);
        if(this.classifications[i]['ClassificationName'] == this.updatedClassificationName){
        alert("修改的分类名已存在");
          return;
        }
        i++;
      }
    console.log("修改后的名称:" + this.updateClassification);
    this.classification = {
      userId:this.loginService.userId,
      classificationName :this.updatedClassificationName,
      Id:this.updateClassification['Id']
    }
    this.loginService.updateClassification(this.classification).subscribe();
    alert("修改分类名成功");
  }
  saveClassification(classifcaiton:any){
    this.updateClassification = classifcaiton;
    console.log("修改的名字是 " + classifcaiton["ClassificationName"])
    this.updateClassificationName = classifcaiton["ClassificationName"];
    this.updateStatus = null;
  }

  deleteClassification(classification:any){
    console.log("分类ID:" +classification['Id']);
    this.loginService.deleteClassification(classification['Id']).subscribe();
    alert("你已成功删除" + classification['ClassificationName'] + "分类,包括该分类下存在的所有文章");
  }

  addClassification(){
    if(this.classificationName == undefined || this.classificationName == "" || this.classificationName == null){
      alert("请输入分类名哟~");
      return;
    }
    let i = 0;
    while(this.classifications[i] != undefined){
      console.log(this.classifications[0]['Id']);
      if(this.classifications[i]['ClassificationName'] == this.classificationName){
      alert("添加的分类已存在~");
        return;
      }
      i++;
    }
    this.classification = {
      userId:this.loginService.userId,
      classificationName : this.classificationName,
      Id : 0
    }
    this.loginService.addClassification(this.classification).subscribe();
    alert("分类添加成功!");
  }

}
