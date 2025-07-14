// src/app/services/leave-request.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LeaveRequest } from '../models/leave-request.model';
import { LeaveRequestSubmit } from '../models/leave-request.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LeaveRequestService {
private baseURL = 'http://localhost:5043/api/v7.0/LeaveRequest';


  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getByEmployee(id: number): Observable<LeaveRequest[]> {
    return this.http.get<LeaveRequest[]>(`${this.baseURL}/employee/${id}`, {
      headers: this.getAuthHeaders()
    });
  }
  // src/app/services/leave-request.ts
getEmployeeByEmail(email: string): Observable<any> {
  const token = localStorage.getItem('token') || '';
  const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  return this.http.get(`http://localhost:5043/api/v8.0/Employee/by-email/${encodeURIComponent(email)}`, { headers });
}

getAll(): Observable<LeaveRequest[]> {
  return this.http.get<LeaveRequest[]>(`http://localhost:5043/api/v7.0/LeaveRequest/GetAll`, {
    headers: this.getAuthHeaders()
  });
}


approveOrReject(req: LeaveRequest): Observable<any> {
  return this.http.put(`http://localhost:5043/api/v7.0/LeaveRequest/Approve`, req, {
    headers: this.getAuthHeaders()
  });
}

 submitLeave(request: LeaveRequestSubmit): Observable<any> {
  return this.http.post(`${this.baseURL}/Submit`, request, {
    headers: this.getAuthHeaders()
  });
}


}
