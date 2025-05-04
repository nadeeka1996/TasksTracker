export enum TaskItemStatus {
    Pending = 0,
    InProgress = 1,
    Completed = 2,
  }
  
  export interface TaskItem {
    id: string;
    title: string;
    description: string;
    status: TaskItemStatus;
    createdDate: string;
    updatedDate: string;
  }
  
  export interface TaskCreateRequest {
    title: string;
    description: string;
    status: TaskItemStatus;
  }
  
  export interface TaskUpdateRequest {
    title: string;
    description: string;
    status: TaskItemStatus;
  }
  