import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ValidationError } from 'src/app/interfaces/validationError';
import { LoginResponse } from 'src/app/interfaces/LoginResponce';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})

export class LoginComponent implements OnInit{

  constructor(
    private service: UserService,
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService
  ){

  }

  ngOnInit(): void {
    if(this.authService.isAuthenticated())
    {
      if(this.authService.hasAnyRole(['Специалист поддержки', 'Менеджер']))
      {
        this.router.navigateByUrl('/home');
      } 
      else if(this.authService.hasRole('Пользователь'))
      {
        this.router.navigateByUrl('/userhome');
      }
    }
    else
    {
      localStorage.removeItem('token');
      this.router.navigate(['/user/login']);
    }
  }

  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', Validators.required)
  });

  get email(){
    return this.form.controls.email as FormControl;
  }
  get password(){
    return this.form.controls.password as FormControl;
  }

  onSubmit(){
    this.service.login(this.email.value, this.password.value)
    .pipe(
      catchError(this.handleError)
    )
    .subscribe(
      (response) => {
        const token = (response as { token: string }).token;
        localStorage.setItem('token', token);
        if(this.authService.hasAnyRole(['Специалист поддержки', 'Менеджер']))
        {
          this.router.navigateByUrl('/home');
        } 
        else if(this.authService.hasRole('Пользователь'))
        {
          this.router.navigateByUrl('/userhome');
        }
      },
      (error) => {
        console.error('Ошибка при авторизации:', error);
        if (error.error && error.error instanceof Array) {
          error.error.forEach((err: ValidationError) => {
            this.toastr.error(err.errorMessage, 'Ошибка авторизации');
          });
        } else {
          this.toastr.error('Произошла ошибка при авторизации', 'Ошибка');
        }
      }
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('Ошибка на стороне клиента:', error.error.message);
    } else {
      console.error('Ошибка на стороне сервера:', error.status, error.error);
    }
    return throwError(error);
  }

}