import { Component, Input, OnInit } from '@angular/core';
import { Task } from 'src/objects/Atividade/Task';

@Component({
  selector: "app-task",
  templateUrl: "./task.component.html",
  styleUrls: ["./task.component.scss"],
  host: {
    class: "task"
  }
})

export class TaskComponent implements OnInit {
  @Input()
  taskModel!: Task;

  constructor() {}

  ngOnInit() {}

  public get Id(): string {
    return (<Task>this.taskModel).Id.substring(0, 6);
  }
}