import { NgModule } from '@angular/core';
import { DataGridComponent } from './data-grid/data-grid.component';
import { TextErrorMessageComponent } from './text-error-message/text-error-message.component';
import { BrowserModule } from '@angular/platform-browser';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort';
import { NoSanitizePipe } from '../utils/sanitize'
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import {MAT_DATE_LOCALE} from '@angular/material/core';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { ConsultaModalComponent } from './consulta-modal/consulta-padrao.component';
import {MatChipsModule} from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CodeInputComponent } from './code-input/code-input.component';
import { MatTableModule } from '@angular/material/table'  
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const maskConfigFunction: () => Partial<IConfig> = () => {
    return {
      validation: true,
    };
};

@NgModule({
    declarations: [ TextErrorMessageComponent,DataGridComponent,NoSanitizePipe,ConsultaModalComponent,CodeInputComponent],
    providers: [{ provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
    exports: [ TextErrorMessageComponent,DataGridComponent,ConsultaModalComponent,CodeInputComponent],
    imports: [  
                BrowserModule,
                MatPaginatorModule,
                MatSortModule,
                MatTooltipModule,
                MatDatepickerModule,
                MatNativeDateModule,
                MatInputModule,
                MatTableModule,
                MatButtonModule,
                FormsModule,
                ReactiveFormsModule,
                MatFormFieldModule,
                MatIconModule,
                MatSelectModule,
                MatProgressBarModule,
                MatChipsModule,
                MatAutocompleteModule,
                MatChipsModule,
                NgxMaskModule.forRoot(maskConfigFunction)
            ]
})

export class ComponentModule { }