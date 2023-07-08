import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthTokenInterceptor } from 'src/factorys/interceptor/header.interceptor';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { UsuarioCrudComponent } from './usuario/usuario-crud/usuario-crud.component';
import {MAT_TABS_CONFIG, MatTabsModule} from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import {MAT_PROGRESS_BAR_DEFAULT_OPTIONS, MatProgressBarModule} from '@angular/material/progress-bar';
import {MatInputModule} from '@angular/material/input';
import {MatBadgeModule} from '@angular/material/badge';
import {MatSelectModule} from '@angular/material/select';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSliderModule} from '@angular/material/slider';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatButtonModule} from '@angular/material/button';
import {MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS} from '@angular/material/radio';
import {MAT_CHIPS_DEFAULT_OPTIONS, MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTooltipModule} from '@angular/material/tooltip';
import {RecoverPasswordComponent } from './recoverPassword/recoverPassword.component';
import { ProjetoComponent } from './projeto/projeto-grid/projeto-grid.component';
import { MatTableModule } from '@angular/material/table'  
import {MAT_PAGINATOR_DEFAULT_OPTIONS, MatPaginatorModule} from '@angular/material/paginator';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { RegisterComponent } from './register/register.component';
import { ComponentModule } from '../../components/components.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { UsuarioComponent } from './usuario/usuario-grid/usuario-grid.component';
import { ProjetoCrudComponent } from './projeto/projeto-crud/projeto-crud.component';
import { EntitiesPagesRoutingModule } from './entities-routing.module';
import { MaintenanceComponent } from './extrapages/maintenance/maintenance.component';
import { ComingSoonComponent } from './extrapages/coming-soon/coming-soon.component';
import { NgbNavModule, NgbDropdownModule, NgbAccordionModule, NgbTooltipModule, NgbPaginationModule, NgbProgressbarModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { FlatpickrModule } from 'angularx-flatpickr';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgxUsefulSwiperModule } from 'ngx-useful-swiper';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExtraPagesRoutingModule } from '../extrapages/extrapages-routing.module';
import { KambamGridTarefasComponent } from './kamban-tarefas/kamban-tarefas-grid/kamban-tarefas-grid.component';
import { KambamTarefasComponent } from './kamban-tarefas/kamban-tarefas/kamban-tarefas.component';
import { CountToModule } from 'angular-count-to';
import { DndModule } from 'ngx-drag-drop';
import { SimplebarAngularModule } from 'simplebar-angular';
import lottie from 'lottie-web';
import { defineElement } from 'lord-icon-element';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AdministracaoTarefasCrudComponent } from './administracao-tarefas/administracao-tarefas-crud/Administracao-tarefas-crud.component';
import { AdministracaoTarefasComponent } from './administracao-tarefas/administracao-tarefas-grid/administracao-tarefas-grid.component';
import { DetalhesTarefasComponent } from './detalhes-tarefas/detalhes-tarefas.component';
import { NgbdListViewSortableHeaderAdministracaoTarefa } from './administracao-tarefas/administracao-tarefas-crud/Administracao-tarefas-crud.directive';
import { TimeLineComponent } from './time-line/time-line.component';
import { CronogramaGridComponent } from './cronograma/cronograma-grid/cronograma-grid.component';
import { CronogramaComponent } from './cronograma/cronograma-crud/cronograma.component';
import { FullCalendarModule } from '@fullcalendar/angular';
import { TemplateCrudComponent } from './template/template-crud/template-crud.component';
import { TemplateComponent } from './template/template-grid/template-grid.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
    return {
      validation: true,
    };
  };

@NgModule({
    declarations: [  
        LoginComponent,
        MainComponent,
        UsuarioComponent,
        ProjetoComponent,
        UsuarioCrudComponent,
        ProjetoCrudComponent,
        RecoverPasswordComponent,
        RegisterComponent,
        MaintenanceComponent,
        ComingSoonComponent,
        TimeLineComponent,
        KambamTarefasComponent,
        KambamGridTarefasComponent,
        DetalhesTarefasComponent,
        CronogramaGridComponent,
        TemplateComponent,
        TemplateCrudComponent,
        CronogramaComponent,
        AdministracaoTarefasComponent,
        AdministracaoTarefasCrudComponent,
        NgbdListViewSortableHeaderAdministracaoTarefa
    ],
    exports: [  
        LoginComponent,
        MainComponent,
        UsuarioComponent,
        ProjetoComponent,
        UsuarioCrudComponent,
        ProjetoCrudComponent,
        RecoverPasswordComponent,
        RegisterComponent,
        MaintenanceComponent,
        TemplateComponent,
        TemplateCrudComponent,
        TimeLineComponent,
        ComingSoonComponent,
        CronogramaGridComponent,
        CronogramaComponent,
        KambamTarefasComponent,
        KambamGridTarefasComponent,
        DetalhesTarefasComponent,
        AdministracaoTarefasComponent,
        AdministracaoTarefasCrudComponent,
        NgbdListViewSortableHeaderAdministracaoTarefa
    ],
    imports: [  
        CommonModule,
        ComponentModule,   
        ReactiveFormsModule,
        FormsModule,
        FullCalendarModule,
        MatChipsModule,
        MatTableModule,
        DragDropModule,
        HttpClientModule,
        MatPaginatorModule,
        MatTabsModule,
        MatIconModule,
        MatProgressBarModule,
        MatInputModule,
        MatBadgeModule,
        MatSelectModule,
        MatSlideToggleModule,
        MatSliderModule,
        MatDatepickerModule,
        MatButtonModule,
        MatRadioModule,
        MatAutocompleteModule,
        MatTooltipModule,
        EntitiesPagesRoutingModule,
        NgbNavModule,
        NgbDropdownModule,
        NgbAccordionModule,
        NgbTooltipModule,
        NgxUsefulSwiperModule,
        NgSelectModule,
        FlatpickrModule,
        NgbPaginationModule,
        ExtraPagesRoutingModule,
        CountToModule,
        Ng2SearchPipeModule,
        NgxMaskModule.forRoot(maskConfigFunction),
        NgbProgressbarModule,
        SharedModule,
        NgbTypeaheadModule,
        SimplebarAngularModule,
        DndModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthTokenInterceptor, multi: true },
        {
            provide: MAT_RADIO_DEFAULT_OPTIONS,
            useValue: { color: 'indigo' },
        },
        {
            provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
            useValue: { color: 'indigo' },
        },
        {
            provide: MAT_PROGRESS_BAR_DEFAULT_OPTIONS,
            useValue: { color: 'indigo' },
        },
        {
            provide: MAT_CHIPS_DEFAULT_OPTIONS,
            useValue: { color: 'indigo' },
        },
        {
            provide: MAT_PAGINATOR_DEFAULT_OPTIONS,
            useValue: { color: 'indigo' },
        },
        {
            provide: MAT_TABS_CONFIG,
            useValue: { color: 'indigo' },
        }
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class EntitiesModule {
    constructor() {
        defineElement(lottie.loadAnimation);
    }
}


