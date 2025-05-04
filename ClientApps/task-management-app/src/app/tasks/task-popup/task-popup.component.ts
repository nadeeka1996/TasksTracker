import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TaskItemStatus } from '../../model/task.model';
import { TaskItem } from '../../model/task.model';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

@Component({
  selector: 'app-task-popup',
  templateUrl: './task-popup.component.html',
  styleUrls: ['./task-popup.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatFormFieldModule,
    ReactiveFormsModule,
  ],
})
export class TaskPopupComponent {
  @Input() task: TaskItem | null = null;
  @Output() save = new EventEmitter<TaskItem>();
  @Output() cancel = new EventEmitter<void>();

  taskForm: FormGroup;

  statusOptions = [
    { label: 'Pending', value: TaskItemStatus.Pending },
    { label: 'In Progress', value: TaskItemStatus.InProgress },
    { label: 'Completed', value: TaskItemStatus.Completed },
  ];

  constructor(private fb: FormBuilder) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: [TaskItemStatus.Pending, Validators.required],
    });
  }

  ngOnChanges(): void {
    if (this.task) {
      this.taskForm.patchValue(this.task);
    } else {
      this.taskForm.reset({
        title: '',
        description: '',
        status: TaskItemStatus.Pending,
      });
    }
  }
  onSave(): void {
    if (this.taskForm.valid) {
      const updated = this.taskForm.value;

      const newTask: TaskItem = {
        id: this.task?.id ?? '',
        title: updated.title,
        description: updated.description,
        status: updated.status,
        createdDate: '',
        updatedDate: '',
      };

      this.save.emit(newTask);
    }
  }

  onCancel(): void {
    this.cancel.emit();
  }
}
