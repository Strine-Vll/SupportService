import { Component, SimpleChanges } from '@angular/core';
import { ModalService } from '../services/modal.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';
import { UserPreview } from '../interfaces/User';
import { Subscription } from 'rxjs';
import { GroupService } from '../services/group.service';
import { Group } from '../interfaces/Group';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-group-management',
  templateUrl: './group-management.component.html',
  styles: [
  ]
})
export class GroupManagementComponent {
  constructor(
    public modalService: ModalService,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private userServcie: UserService,
    private groupService: GroupService,
    private activateRoute: ActivatedRoute,
    private fb: FormBuilder
  ){}

  users: UserPreview[] = [];
  group!: Group;
  private userSubscription!: Subscription;
  private groupSubscription!: Subscription;
  groupId: number = 0;
  originalGroupName!: string;
  form!: FormGroup;

  ngOnInit(): void {
    this.activateRoute.params.subscribe(params => {
      this.groupId = params['id'];
    });

    this.form = this.fb.group({
      groupName: ['', Validators.required]
    });

    this.userSubscription = this.userServcie.getGroupUsers(this.groupId)
    .subscribe(
      (users: UserPreview[]) => {
        this.users = users;
      },
      error => {
        console.error('Ошибка при получении пользователей:', error);
      }
    );
    this.groupSubscription = this.groupService.getGroup(this.groupId)
    .subscribe(
      (group: Group) => {
        this.group = group;

        this.form.patchValue({
          groupName: group.name
        });
      },
      error => {
        console.error('Ошибка при получении группы:', error);
      }
    );
  }

  get groupName(): FormControl {
    return this.form.get('groupName') as FormControl;
  }

  updateGroup() {
    if (this.form.valid) {
      this.group.name = this.form.value.groupName;

      this.groupService.updateGroup(this.group).subscribe(
        response => {
          window.location.reload();
        },
        error => {
          console.error('Ошибка при создании:', error);
          this.toastr.error('Ошибка при создании группы');
        }
      );
    }
  }

  removeUser(userId: number) {
    this.users = this.users.filter(user => user.id !== userId);

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
