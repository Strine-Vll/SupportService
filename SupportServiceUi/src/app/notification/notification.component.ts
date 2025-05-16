import { Component } from '@angular/core';
import { ModalService } from '../services/modal.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { Notification } from '../interfaces/Notification';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html'
})
export class NotificationComponent {
  constructor(
    public modalService: ModalService,
    private router: Router,
    private notificationService: NotificationService,
    public authService: AuthService,
    private toastr: ToastrService
  ){}

  notifications: Notification[] = [];
  private notificationSubscription!: Subscription;
  notificationId!: number;

  ngOnInit(): void {
    this.notificationSubscription = this.notificationService.getNotifications(this.authService.getUserId())
    .subscribe(
      (notifications: Notification[]) => {
        this.notifications = notifications;
      },
      error => {
        console.error('Ошибка при получении оповещений:', error);
      }
    );
  }

  ngOnDestroy() {
    if(this.notificationSubscription) {
      this.notificationSubscription.unsubscribe();
    }
  }

  openDeleteModal(id: number) {
    this.notificationId = id;
    this.modalService.open('deleteNotification');
  }

  deleteGroup() {
    this.notificationService.deleteNotification(this.notificationId.toString()).subscribe(
      response => {
        this.modalService.close('deleteNotification');
        window.location.reload();
      },
      error => {
        this.toastr.error('Ошибка при удалении оповещения');
      }
    );
  }
}
