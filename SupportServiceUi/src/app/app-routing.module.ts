import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { HomeComponent } from './home/home.component';
import { GroupComponent } from './group/group.component';
import { RequestComponent } from './request/request.component';
import { CreateRequestComponent } from './create-request/create-request.component';
import { RoleGuard } from './auth/role.guard';
import { UserHomeComponent } from './user-home/user-home.component';
import { RequestManagementComponent } from './request-management/request-management.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { NotificationComponent } from './notification/notification.component';
import { StatsComponent } from './stats/stats.component';
import { AppointedRequestsComponent } from './appointed-requests/appointed-requests.component';
import { GroupManagementComponent } from './group-management/group-management.component';

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full'},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent },
    ]
  },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер'] } },
  { path: 'group/:id', component: GroupComponent, canActivate: [AuthGuard, RoleGuard] , 
    data: { roles: ['Специалист поддержки', 'Менеджер'] } },
  { path: 'servicerequest/create', component: CreateRequestComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер'] } },
  { path: ':groupid/servicerequest/:id', component: RequestComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер'] } },
  { path: 'requests/appointed', component: AppointedRequestsComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер'] } },
  { path: 'userhome', component: UserHomeComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: 'Пользователь' }},
  { path: 'servicerequest/:id', component: RequestComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер', 'Пользователь'] } },
  { path: 'notifications', component: NotificationComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Специалист поддержки', 'Менеджер', 'Пользователь'] } },
  { path: 'management/requests', component: RequestManagementComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: 'Менеджер' } },
  { path: 'management/users', component: UserManagementComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: 'Менеджер' } },
  { path: 'management/stats', component: StatsComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: 'Менеджер' } },
  { path: 'management/groups/:id', component: GroupManagementComponent, canActivate: [AuthGuard, RoleGuard], 
    data: { roles: 'Менеджер' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
