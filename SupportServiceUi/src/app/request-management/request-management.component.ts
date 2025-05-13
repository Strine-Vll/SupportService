import { Component } from '@angular/core';
import { ModalService } from '../services/modal.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { RequestService } from '../services/request.service';
import { ServiceRequestPreview } from '../interfaces/ServiceRequest';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-request-management',
  templateUrl: './request-management.component.html',
  styles: [
  ]
})
export class RequestManagementComponent {
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
    this.requestSubscription = this.requestServcie.getUnallocatedRequests()
    .subscribe(
      (requests: ServiceRequestPreview[]) => {
        this.requests = requests;
      },
      error => {
        console.error('Ошибка при получении запросов:', error);
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
