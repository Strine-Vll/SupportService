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

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full'},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent },
    ]
  },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'group/:id', component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'servicerequest/create', component: CreateRequestComponent, canActivate: [AuthGuard] },
  { path: ':groupid/servicerequest/:id', component: RequestComponent, canActivate: [AuthGuard] }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
