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
  getHeaderAuth() {
    let headers = new HttpHeaders();
    headers.append('Authorization', `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`)
    return {
      headers: headers
    }
  }
  getHeaderMultiPart() {
    let headers = new HttpHeaders();
    headers.append('Authorization', `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`)
    return {
      headers: headers
    }
  }
  userLogin(data: any): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.LOGIN}`, data);
  }
  userSignup(data: any): Observable<BaseAPIResponse> {
    console.log(data);
    let _headers = new HttpHeaders();
    _headers.set('Content-Type', 'multipart/form-data');
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SIGNUP}`, data, {
      headers: _headers
    });
  }
  getUserInfo(): any {
    return this.http.get<any>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SELF_INFORMATION}`, {
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
  getAllUser() {
    return this.http.get<any>(`${API.BASE_URL}/${API.USER}`);
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
  //cart
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
  //order
  createOrder(data: any): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}`, data,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  getUserOrder(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}/${API.USER}`,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  getDetailOrder(id: string): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}/${id}`,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  getAllOrder(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}`,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  updatePaymentStatus(data: any): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}`, data,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  updateOrderStatus(data: any): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}/${API.SHOP.STATUS}`, data,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  cancelOrder(id: string): Observable<BaseAPIResponse> {
    return this.http.delete<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.ORDER}/${id}`,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  //location
  getAllProvince() {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.PROVINCE}`)
  }
  getDistrictByProvinceId(id: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.DISTRICT}`, {
      params: {
        id: id
      }
    });
  }
  getHomeletByDistrictId(id: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.HOMELET}`, {
      params: {
        id: id
      }
    });
  }
  getProvinceByName(name: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.PROVINCE}/${API.FILTER}`, {
      params: {
        name: name
      }
    });
  }
  getDistrictByName(name: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.DISTRICT}/${API.FILTER}`, {
      params: {
        name: name
      }
    });
  }
  getHomeletByName(name: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.LOCATION.LOCATION}/${API.LOCATION.HOMELET}/${API.FILTER}`, {
      params: {
        name: name
      }
    });
  }

  //address
  createAddress(data: any) {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.ADDRESS}`, data, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  updateAddress(data: any) {
    return this.http.put<BaseAPIResponse>(`${API.BASE_URL}/${API.ADDRESS}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
  deleteAddress(id: any) {
    return this.http.delete<BaseAPIResponse>(`${API.BASE_URL}/${API.ADDRESS}/${id}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
      })
    });
  }
}
