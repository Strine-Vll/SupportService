import { Component, Input } from '@angular/core';
import { ModalService } from '../services/modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styles: [
  ]
})
export class ModalComponent {
  @Input() title: string = '';

  constructor(public modalService: ModalService) {}
}
