import { Component } from '@angular/core';
import { Board } from 'src/objects/Atividade/Board';
import { DataService } from '../service/data.service';

@Component({
  selector: 'atividade-kanban-root',
  templateUrl: './atividade-kanban.component.html',
  styleUrls: ['../atividade.component.css']
})

export class AtividadeKanbanComponent{
  constructor(private _dataService: DataService) {}

  public get Board(): Board {
    return this._dataService.getData();
  }

};


