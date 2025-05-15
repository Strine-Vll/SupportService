import { Component, Input, SimpleChanges } from '@angular/core';
import { UserService } from '../services/user.service';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Role } from '../interfaces/Role';
import { EditUser } from '../interfaces/User';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent {
  constructor(
    private userService: UserService,
    public authService: AuthService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){ }

  form!: FormGroup;
  @Input() userId: number = 0;
  public user!: EditUser;
  public roles: Role[] = [
    { id: 1, roleName: 'Пользователь' },
    { id: 2, roleName: 'Специалист поддержки'},
    { id: 3, roleName: 'Менеджер'}
  ];

  ngOnInit() {
    this.initForm();

    this.userService.getUserToEdit(this.userId)
    .subscribe(user => {
      this.user = user;
      this.patchForm();
    });
  }
  
  ngOnChanges(changes: SimpleChanges) {
    if (changes['user'] && this.user) {
      this.patchForm();
    }
  }

  private initForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      role: [null]
    });
  }

  private patchForm() {
    this.form.patchValue({
      name: this.user.name,
      email: this.user.email,
      role: this.user.role ? this.user.role.id : null
    });
  }

  get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get role(): FormControl {
    return this.form.get('role') as FormControl;
  }

  onSubmit() {
    if (this.form.invalid) return;
    
    const roleId = this.form.value.role;
    const selectedRole = this.roles.find(r => r.id == roleId);

    this.user.name = this.form.value.name;
    this.user.email = this.form.value.email;
    this.user.role = selectedRole!;

    this.userService.editUser(this.user).subscribe({
      next: () => {
        window.location.reload();
      },
      error: err => {
        console.error('Ошибка при обновлении:', err);
        this.toastr.error('Ошибка при обновлении пользователя');
      }
    });
  }
}
