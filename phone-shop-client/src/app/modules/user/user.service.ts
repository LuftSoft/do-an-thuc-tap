import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API } from 'src/app/core/constant/API';
import { BaseAPIResponse, CONFIG } from 'src/app/core/constant/CONFIG';
import { LoginModel, SignupModel } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }
  userLogin(data: any): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.LOGIN}`, data);
  }
  userSignup(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SIGNUP}`, data);
  }
  getUserInfo() {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SELF_INFORMATION}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  fogotPassword(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.FOGOT_PASSWORD}`, data);
  }

  resetPassword(data: SignupModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.RESET_PASSWORD}`, data);
  }
  //product
  getListPhone(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}`);
  }
  getDetailPhone(id: string): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}/${id}`);
  }
  createNewPhone(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}`);
  }
  updatePhone(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}`);
  }
  deletePhone(id: string): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}/${id}`);
  }
  //
  getUserCart(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.CART}`,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  addToCart(id: string, quantity: number): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.CART}`,
      {
        id: 0,
        userId: "",
        phoneId: id,
        quantity: quantity ? quantity : 1
      }, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  addToCartByNumber(cart_id: number, phone_id: string, quantity: number): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.CART}`,
      {
        id: cart_id,
        userId: "",
        phoneId: phone_id,
        quantity: quantity
      }, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  deleteCart(id: number): Observable<BaseAPIResponse> {
    return this.http.delete<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.CART}/${id}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  //
  createOrder(id: string): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.CART}`, {});
  }
}
