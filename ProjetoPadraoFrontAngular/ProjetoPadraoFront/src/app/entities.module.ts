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
import {MatTabsModule} from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatInputModule} from '@angular/material/input';
import {MatBadgeModule} from '@angular/material/badge';
import {MatSelectModule} from '@angular/material/select';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSliderModule} from '@angular/material/slider';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatButtonModule} from '@angular/material/button';
import {MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS} from '@angular/material/radio';
import {MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTooltipModule} from '@angular/material/tooltip';
import {RecoverPasswordComponent } from './entities/recoverPassword/recoverPassword.component';

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
        UsuarioCrudComponent,
        RecoverPasswordComponent
    ],
    exports: [  
        AppComponent,
        LoginComponent,
        MainComponent, 
        UsuarioComponent,
        UsuarioCrudComponent,
    ],
    imports: [  
        CommonModule,
        ComponentModule,   
        ReactiveFormsModule,
        NgxMaskModule.forRoot(maskConfigFunction),
        FormsModule,
        HttpClientModule,
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
        }
    ],
})

export class EntitiesModule { }