import { Component } from '@angular/core';
import { UserPreview } from '../interfaces/User';
import { ModalService } from '../services/modal.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styles: [
  ]
})
export class UserManagementComponent {
  constructor(
    public modalService: ModalService,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private userServcie: UserService
  ){}
    
  users: UserPreview[] = [];
  private userSubscription!: Subscription;
  userId!: number;

  ngOnInit(): void {
    this.userSubscription = this.userServcie.getUsersToManage()
    .subscribe(
      (requests: UserPreview[]) => {
        this.users = requests;
      },
      error => {
        console.error('Ошибка при получении пользователей:', error);
      }
    );
  }
  
  openEditModal(userId: number) {
    this.userId = userId;
    this.modalService.open('editUser');
  }

  openDeactivateModal(userId: number) {
    this.userId = userId;
    this.modalService.open('deactivateUser');
  }

  deactivateUser() {
    this.userServcie.deactivateUser(this.userId).subscribe(
      response => {
        this.modalService.close('deactivateUser');
        window.location.reload();
      },
      error => {
        this.toastr.error('Ошибка при деактивации пользователя');
      }
    );
  }

  ngOnDestroy() {
    if(this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }
}
