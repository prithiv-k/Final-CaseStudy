import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../../services/employee';
import { Employee } from '../../../models/employee.model';

@Component({
  standalone: true,
  selector: 'app-employee-management',
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-management.html',
  styleUrls: ['./employee-management.css']
})
export class EmployeeManagementComponent implements OnInit {
  employees: Employee[] = [];
  newEmployee: Employee = {
    employeeId: 0,
    name: '',
    email: '',
    department: '',
    role: '' // ✅ Add role field
  };
  editing = false;
  error = '';
  success = '';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAllEmployees().subscribe({
      next: (res) => this.employees = res,
      error: () => this.error = 'Failed to load employees'
    });
  }

  saveEmployee() {
    if (!this.newEmployee.role) {
      this.error = 'Role is required';
      return;
    }

    if (this.editing) {
      this.employeeService.updateEmployee(this.newEmployee).subscribe({
        next: () => {
          this.success = 'Employee updated successfully';
          this.cancel();
          this.loadEmployees();
        },
        error: () => this.error = 'Update failed'
      });
    } else {
      this.employeeService.addEmployee(this.newEmployee).subscribe({
        next: () => {
          this.success = 'Employee added successfully';
          this.cancel();
          this.loadEmployees();
        },
        error: () => this.error = 'Add failed'
      });
    }
  }

  edit(emp: Employee) {
    this.newEmployee = { ...emp };
    this.editing = true;
    this.success = '';
    this.error = '';
  }

  delete(emp: Employee) {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(emp).subscribe({
        next: () => {
          this.success = 'Employee deleted successfully';
          this.loadEmployees();
        },
        error: () => this.error = 'Delete failed'
      });
    }
  }

  cancel() {
    this.newEmployee = {
      employeeId: 0,
      name: '',
      email: '',
      department: '',
      role: ''
    };
    this.editing = false;
    this.error = '';
    this.success = '';
  }
}
