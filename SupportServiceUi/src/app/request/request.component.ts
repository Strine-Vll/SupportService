import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestService } from '../services/request.service';
import { JwtService } from '../services/jwt.service';
import { ServiceRequestOverview } from '../interfaces/ServiceRequest';
import { Subscription, switchMap } from 'rxjs';

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
    private requestService: RequestService,
    private jwtService: JwtService
  ){}

  requestId!: string;
  groupId!: string;
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

  ngOnDestroy(){
    if(this.requestSubscription) {
      this.requestSubscription.unsubscribe();
    }
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
