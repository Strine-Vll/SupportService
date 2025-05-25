import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { RequestService } from '../services/request.service';
import { ServiceRequestPreview } from '../interfaces/ServiceRequest';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-appointed-requests',
  templateUrl: './appointed-requests.component.html',
  styles: [
  ]
})
export class AppointedRequestsComponent {
  constructor(
      private router: Router,
      private authService: AuthService,
      private toastr: ToastrService,
      private requestServcie: RequestService
    ){}
  
    requests: ServiceRequestPreview[] = [];
    private requestSubscription!: Subscription;
    requestId!: number;
  
    ngOnInit(): void {
      this.requestSubscription = this.requestServcie.getRequestsForProcessing(Number(this.authService.getUserId()))
      .subscribe(
        (requests: ServiceRequestPreview[]) => {
          this.requests = requests;
        },
        error => {
          console.error('Ошибка при получении запросов:', error);
        }
      );
    }
  
    ngOnDestroy() {
      if(this.requestSubscription) {
        this.requestSubscription.unsubscribe();
      }
    }
}
