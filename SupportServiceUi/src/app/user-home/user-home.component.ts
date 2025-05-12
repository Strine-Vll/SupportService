import { Component, OnDestroy, OnInit } from '@angular/core';
import { ModalService } from '../services/modal.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { ServiceRequestPreview } from '../interfaces/ServiceRequest';
import { Subscription } from 'rxjs';
import { RequestService } from '../services/request.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styles: [
  ]
})
export class UserHomeComponent implements OnInit, OnDestroy {
  constructor(
    public modalService: ModalService,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private requestServcie: RequestService
  ){}

  requests: ServiceRequestPreview[] = [];
  private requestSubscription!: Subscription;
  requestId!: number;

  ngOnInit(): void {
    this.requestSubscription = this.requestServcie.getUserRequests(Number(this.authService.getUserId()))
    .subscribe(
      (requests: ServiceRequestPreview[]) => {
        this.requests = requests;
      },
      error => {
        console.error('Ошибка при получении групп:', error);
      }
    );
  }

  openCloseRequestModal(requestId: number) {
    this.requestId = requestId;
    this.modalService.open('closeRequest');
  }

  ngOnDestroy() {
    if(this.requestSubscription) {
      this.requestSubscription.unsubscribe();
    }
  }
}
