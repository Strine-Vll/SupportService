import { Component, Input, SimpleChanges } from '@angular/core';
import { RequestService } from '../services/request.service';
import { UserService } from '../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserPreview } from '../interfaces/User';
import { CreateServiceRequestDto, EditServiceRequest, ServiceRequestOverview } from '../interfaces/ServiceRequest';
import { Status } from '../interfaces/Status';
import { StatusService } from '../services/status.service';
import { Group } from '../interfaces/Group';
import { AuthService } from '../services/auth.service';
import { GroupService } from '../services/group.service';

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
    private groupService: GroupService,
    public authService: AuthService,
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
  public groupId: number | null = null;
  public request!: EditServiceRequest;
  public groups: Group[] = [];
  public users: UserPreview[] = [];
  public statuses: Status[] = [];

  ngOnInit() {
    this.initForm();

    this.requestService.getEditRequest(this.requestId)
    .subscribe(request => {
      this.request = request;
      this.patchForm();
    });

    if (this.groupId)
    {
      this.userService.getGroupUsers(this.groupId).subscribe(users => {
        this.users = users;
      });
    }

    this.groupService.getGroups(Number(this.authService.getUserId())).subscribe(groups => {
      this.groups = groups;
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
      appointed: [null],
      group: [null]
    });
  }

  private patchForm() {
    this.form.patchValue({
      title: this.request.title,
      description: this.request.description,
      status: this.request.status ? this.request.status.id : null,
      appointed: this.request.appointed ? this.request.appointed.id : null,
      group: this.request.group ? this.request.group.id : null
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

  get group(): FormControl {
    return this.form.get('group') as FormControl;
  }

  onSubmit() {
    if (this.form.invalid) return;
    
    const statusId = this.form.value.status;
    const appointedId = this.form.value.appointed;
    const groupId = this.form.value.group;
    const selectedStatus = this.statuses.find(s => s.id == statusId) || null;
    const selectedAppointed = this.users.find(u => u.id == appointedId) || null;
    const selectedGroup = this.groups.find(g => g.id == groupId) || null;

    const updatedRequest: EditServiceRequest = {
      ...this.request,
      title: this.form.value.title,
      description: this.form.value.description,
      status: selectedStatus!,
      appointed: selectedAppointed,
      group: selectedGroup
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
