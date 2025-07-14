import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet } from '../models/timesheet.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TimesheetService {
  private baseURL = 'http://localhost:5043/api/v2.0/Timesheet';

  constructor(private http: HttpClient) {}

  getByEmployee(id: number): Observable<Timesheet[]> {
    return this.http.get<Timesheet[]>(`${this.baseURL}/employee/${id}/GetById`);
  }

  addOrUpdate(timesheet: Timesheet): Observable<any> {
    return this.http.post(`${this.baseURL}/Add`, timesheet);
  }
}
