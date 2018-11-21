import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../_Constants/Constants.enum';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(Model: any) {
    return this.http.post(Constants.BaseUrl + "Auth/login", Model)
      .pipe(
        map(
          (Result: any) => {
            const data = Result;
            if (data) {
              localStorage.setItem("token", data.token);
            }
          }
        )
      )
  }

  IsAuth() {
    // if(localStorage.getItem("token"))
    //   return true;
    // else return false;

    //the short hand ffor previous peace
    return !!localStorage.getItem("token")
  }

  Logout(){
    localStorage.removeItem("token");
  }
}
