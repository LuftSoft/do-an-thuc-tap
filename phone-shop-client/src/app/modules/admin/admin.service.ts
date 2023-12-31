import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API } from 'src/app/core/constant/API';
import { BaseAPIResponse, CONFIG } from 'src/app/core/constant/CONFIG';

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
  createProduct(data: any): Observable<any> {
    console.log(data);
    let _headers = new HttpHeaders();
    _headers.set('Content-Type', 'multipart/form-data');
    return this.http.post<any>(`${API.BASE_URL}/${API.SHOP.PHONE}`,
      data,
      { headers: _headers })
      ;
  }
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
  //warehouuse
  getAllSupplier(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.SUPPILER}`);
  }
  createWarehouseTicket(data: any) {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.WAREHOUSE}`, data,
      {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.USER_ACCESS_TOKEN)}`
        })
      });
  }
  getAllTicket() {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.WAREHOUSE}`);
  }
  getOneTicket(id: string) {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.WAREHOUSE}/${id}`);
  }
  //dashboard
  getDashBoard(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.PHONE}/${API.DASHBOARD}`);
  }
  //report
  getDayReport(): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.THIRTY_DAY}`);
  }
  getMonthReport(year: number): Observable<BaseAPIResponse> {
    return this.http.get<BaseAPIResponse>(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.MONTH}/${year}`);
  }
  postRangeDayReport(startDate: any, endDate: any): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.RANGE_DAY}`, {
      startDate: startDate,
      endDate: endDate
    });
  }
  getUserInfoWithToken(token: string): any {
    return this.http.get<any>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SELF_INFORMATION}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`
      })
    });
  }
  getUserInfo(): any {
    return this.http.get<any>(`${API.BASE_URL}/${API.USER}/${API.AUTH.SELF_INFORMATION}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem(CONFIG.AUTH.ADMIN_ACCESS_TOKEN)}`
      })
    });
  }
  exportDayReport(): Observable<any> {
    return this.http.get(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.PDF}/${API.REPORT.THIRTY_DAY}`, { responseType: 'blob' });
  }
  exportMonthReport(year: number) {
    return this.http.get(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.PDF}/${API.REPORT.MONTH}/${year}`, { responseType: 'blob' });
  }
  exportRabgeDayReport(startDate: any, endDate: any) {
    return this.http.post(`${API.BASE_URL}/${API.REPORT.REPORT}/${API.REPORT.PDF}/${API.REPORT.RANGE_DAY}`, {
      startDate: startDate,
      endDate: endDate
    }, { responseType: 'blob' });
  }
  //supplier
  createSupplier(data: any): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(`${API.BASE_URL}/${API.SHOP.SUPPILER}`, data);
  }
}
