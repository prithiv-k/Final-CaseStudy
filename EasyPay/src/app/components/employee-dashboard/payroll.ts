import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayrollService } from '../../services/payroll';
import { Payroll } from '../../models/payroll.model';

@Component({
  standalone: true,
  selector: 'app-payroll',
  imports: [CommonModule],
  templateUrl: './payroll.html',
  styleUrls: ['./payroll.css']
})
export class PayrollComponent implements OnInit {
  payrolls: Payroll[] = [];
  error = '';

  constructor(private payrollService: PayrollService) {}

 ngOnInit(): void {
  this.payrollService.getPayrollForLoggedInUser().subscribe({
    next: (data) => {
      this.payrolls = data;
      console.log("✅ Payroll loaded:", data); // ✅ log here
    },
    error: (err) => {
      console.error("❌ Failed to load payrolls:", err);
    }
  });
}

}
