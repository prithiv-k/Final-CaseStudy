// ✅ profile.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../services/employee';
import { Employee } from '../../models/employee.model';

@Component({
  standalone: true,
  selector: 'app-profile',
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})
export class ProfileComponent implements OnInit {
  employee?: Employee;
  error = '';

  constructor(private employeeService: EmployeeService) {}
   
 ngOnInit(): void {
  console.log('🧠 ProfileComponent loaded');

  const token = localStorage.getItem('token');
  if (!token) {
    console.warn('⛔ No token found');
    return;
  }

  const payload = JSON.parse(atob(token.split('.')[1]));
  const email =
    payload?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] ||
    payload?.email || payload?.Email;

  console.log('✅ Extracted Email:', email);

  if (email) {
    this.employeeService.getEmployeeByEmail(email).subscribe({
      next: (res) => {
        console.log('✅ Employee Data:', res);
        this.employee = res;
      },
      error: (err) => {
        console.error('❌ Failed to fetch employee', err);
      }
    });
  } else {
    console.warn('⛔ Email not present in token');
  }
}

}
