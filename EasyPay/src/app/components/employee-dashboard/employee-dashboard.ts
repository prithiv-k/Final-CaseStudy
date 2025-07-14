import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-employee-dashboard',
  templateUrl: './employee-dashboard.html',
  styleUrls: ['./employee-dashboard.css'],
  imports: [RouterOutlet, RouterModule], // ✅ Add RouterModule here
})
export class EmployeeDashboardComponent {
  logout() {
    localStorage.clear();
    location.href = '/login';
  }
}
