import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { TaskCreateRequest, TaskItem, TaskUpdateRequest } from '../../../model/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private readonly apiUrl = `${environment.apiUrl}/tasks`;
  constructor(private http: HttpClient) {}

  getTasks(pageNumber:number): Observable<any> {
    return this.http.get<TaskItem[]>(`${this.apiUrl}/`, {
      params: { pageNumber: pageNumber.toString() }
    });
  }

  getTask(id: string): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/${id}`);
  }

  createTask(request: TaskCreateRequest): Observable<string> {
    return this.http.post<string>(this.apiUrl, request);
  }

  updateTask(id: string, request: TaskUpdateRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteTask(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

