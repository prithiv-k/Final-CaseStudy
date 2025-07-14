import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LeaveRequestService } from '../../services/leave-request';
import { LeaveRequest } from '../../models/leave-request.model';

@Component({
  standalone: true,
  selector: 'app-leave-request',
  imports: [CommonModule, FormsModule],
  templateUrl: './leave-request.html',
  styleUrls: ['./leave-request.css']

})
export class LeaveRequestComponent implements OnInit {
  leaveRequests: LeaveRequest[] = [];
  newRequest = { startDate: '', endDate: '' };
  employeeId = 0;
  error = '';
  success = '';

  constructor(private leaveService: LeaveRequestService) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    let email = '';

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      email = payload?.email || payload?.Email || payload?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
    }

    if (email) {
      this.leaveService.getEmployeeByEmail(email).subscribe({
        next: (emp) => {
          this.employeeId = emp.employeeId;
          this.loadLeaves();
        },
        error: (err) => {
          console.error('❌ Could not load employee', err);
          this.error = 'Failed to load employee data.';
        }
      });
    } else {
      console.error("❌ Email not found in JWT token.");
      this.error = 'Invalid token - email missing.';
    }
  }

 loadLeaves() {
  this.leaveService.getByEmployee(this.employeeId).subscribe({
    next: (res) => this.leaveRequests = res,
    error: () => this.error = 'Failed to load leave requests.'
  });
}


  submit() {
    const payload = {
      startDate: this.newRequest.startDate,
      endDate: this.newRequest.endDate,
      status: 'Pending'
    };

    this.leaveService.submitLeave(payload).subscribe({
      next: () => {
        this.success = 'Leave request submitted successfully.';
        this.error = '';
        this.newRequest = { startDate: '', endDate: '' };
        this.loadLeaves();
      },
      error: (err) => {
        console.error('❌ Submit failed', err);
        this.error = err.error?.title || err.error || 'Submission failed.';
        this.success = '';
      }
    });
  }
}
