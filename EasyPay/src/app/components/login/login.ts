// src/app/components/login/login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';
import { User } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  user: User = { email: '', password: '',role:'' };
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

 login() {
  this.authService.login(this.user).subscribe({
    next: (res) => {
      this.authService.storeToken(res.token, res.role);

      // Redirect based on role
    if (res.role === 'Employee') {
  this.router.navigate(['/employee']);
} else if (res.role === 'Manager') {
  this.router.navigate(['/manager']);
} else {
  alert('Unsupported role');
}

    },
    error: (err) => {
      alert(err.error || 'Login failed!');
    }
  });
}

}
