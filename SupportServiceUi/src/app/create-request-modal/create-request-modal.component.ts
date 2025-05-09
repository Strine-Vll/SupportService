import { Component } from '@angular/core';
import { RequestService } from '../services/request.service';
import { JwtService } from '../services/jwt.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateServiceRequestDto } from '../interfaces/ServiceRequest';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';
import { UserPreview } from '../interfaces/User';

@Component({
  selector: 'app-create-request-modal',
  templateUrl: './create-request-modal.component.html',
  styles: [
  ]
})
export class CreateRequestModalComponent {
  constructor(
    private requestService: RequestService,
    private userService: UserService,
    private jwtService: JwtService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){
    this.activateRoute.params.subscribe(params => {
      this.groupId = params['id'];
    });
  }

  form!: FormGroup;
  public groupId: number = 0;
  public users: UserPreview[] = [];
  
  ngOnInit() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      appointed: [null]
    });

    this.userService.getGroupUsers(this.groupId).subscribe(users => {
      this.users = users;
    });
  }

  get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  get description(): FormControl {
    return this.form.get('description') as FormControl;
  }

  get appointed(): FormControl {
    return this.form.get('appointed') as FormControl;
  }

  onSubmit() {
    if (this.form.valid) {
      const serviceRequest: CreateServiceRequestDto = {
        title: this.title.value,
        description: this.description.value,
        appointedId: this.appointed.value || null,
        groupId: this.groupId,
        createdById: Number(this.jwtService.getUserId())
      }

      this.requestService.createRequest(serviceRequest).subscribe(
        response => {
          window.location.reload();
        },
        error => {
          console.error('Ошибка при создании:', error);
          this.toastr.error('Ошибка при создании запроса');
        }
      );
    }
  }
}


