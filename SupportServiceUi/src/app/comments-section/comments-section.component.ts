import { Component, Input } from '@angular/core';
import { CommentService } from '../services/comment.service';
import { JwtService } from '../services/jwt.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Comment, CommentToCreate } from '../interfaces/Comment';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-comments-section',
  templateUrl: './comments-section.component.html'
})
export class CommentsSectionComponent {
  @Input() serviceRequestId!: string;
  private commentsSubscription!: Subscription;
  comments: Comment[] = [];
  form!: FormGroup;
  selectedFiles: File[] = [];
  isUploading = false;

  readonly maxFilesCount = 10;
  readonly maxFileSizeMB = 10;

  constructor
  (
    private commentService: CommentService,
    private jwtService: JwtService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ){}
  
  ngOnInit() {
    this.form = this.fb.group({
      message: ['', Validators.required]
    });

    this.loadComments();
  }

  get message(): FormControl {
    return this.form.get('message') as FormControl;
  }

  onFilesSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files) {
      this.selectedFiles = [];
      return;
    }

    const filesArray = Array.from(input.files);

    if (filesArray.length > this.maxFilesCount) {
      this.toastr.error(`Максимум можно загрузить ${this.maxFilesCount} файлов`, 'Ошибка при создании комментария');
      this.selectedFiles = [];
      input.value = '';
      return;
    }
    
    for (const file of filesArray) {
      if (file.size > this.maxFileSizeMB * 1024 * 1024) {
        this.toastr.error(`Файл "${file.name}" превышает максимальный размер ${this.maxFileSizeMB} МБ`, 'Ошибка при создании комментария');
        this.selectedFiles = [];
        input.value = '';
        return;
      }
    }

    this.selectedFiles = filesArray;
  }
  
  onSubmit() {
    if (this.form.valid) {
      this.isUploading = true;

      const comment: CommentToCreate = {
        message: this.message.value,
        createdById: Number(this.jwtService.getUserId()),
        serviceRequestId: Number(this.serviceRequestId)
      }

      const formData = new FormData();
      formData.append('Message', comment.message);
      formData.append('ServiceRequestId', comment.serviceRequestId.toString());
      formData.append('CreatedById', comment.createdById.toString());

      this.selectedFiles.forEach((file, index) => {
        formData.append('attachments', file, file.name);
      });

      this.commentService.createComment(formData).subscribe(
        response => {
          this.form.reset();
          this.selectedFiles = [];
          this.isUploading = false;
          this.loadComments();
        },
        error => {
          this.isUploading = false;
          this.toastr.error(error, 'Ошибка при создании комментария');
        }
      );
    }
  }

  loadComments() {
    this.commentsSubscription = this.commentService.getComments(this.serviceRequestId)
    .subscribe(
      (comments: Comment[]) => {
        this.comments = comments;
      },
      error => {
        this.toastr.error(error, 'Ошибка при получении комментариев');
      }
    );
  }

  ngOnDestroy() {
    this.commentsSubscription?.unsubscribe();
  }
}
