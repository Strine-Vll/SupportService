import { Component, Input, SimpleChanges } from '@angular/core';
import { RequestService } from '../services/request.service';
import { UserService } from '../services/user.service';
import { JwtService } from '../services/jwt.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserPreview } from '../interfaces/User';
import { CreateServiceRequestDto, EditServiceRequest, ServiceRequestOverview } from '../interfaces/ServiceRequest';
import { Status } from '../interfaces/Status';
import { StatusService } from '../services/status.service';

@Component({
  selector: 'app-edit-request-modal',
  templateUrl: './edit-request-modal.component.html',
  styles: [
  ]
})
export class EditRequestModalComponent {
  constructor(
    private requestService: RequestService,
    private userService: UserService,
    private statusService: StatusService,
    private jwtService: JwtService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){
    this.activateRoute.params.subscribe(params => {
      this.requestId = params['id'];
      this.groupId = params['groupid'];
    });
  }

  form!: FormGroup;
  public requestId: number = 0;
  public groupId: number = 0;
  public request!: EditServiceRequest;
  public users: UserPreview[] = [];
  public statuses: Status[] = [];

  ngOnInit() {
    this.initForm();

    this.requestService.getEditRequest(this.requestId)
    .subscribe(request => {
      this.request = request;
      this.patchForm();
    });

    this.userService.getGroupUsers(this.groupId).subscribe(users => {
      this.users = users;
    });
    
    this.statusService.getStatuses().subscribe(statuses => {
      this.statuses = statuses;
    });
  }
  
  ngOnChanges(changes: SimpleChanges) {
    if (changes['request'] && this.request) {
      this.patchForm();
    }
  }

  private initForm() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: ['', Validators.required],
      appointed: [null]
    });
  }

  private patchForm() {
    this.form.patchValue({
      title: this.request.title,
      description: this.request.description,
      status: this.request.status ? this.request.status.id : null,
      appointed: this.request.appointed ? this.request.appointed.id : null
    });
  }

  get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  get description(): FormControl {
    return this.form.get('description') as FormControl;
  }

  get status(): FormControl {
    return this.form.get('status') as FormControl;
  }

  get appointed(): FormControl {
    return this.form.get('appointed') as FormControl;
  }

  onSubmit() {
    if (this.form.invalid) return;
    
    const statusId = this.form.value.status;
    const appointedId = this.form.value.appointed;
    console.log(statusId, this.statuses);
    const selectedStatus = this.statuses.find(s => s.id == statusId) || null;
    const selectedAppointed = this.users.find(u => u.id == appointedId) || null;

    const updatedRequest: EditServiceRequest = {
      ...this.request,
      title: this.form.value.title,
      description: this.form.value.description,
      status: selectedStatus!,
      appointed: selectedAppointed!
    };

    this.requestService.editRequest(updatedRequest).subscribe({
      next: () => {
        window.location.reload();
      },
      error: err => {
        console.error('Ошибка при обновлении:', err);
        this.toastr.error('Ошибка при обновлении запроса');
      }
    });
  }
}
