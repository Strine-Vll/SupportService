import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Group } from '../interfaces/Group';
import { GroupService } from '../services/group.service';
import { JwtService } from '../services/jwt.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(
    private router: Router,
    private groupService: GroupService,
    private jwtService: JwtService
  ){}

  groups: Group[] = [];
  private groupSubscription!: Subscription;

  ngOnInit(): void {
    this.groupSubscription = this.groupService.getGroups(Number(this.jwtService.getUserId()))
    .subscribe(
      (groups: Group[]) => {
        this.groups = groups;
      },
      error => {
        console.error('Ошибка при получении групп:', error);
      }
    );

  }

  ngOnDestroy(){
    if(this.groupSubscription) {
      this.groupSubscription.unsubscribe();
    }
  }
}
