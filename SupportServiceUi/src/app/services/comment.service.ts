import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Comment, CommentToCreate } from '../interfaces/Comment';

@Injectable({
  providedIn: 'root'
})

export class CommentService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getComments(requestId: string): Observable<Comment[]> {
    let params = new HttpParams().set('requestId', requestId);

    return this.http.get<Comment[]>(this.baseUrl + '/Comment/RequestComments', { params })
    .pipe(
      map(data => data)
    );
  }

  createComment(formData: FormData): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/comment`, formData).pipe(
        catchError(error => {
            console.error('Ошибка отправки комментария:', error);
            return throwError(error);
        })
    );
  }
}