import { Component } from '@angular/core';
import { GroupService } from '../services/group.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Group } from '../interfaces/Group';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styles: [
  ]
})
export class CreateGroupComponent {
  constructor
  (
    private groupService: GroupService,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){}

  form!: FormGroup;

  ngOnInit() {
    this.form = this.fb.group({
      groupName: ['', Validators.required]
    }
  );
  }

  get groupName(): FormControl {
    return this.form.get('groupName') as FormControl;
  }

 onSubmit() {
    if (this.form.valid) {
      const group: Group = { 
        id: 0,
        name: this.groupName.value 
      }

      this.groupService.createGroup(group, Number(this.authService.getUserId())).subscribe(
        response => {
          window.location.reload();
        },
        error => {
          console.error('Ошибка при создании:', error);
          this.toastr.error('Ошибка при создании группы');
        }
      );
    }
  }
}
