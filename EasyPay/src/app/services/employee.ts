import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model'; // ✅ Adjust path if needed

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private baseURL = 'http://localhost:5043/api/v8.0/Employee';

  constructor(private http: HttpClient) {}

  // ✅ Auth headers with JWT token
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token') || '';
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  // ✅ Get employee details by email (corrected path)
  getEmployeeByEmail(email: string): Observable<Employee> {
    return this.http.get<Employee>(
      `${this.baseURL}/by-email/${encodeURIComponent(email)}`,
      { headers: this.getAuthHeaders() }
    );
  }

  // ✅ Get all employees
  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseURL}/GetAll`, {
      headers: this.getAuthHeaders()
    });
  }

  // ✅ Add new employee
  addEmployee(emp: Employee): Observable<Employee> {
    return this.http.post<Employee>(`${this.baseURL}/Add`, emp, {
      headers: this.getAuthHeaders()
    });
  }

  // ✅ Update employee
  updateEmployee(emp: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${this.baseURL}/Update`, emp, {
      headers: this.getAuthHeaders()
    });
  }

  // ✅ Delete employee
  deleteEmployee(emp: Employee): Observable<Employee> {
    return this.http.request<Employee>('delete', `${this.baseURL}/Delete`, {
      body: emp,
      headers: this.getAuthHeaders()
    });
  }
}
