import { Component, Input } from '@angular/core';
import { AuditLogService } from '../services/auditlog.service';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { AuditLog } from '../interfaces/AuditLog';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-request-history-modal',
  templateUrl: './request-history-modal.component.html',
  styles: [
  ]
})
export class RequestHistoryModalComponent {
  constructor(
    private auditLogService: AuditLogService,
    public authService: AuthService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService
  ){ }

  @Input() requestId: number = 0;
  public logs: AuditLog[] = [];
  private logSubscription!: Subscription;
    
  ngOnInit() {
    this.logSubscription = this.auditLogService.getLogs(this.requestId)
    .subscribe(
      (logs: AuditLog[]) => {
        this.logs = logs;
      },
      error => {
        console.error('Ошибка при получении истории:', error);
      }
    );
  }
}
