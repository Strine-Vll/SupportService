import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Group } from '../interfaces/Group';
import { GroupService } from '../services/group.service';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';
import { ModalService } from '../services/modal.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(
    public modalService: ModalService,
    private router: Router,
    private groupService: GroupService,
    public authService: AuthService,
    private toastr: ToastrService
  ){}

  groups: Group[] = [];
  private groupSubscription!: Subscription;
  groupId!: number;  

  ngOnInit(): void {
    this.groupSubscription = this.groupService.getGroups(Number(this.authService.getUserId()))
    .subscribe(
      (groups: Group[]) => {
        this.groups = groups;
      },
      error => {
        console.error('Ошибка при получении групп:', error);
      }
    );
  }

  ngOnDestroy() {
    if(this.groupSubscription) {
      this.groupSubscription.unsubscribe();
    }
  }

  openDeleteModal(id: number) {
    this.groupId = id;
    this.modalService.open('deleteGroup');
  }

  deleteGroup() {
    this.groupService.deleteGroup(this.groupId).subscribe(
      response => {
        this.modalService.close('deleteGroup');
        window.location.reload();
      },
      error => {
        this.toastr.error('Ошибка при удалении запроса');
      }
    );
  }
}
