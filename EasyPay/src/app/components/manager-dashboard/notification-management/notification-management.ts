import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../../services/notification';
import { Notification } from '../../../models/notification.model';

@Component({
  standalone: true,
  selector: 'app-notification-sender',
  templateUrl: './notification-management.html',
  styleUrls: ['./notification-management.css'],
  imports: [CommonModule, FormsModule],
})
export class NotificationManagementComponent implements OnInit {
  message = '';
  employeeId: number | null = null;
  notifications: Notification[] = [];
  error = '';
  success = '';

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    // Optionally load notifications
    this.loadAllNotifications();
  }

  loadAllNotifications() {
    this.notificationService.getAll().subscribe({
      next: (data) => (this.notifications = data),
      error: () => (this.error = 'Failed to fetch notifications'),
    });
  }

  send() {
    if (!this.employeeId || !this.message.trim()) {
      this.error = 'Please enter a message and employee ID.';
      return;
    }

    const notif: Notification = {
      employeeId: this.employeeId,
      message: this.message,
      sentDate: new Date().toISOString(),
      isRead: false,
    };

    this.notificationService.sendNotification(notif).subscribe({
      next: () => {
        this.success = 'Notification sent!';
        this.error = '';
        this.message = '';
        this.employeeId = null;
        this.loadAllNotifications(); // Refresh list
      },
      error: (err) => {
        this.error = 'Failed to send notification: ' + (err.error?.title || err.message);
        this.success = '';
      },
    });
  }
}
