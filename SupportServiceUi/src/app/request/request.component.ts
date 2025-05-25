import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestService } from '../services/request.service';
import { AuthService } from '../services/auth.service';
import { ServiceRequestOverview } from '../interfaces/ServiceRequest';
import { Subscription, switchMap } from 'rxjs';
import { ModalService } from '../services/modal.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styles: [
  ]
})
export class RequestComponent {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public modalService: ModalService,
    private requestService: RequestService,
    public authService: AuthService,
    private toastr: ToastrService,
  ){}

  requestId!: string;
  groupId: string | null = null;
  request!: ServiceRequestOverview;
  private requestSubscription!: Subscription;

  /*parseDateString(dateString: string): Date {
    const [time, dayMonthYear] = dateString.split(' ');
    const [day, month, year] = dayMonthYear.split('-');
    const [hours, minutes] = time.split(':');
  
    const formattedDateString = `${month}-${day}-${year} ${hours}:${minutes}`;
    return new Date(formattedDateString);
  }*/

  ngOnInit(): void {
    this.requestId = this.route.snapshot.params['id'];
    this.groupId = this.route.snapshot.params['groupid'];
  
    this.requestSubscription = this.requestService.getRequest(parseInt(this.requestId))
      .subscribe(
        (request: ServiceRequestOverview) => {
          this.request = request;
        },
        error => {
          console.error('Ошибка при получении групп:', error);
        }
      );
  }

  ngOnDestroy() {
    if(this.requestSubscription) {
      this.requestSubscription.unsubscribe();
    }
  }

  deleteRequest() {
    this.requestService.deleteRequest(this.requestId).subscribe(
      response => {
        this.modalService.close('deleteRequest');
        if(this.groupId && this.groupId.length > 0) {
          this.router.navigate(['/group', this.groupId]);
        }
        else {
          this.router.navigate(['/home']);
        }
      },
      error => {
        this.toastr.error('Ошибка при удалении запроса');
      }
    );
  }

  /*onDelete() {
    this.goalService.deleteGoal(this.groupId).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      error => {
        console.error('Ошибка при удалении транзакции:', error);
      }
    );
  }*/
}
