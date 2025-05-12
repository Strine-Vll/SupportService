import { Component, Input } from '@angular/core';
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
  form!: FormGroup;
  @Input() requestId!: number;

  constructor(
    private requestService: RequestService,
    public authService: AuthService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){ }

  ngOnInit() {
    this.form = this.fb.group({
      satisfaction: [5, Validators.required]  // Устанавливаем начальное значение 5
    });
  }

  get satisfaction(): FormControl {
    return this.form.get('satisfaction') as FormControl;
  }

  onRangeChange(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.satisfaction.setValue(value);
  }

  onSubmit() {
    if (this.form.valid) {
      this.requestService.closeRequest(this.requestId, this.satisfaction.value).subscribe(
        response => {
          window.location.reload();
        },
        error => {
          console.error('Ошибка при закрытии:', error);
          this.toastr.error('Ошибка при закрытии запроса');
        }
      );
    }
  }
}