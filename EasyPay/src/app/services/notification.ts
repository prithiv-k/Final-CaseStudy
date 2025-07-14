import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Notification } from '../models/notification.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private baseURL = 'http://localhost:5043/api/v6.0/Notification';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token') || '';
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  // ✅ Send notification (for Manager)
  sendNotification(notification: Notification): Observable<any> {
    return this.http.post(`${this.baseURL}/Add`, notification, {
      headers: this.getAuthHeaders(),
    });
  }

  // ✅ Get all notifications for employee
  getAllByEmployee(employeeId: number): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseURL}/employee/${employeeId}`, {
      headers: this.getAuthHeaders(),
    });
  }

  // ✅ Mark as read (if implemented in backend)
  markAsRead(id: number): Observable<any> {
    return this.http.put(`${this.baseURL}/MarkAsRead/${id}`, {}, {
      headers: this.getAuthHeaders(),
    });
  }

  // ✅ (optional) Get all notifications (for Manager/Admin overview)
  getAll(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseURL}/GetAll`, {
      headers: this.getAuthHeaders(),
    });
  }
}
