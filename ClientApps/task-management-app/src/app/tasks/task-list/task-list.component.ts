import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../core/services/http/task.service';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormsModule } from '@angular/forms';
import { TaskCreateRequest, TaskItem, TaskItemStatus } from '../../model/task.model';
import { ToolbarComponent } from '../../shared/toolbar/toolbar.component';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  standalone: true,
  selector: 'app-task-list',
  imports: [
    MatToolbarModule,
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    ToolbarComponent,
    MatButtonModule,
    MatInputModule
],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  taskData: TaskItem[] = [];
  currentPage = 1;
  TaskItemStatus = TaskItemStatus;

  title: string = '';
  description: string = '';

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.fetchTasks();
  }

  fetchTasks(): void {
    this.taskService.getTasks(this.currentPage).subscribe({
      next: (response) => {
        this.taskData = response.value;
      },
      error: (err) => console.error(err),
    });
  }

  hasNextPage(): boolean {
    return this.taskData.length === 5;
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.fetchTasks();
    }
  }

  goToNextPage(): void {
    this.currentPage++;
    this.fetchTasks();
  }

  addTaskItem(): void {
    const newTask: TaskCreateRequest = {
      title: this.title,
      description: this.description,
      status: TaskItemStatus.Pending,
    };
    this.title = '';
    this.description = '';

    this.taskService.createTask(newTask).subscribe({
      next: () => this.fetchTasks(),
      error: () => {},
    });
  }

  markAsDone(task: TaskItem): void {
    const updatedTask = {
      ...task,
      status: TaskItemStatus.Completed,
    };

    this.taskService.updateTask(task.id, updatedTask).subscribe({
      next: () => {
        this.taskService.getTasks(this.currentPage).subscribe({
          next: (response) => {
            if (response.value.length === 0 && this.currentPage > 1) {
              this.currentPage--;
              this.fetchTasks();
            } else {
              this.taskData = response.value;
            }
          },
          error: () => {},
        });
      },
      error: () => {},
    });
  }
}
