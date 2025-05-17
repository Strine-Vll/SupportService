import { Component, Input } from '@angular/core';
import { StatService } from '../services/stat.service';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Stat } from '../interfaces/Stat';

@Component({
  selector: 'app-request-stat-modal',
  templateUrl: './request-stat-modal.component.html',
  styles: [
  ]
})
export class RequestStatModalComponent {
  constructor(
    private statService: StatService,
    public authService: AuthService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService
  ){ }
  
  @Input() requestId: string = '';
  public stat!: Stat;

  ngOnInit() {
    this.statService.getRequestStat(Number(this.requestId))
    .subscribe(stat => {
        this.stat = stat;

        this.stat.reactionTime = this.convertTimeSpanToDate(stat.reactionTime!.toString());
        this.stat.resolutionTime = this.convertTimeSpanToDate(stat.resolutionTime!.toString());
    });
  }

  private convertTimeSpanToDate(timeSpan: string | null): Date | null {
      if (!timeSpan || timeSpan === '00:00:00') {
          return null;
      }

      const [hours, minutes, seconds] = timeSpan.split(':').map(Number);
      const date = new Date();
      date.setHours(hours, minutes, seconds);

      return date;
  }
}
