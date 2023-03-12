import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { ToastrModule } from 'ngx-toastr';
import { ComponentModule } from 'src/components/components.module';
import { AuthTokenInterceptor } from 'src/factorys/interceptor/header.interceptor';
import { AppRoutingModule } from 'src/factorys/routes/app-routing.module';
import { AppComponent } from './entities/base/app.component';
import { LoginComponent } from './entities/login/login.component';
import { MainComponent } from './entities/main/main.component';
import { UsuarioCrudComponent } from './entities/usuario/usuario-crud/usuario-crud.component';
import { UsuarioComponent } from './entities/usuario/usuario-grid/usuario-grid.component';
import {MAT_TABS_CONFIG, MatTabsModule} from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import {MAT_PROGRESS_BAR_DEFAULT_OPTIONS, MatProgressBarModule} from '@angular/material/progress-bar';
import {MatInputModule} from '@angular/material/input';
import {MatBadgeModule} from '@angular/material/badge';
import {MatSelectModule} from '@angular/material/select';
import {MAT_SLIDE_TOGGLE_DEFAULT_OPTIONS, MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSliderModule} from '@angular/material/slider';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatButtonModule} from '@angular/material/button';
import {MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS} from '@angular/material/radio';
import {MAT_CHIPS_DEFAULT_OPTIONS, MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTooltipModule} from '@angular/material/tooltip';
import {RecoverPasswordComponent } from './entities/recoverPassword/recoverPassword.component';
import { ProjetoComponent } from './entities/projeto/projeto-grid/projeto-grid.component';
import { ProjetoCrudComponent } from './entities/projeto/projeto-crud/projeto-crud.component';
import { MatTableModule } from '@angular/material/table'  
import {MAT_PAGINATOR_DEFAULT_OPTIONS, MatPaginatorModule} from '@angular/material/paginator';
import { MAT_FORM_FIELD, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';

const maskConfigFunction: () => Partial<IConfig> = () => {
    return {
      validation: true,
    };
  };

@NgModule({
    declarations: [  
        AppComponent,
        LoginComponent,
        MainComponent,
        UsuarioComponent,
        ProjetoComponent,
        UsuarioCrudComponent,
        ProjetoCrudComponent,
        RecoverPasswordComponent
    ],
    exports: [  
        AppComponent,
        LoginComponent,
        MainComponent, 
        UsuarioComponent,
        ProjetoComponent,
        ProjetoCrudComponent,
        UsuarioCrudComponent,
    ],
    imports: [  
        CommonModule,
        ComponentModule,   
        ReactiveFormsModule,
        NgxMaskModule.forRoot(maskConfigFunction),
        FormsModule,
        MatTableModule,
        HttpClientModule,
        MatPaginatorModule,
        ToastrModule.forRoot({
            timeOut: 5000,
            enableHtml: true,
            progressBar: true,
            progressAnimation: 'decreasing',
            preventDuplicates: true
        }),
        AppRoutingModule,
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
        MatChipsModule,
        MatAutocompleteModule,
        MatTooltipModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthTokenInterceptor, multi: true },
        {
            provide: MAT_RADIO_DEFAULT_OPTIONS,
            useValue: { color: 'accent' },
        },
        {
            provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
            useValue: { color: 'accent' },
        },
        {
            provide: MAT_PROGRESS_BAR_DEFAULT_OPTIONS,
            useValue: { color: 'accent' },
        },
        {
            provide: MAT_CHIPS_DEFAULT_OPTIONS,
            useValue: { color: 'accent' },
        },
        {
            provide: MAT_PAGINATOR_DEFAULT_OPTIONS,
            useValue: { color: 'accent' },
        },
        {
            provide: MAT_TABS_CONFIG,
            useValue: { color: 'accent' },
        }
    ],
})

export class EntitiesModule { }