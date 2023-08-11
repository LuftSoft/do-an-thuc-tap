import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API } from 'src/app/core/constant/API';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(
    private http: HttpClient
  ) { }
  getAllBrand(): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.SHOP.BRAND}`); }
  getAllProduct(): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.SHOP.PHONE}`); }
  getOneProduct(id: string): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.SHOP.PHONE}/${id}`); }
  createProduct(data: any): Observable<any> { return this.http.post<any>(`${API.BASE_URL}/${API.SHOP.PHONE}`, { FormData: data }); }
  updateProduct(data: any): Observable<any> { return this.http.put<any>(`${API.BASE_URL}/${API.SHOP.PHONE}`, data); }
  deleteProduct(id: string): Observable<any> { return this.http.delete<any>(`${API.BASE_URL}/${API.SHOP.PHONE}/${id}`); }
  //
  getAllOrder(): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.SHOP.ORDER}`); }
  getOneOrder(): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.SHOP.ORDER}`); }
  createOrder(data: any): Observable<any> { return this.http.post<any>(`${API.BASE_URL}/${API.SHOP.ORDER}`, data); }
  updateOrderPaymentStatus(data: any): Observable<any> { return this.http.put<any>(`${API.BASE_URL}/${API.SHOP.ORDER}`, data); }
  updateOrderStatus(data: any): Observable<any> { return this.http.put<any>(`${API.BASE_URL}/${API.SHOP.ORDER}/${API.SHOP.STATUS}`, data); }
  deleteOrder(): Observable<any> { return this.http.delete<any>(`${API.BASE_URL}/${API.SHOP.ORDER}`); }
  //
  getUserDetail(id: string): Observable<any> { return this.http.get<any>(`${API.BASE_URL}/${API.USER}/${id}`); }

}
