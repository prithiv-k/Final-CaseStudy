export interface Notification {
  notificationId?: number;
  employeeId: number;
  message: string;
  sentDate: string;
  isRead: boolean;
}
