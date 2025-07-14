import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveRequestService } from '../../../services/leave-request';
import { LeaveRequest } from '../../../models/leave-request.model';
import { HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-leave-requests',
  imports: [CommonModule, HttpClientModule],
  templateUrl: './leave-management.html',
  styleUrls: ['./leave-management.css']
})
export class LeaveManagementComponent implements OnInit {
  requests: LeaveRequest[] = [];
  error = '';
  success = '';

  constructor(private leaveService: LeaveRequestService) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll() {
    this.leaveService.getAll().subscribe({
      next: (res) => {
        this.requests = res;
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to load leave requests';
      }
    });
  }

  updateStatus(req: LeaveRequest, status: string) {
  if (!req.employeeId) {
    this.error = 'Missing employeeId for request ID ' + req.leaveRequestId;
    return;
  }

  const updated: LeaveRequest = {
    leaveRequestId: req.leaveRequestId!,
    employeeId: req.employeeId,
    startDate: req.startDate,
    endDate: req.endDate,
    status: status
  };

  console.log("📦 Sending Payload to Backend:", updated); // 👈 Add this

  this.leaveService.approveOrReject(updated).subscribe({
    next: () => {
      req.status = status;
      this.success = `Request ${status}`;
      this.error = '';
    },
    error: (err) => {
      console.error('❌ Backend error:', err);
      this.error = 'Update failed: ' + (err.error?.title || err.message || err.statusText);
    }
  });
}

}
