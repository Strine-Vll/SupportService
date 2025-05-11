import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { RequestService } from '../services/request.service';

@Component({
  selector: 'app-close-request-modal',
  templateUrl: './close-request-modal.component.html',
  styles: [
  ]
})
export class CloseRequestModalComponent {
  constructor(
      private requestService: RequestService,
      public authService: AuthService,
      private activateRoute: ActivatedRoute,
      private toastr: ToastrService,
      private fb: FormBuilder
    ){ }

  public ratings = [
    { value: 5, half: false },
    { value: 4.5, half: true },
    { value: 4, half: false },
    { value: 3.5, half: true },
    { value: 3, half: false },
    { value: 2.5, half: true },
    { value: 2, half: false },
    { value: 1.5, half: true },
    { value: 1, half: false },
    { value: 0.5, half: true }
  ];
  form!: FormGroup;
  public requestId!: number;

  ngOnInit() {
    this.form = this.fb.group({
      satisfaction: [null, Validators.required]
    });
  }

  get satisfaction(): FormControl {
    return this.form.get('satisfaction') as FormControl;
  }

  onSubmit() {
    if (this.form.valid) {
      this.requestService.closeRequest(this.requestId.toString(), this.satisfaction.value).subscribe(
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
