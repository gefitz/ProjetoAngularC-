import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { error } from 'console';
import { catchError, Observable, tap } from 'rxjs';
import { ReturnApiModel } from '../Models/ReturnApiModel.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  headers = new HttpHeaders({'Content-Type':'application/json'});
  apiUrl:string = "https://localhost:7255/api/"
  ret = new Observable<ReturnApiModel>()
  constructor(private http: HttpClient) { }
 get(parametro: any){
    var url = this.apiUrl + parametro.endPoint;
    var ret = this.http.get<ReturnApiModel>(url)
    return ret;
  }
  delete(parametro:any){
    var url = this.apiUrl + parametro.endPoint;
    return this.http.delete<ReturnApiModel>(url,{
      headers: this.headers,
      body:parametro.data
    });
  }
  post(parametro:any){
    const url = this.apiUrl + parametro.endPoint;
    return this.http.post<ReturnApiModel>(url,{body:parametro.data,headers:this.headers}) 
  }
}
