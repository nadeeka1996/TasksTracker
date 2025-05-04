import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../../core/services/http/task.service';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { MessagePopupComponent } from '../../shared/message-popup/message-popup.component';
import { TaskPopupComponent } from '../task-popup/task-popup.component';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { TaskItem, TaskItemStatus, TaskUpdateRequest } from '../../model/task.model';
import { ToolbarComponent } from '../../shared/toolbar/toolbar.component';

@Component({
  standalone: true,
  selector: 'app-task-list',
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatCheckboxModule,
    MatTooltipModule,
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatTooltipModule,
    MatCheckboxModule,
    TaskPopupComponent,
    MessagePopupComponent,
    MatSortModule,
    MatInputModule,
    MatIconModule,
    FormsModule,
    ToolbarComponent
  ],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  tasks: TaskItem[] = [];
  filteredTasks = new MatTableDataSource<TaskItem>([]);
  editingTask: TaskItem | null = null;
  TaskItemStatus = TaskItemStatus;
  displayedColumns: string[] = ['title', 'description', 'status', 'actions'];

  popupMessage: string | null = null;
  popupType: 'success' | 'error' = 'success';
  searchTerm = '';

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private taskService: TaskService, private dialog: MatDialog) {}

  ngOnInit() {
    this.fetchTasks();
    this.filteredTasks.filterPredicate = (data: TaskItem, filter: string) => {
      const title = data.title?.toLowerCase() || '';
      const description = data.description?.toLowerCase() || '';
      const status = this.statusDescriptions[data.status]?.toLowerCase() || '';

      return (
        title.includes(filter) ||
        description.includes(filter) ||
        status.includes(filter)
      );
    };
  }
  ngAfterViewInit() {
    this.sort?.sortChange.subscribe(() => {
      this.fetchTasks();
    });
  }
  fetchTasks() {
    this.taskService.getTasks().subscribe({
      next: (response) => {
        debugger;
        this.tasks = response.value;
        this.filteredTasks.data = this.tasks;
        this.filteredTasks.sort = this.sort;
      },
      error: (err) => console.error(err),
    });
  }

  onCancelEdit() {
    this.editingTask = null;
  }

  onSaveEdit(updated: TaskUpdateRequest) {
    if (!this.editingTask) return;

    const statusNumber =
      TaskItemStatus[updated.status as unknown as keyof typeof TaskItemStatus];
    this.taskService
      .updateTask(this.editingTask.id, {
        title: updated.title,
        description: updated.description,
        status: statusNumber,
      })
      .subscribe({
        next: () => {
          this.editingTask = null;
          this.fetchTasks();
        },
        error: (err) => console.error('Error saving task:', err),
      });
  }

  delete(id: string) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete',
        message: 'Are you sure you want to delete this task?',
      },
    });

    dialogRef.afterClosed().subscribe((confirm: boolean) => {
      if (confirm) {
        this.taskService.deleteTask(id).subscribe(
          {
            next: () => {
              this.fetchTasks();
              this.showSuccess('Task deleted successfully!');
            },
            error: () => this.showError('Failed to delete task.'),
          });
      }
    });
  }

  toggle(task: TaskItem) {
    debugger
    const newStatus =
      task.status === TaskItemStatus.Completed
        ? TaskItemStatus.Pending
        : TaskItemStatus.Completed;
    this.taskService
      .updateTask(task.id, {
        title: task.title,
        description: task.description,
        status: newStatus,
      })
      .subscribe(() => this.fetchTasks());
  }

  getStatusDescription(status: TaskItemStatus): string {
    return this.statusDescriptions[status];
  }

  statusDescriptions: { [key in TaskItemStatus]: string } = {
    [TaskItemStatus.Pending]: 'Not Started Yet',
    [TaskItemStatus.InProgress]: 'Work in Progress',
    [TaskItemStatus.Completed]: 'Completed',
  };

  popupVisible = false;

  openAddPopup() {
    this.editingTask = null;
    this.popupVisible = true;
  }

  edit(task: TaskItem) {
    this.editingTask = task;
    this.popupVisible = true;
  }

  onSave(task: TaskItem) {
    if (task.id) {
      this.taskService.updateTask(task.id, task).subscribe({
        next: () => {
          this.fetchTasks();
          this.showSuccess('Task updated successfully!');
        },
        error: () => this.showError('Failed to update task.'),
      });
    } else {
      this.taskService.createTask(task).subscribe({
        next: () => {
          this.fetchTasks();
          this.showSuccess('Task created successfully!');
        },
        error: () => this.showError('Failed to create task.'),
      });
    }
    this.popupVisible = false;
  }

  onCancel() {
    this.popupVisible = false;
  }

  showSuccess(msg: string) {
    this.popupType = 'success';
    this.popupMessage = msg;
  }

  showError(msg: string) {
    this.popupType = 'error';
    this.popupMessage = msg;
  }

  applyFilter(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.searchTerm = input.value;
    this.filteredTasks.filter = this.searchTerm.trim().toLowerCase();
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.filteredTasks.filter = '';
  }
}
