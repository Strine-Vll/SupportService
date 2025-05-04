import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestService } from '../services/request.service';
import { JwtService } from '../services/jwt.service';
import { ServiceRequestPreview } from '../interfaces/ServiceRequest';
import { Subscription, switchMap } from 'rxjs';
import { ModalService } from '../services/modal.service';
import { Pipe, PipeTransform } from '@angular/core';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styles: [
  ]
})
export class GroupComponent {
  constructor(
    private route: ActivatedRoute,
    public modalService: ModalService,
    private router: Router,
    private requestService: RequestService,
    private jwtService: JwtService
  ){}

  groupId!: string;
  request!: ServiceRequestPreview;
  requests: ServiceRequestPreview[] = [];
  private requestSubscription!: Subscription;
  sortAscending = true;
  statuses: string[] = ['Новое', 'В работе', 'Не закрыта', 'Решено', 'Закрыто']
  
  /*parseDateString(dateString: string): Date {
    const [time, dayMonthYear] = dateString.split(' ');
    const [day, month, year] = dayMonthYear.split('-');
    const [hours, minutes] = time.split(':');
  
    const formattedDateString = `${month}-${day}-${year} ${hours}:${minutes}`;
    return new Date(formattedDateString);
  }*/

  ngOnInit(): void {
    this.groupId = this.route.snapshot.params['id'];
  
    this.requestSubscription = this.requestService.getRequests(parseInt(this.groupId))
      .subscribe(
        (requests: ServiceRequestPreview[]) => {
          this.requests = requests;
        },
        error => {
          console.error('Ошибка при получении групп:', error);
        }
      );
  }

  getRequestsByStatus(status: string) {
    return this.requests.filter(request => request.status === status);
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
