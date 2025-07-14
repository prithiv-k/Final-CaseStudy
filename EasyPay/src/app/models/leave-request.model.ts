export interface LeaveRequest {
  leaveRequestId?: number;
  employeeId: number;
  startDate: string;
  endDate: string;
  status?: string;
  employee?: any;
}

// ✅ Used for employees when submitting a request
export interface LeaveRequestSubmit {
  startDate: string;
  endDate: string;
  status?: string;
}
