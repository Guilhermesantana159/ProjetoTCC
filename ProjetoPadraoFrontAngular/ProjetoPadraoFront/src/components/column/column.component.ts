import { Component, Input } from "@angular/core";
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem
} from "@angular/cdk/drag-drop";
import { Column } from "src/objects/Atividade/Column";

@Component({
  selector: "kanban-column",
  templateUrl: "./column.component.html",
  styleUrls: ["./column.component.scss"]
})
export class KanbanColumnComponent {
  @Input() column!: Column;

  drop(event: CdkDragDrop<Task[]> | any) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }
}
