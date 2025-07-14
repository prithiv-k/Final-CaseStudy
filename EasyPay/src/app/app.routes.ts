import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';

import { ManagerDashboardComponent } from './components/manager-dashboard/manager-dashboard/manager-dashboard';
import { EmployeeManagementComponent } from './components/manager-dashboard/employee-management/employee-management';
import { LeaveManagementComponent } from './components/manager-dashboard/leave-management/leave-management';
import { NotificationManagementComponent } from './components/manager-dashboard/notification-management/notification-management';
import { EmployeeDashboardComponent } from './components/employee-dashboard/employee-dashboard';
import { LeaveRequestComponent } from './components/employee-dashboard/leave-request';
import { ProfileComponent } from './components/employee-dashboard/profile';
import { PayrollComponent } from './components/employee-dashboard/payroll';


export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
  path: 'employee',
  component: EmployeeDashboardComponent,
  children: [
    { path: '', redirectTo: 'leave', pathMatch: 'full' },
    { path: 'leave', component: LeaveRequestComponent },
    { path: 'payroll', component: PayrollComponent },
    { path: 'profile', component: ProfileComponent }
  ]
},
  
   {
    path: 'manager',
    component: ManagerDashboardComponent,
    children: [
      { path: '', redirectTo: 'employees', pathMatch: 'full' },
      { path: 'employees', component:EmployeeManagementComponent },
      { path: 'leaves',component:LeaveManagementComponent  },
      { path: 'notifications', component:NotificationManagementComponent}
    ]
  },

  { path: '**', redirectTo: 'login' }
];

