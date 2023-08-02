import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API } from 'src/app/core/constant/API';
import { BaseAPIResponse } from 'src/app/core/constant/CONFIG';
import { LoginModel, SignupModel } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }
  userLogin(data: LoginModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.LOGIN}`, { data });
  }
  userSignup(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SIGNUP}`, { data });
  }

  fogotPassword(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.FOGOT_PASSWORD}`, { data });
  }

  resetPassword(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.RESET_PASSWORD}`, { data });
  }

}
