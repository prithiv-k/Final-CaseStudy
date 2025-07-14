// src/app/services/payroll.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Payroll } from '../models/payroll.model';

@Injectable({ providedIn: 'root' })
export class PayrollService {
  private baseURL = 'http://localhost:5043/api/v4.0/Payroll';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token') || '';
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

 private getEmployeeIdFromToken(): number {
  const token = localStorage.getItem('token');
  if (!token) return 0;

  const payload = JSON.parse(atob(token.split('.')[1]));
  return Number(payload?.employeeId || 0); // 👈 fix casing
}


  getPayrollForLoggedInUser(): Observable<Payroll[]> {
    const employeeId = this.getEmployeeIdFromToken();
    if (!employeeId) return of([]);

    return this.http.get<Payroll[]>(`${this.baseURL}/employee/${employeeId}`, {
      headers: this.getAuthHeaders()
    });
  }

}
