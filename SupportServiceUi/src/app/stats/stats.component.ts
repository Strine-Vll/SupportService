import { Component } from '@angular/core';
import { StatService } from '../services/stat.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Stat, StatFilter } from '../interfaces/Stat';
import { UserPreview } from '../interfaces/User';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styles: [
  ]
})
export class StatsComponent {
  constructor(
    private statService: StatService,
    private userService: UserService,
    private activateRoute: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){ }

  form!: FormGroup;
  public requestId: number = 0;
  public groupId: number | null = null;
  public satisfactionIndex: number | null = null;
  public reescalateAmount: number | null = null;
  public reactionTime: Date | null = null;
  public resolutionTime: Date | null = null;
  public stats: Stat[] = [];
  public users: UserPreview[] = [];

  ngOnInit() {
    this.form = this.fb.group({
        startDate: ['', Validators.required],
        endDate: ['', Validators.required],
        user: [null]
    });

    this.userService.getActiveUsers().subscribe(users => {
      this.users = users;
    });
  }

  get title(): FormControl {
    return this.form.get('startDate') as FormControl;
  }

  get description(): FormControl {
    return this.form.get('endDate') as FormControl;
  }

  get appointed(): FormControl {
    return this.form.get('user') as FormControl;
  }

  calculateAverages() {
    if (this.stats.length === 0) {
      this.satisfactionIndex = null;
      this.reescalateAmount = null;
      this.reactionTime = null;
      this.resolutionTime = null;
      return;
    }

    const sum = this.stats.reduce(
      (acc, stat) => {
        acc.satisfactionIndex += stat.satisfactionIndex ?? 0;
        acc.reescalateAmount += stat.reescalateAmount ?? 0;

        acc.reactionTime += stat.reactionTime ? stat.reactionTime.getTime() : 0;
        acc.resolutionTime += stat.resolutionTime ? stat.resolutionTime.getTime() : 0;

        return acc;
      },
      { satisfactionIndex: 0, reescalateAmount: 0, reactionTime: 0, resolutionTime: 0 }
    );

    const count = this.stats.length;
    this.satisfactionIndex = sum.satisfactionIndex / count;
    this.reescalateAmount = sum.reescalateAmount / count;

    this.reactionTime = sum.reactionTime > 0 ? new Date(sum.reactionTime / count) : null;
    this.resolutionTime = sum.resolutionTime > 0 ? new Date(sum.resolutionTime / count) : null;
  }

  onSubmit() {
    if (this.form.invalid) return;

    console.log('Form Values:', this.form.value);

    const startDateStr = this.form.value.startDate;
    const endDateStr = this.form.value.endDate;

    const statFilter: StatFilter = {
        startDate: startDateStr ? new Date(startDateStr) : null,
        endDate: endDateStr ? new Date(endDateStr) : null,
        userId: this.form.value.user ?? null
    };

    console.log(statFilter);

    this.statService.filterStat(statFilter)
    .subscribe(stats => {
        this.stats = stats.map(stat => ({
            ...stat,
            reactionTime: this.convertTimeSpanToDate(stat.reactionTime!.toString()),
            resolutionTime: this.convertTimeSpanToDate(stat.resolutionTime!.toString())
        }));
        this.calculateAverages();
    }, error => {
        console.error('Ошибка:', error);
        this.toastr.error('Ошибка при получении данных');
    });
}

  private convertTimeSpanToDate(timeSpan: string | null): Date | null {
    if (!timeSpan || timeSpan === '00:00:00') {
      return null;
    }
    const [hours, minutes, seconds] = timeSpan.split(':').map(Number);
    const date = new Date();
    date.setHours(hours, minutes, seconds);
    return date;
  }
}
