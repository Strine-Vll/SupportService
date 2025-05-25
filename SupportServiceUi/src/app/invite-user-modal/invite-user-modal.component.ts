import { Component, Input } from '@angular/core';
import { UserPreview } from '../interfaces/User';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupService } from '../services/group.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, startWith, Subscription } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-invite-user-modal',
  templateUrl: './invite-user-modal.component.html',
  styles: [
  ]
})
export class InviteUserModalComponent {
constructor
  (
    private userService: UserService,
    private groupService: GroupService,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){}

  @Input() users: UserPreview[] = [];
  @Input() groupId: number = 0;
  usersToInvite: UserPreview[] = [];
  form!: FormGroup;
  private userSubscription!: Subscription;

  ngOnInit() {
    this.form = this.fb.group({
      selectedUserId: [null, Validators.required]
    });

    this.userSubscription = this.userService.getUsersToInvite(this.groupId)
    .subscribe(
      (users: UserPreview[]) => {
        this.usersToInvite = users;
      },
      error => {
        console.error('Ошибка при получении пользователей:', error);
      }
    );
  }

  onSubmit() {
    if (this.form.valid) {
      const selectedUserId = this.form.value.selectedUserId;

      const selectedUser = this.usersToInvite.find(user => user.id == selectedUserId);

      if (selectedUser) {
        const userExists = this.users.some(user => user.id == selectedUserId);

        if (!userExists) {
          this.users.push(selectedUser);
        }
      }

      this.groupService.updateUserList(this.users, this.groupId).subscribe(
        response => {
          window.location.reload();
        },
        error => {
          this.toastr.error('Ошибка при приглашении');
        }
      );
    }
  }
}
