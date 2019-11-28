import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { ReactiveFormsModule }    from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatSidenavModule, MatListModule } from '@angular/material';
import { AppComponent }  from './app.component';
import { routing } from './app.routing';

import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SideMenu } from './side-menu/side-menu.component';
import { TourComponent } from './tour/tour.component';

import { AlertComponent } from './_directives';
import { AuthGuard } from './_guards';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AlertService, AuthenticationService, ManagerService, OrderService } from './_services';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';

@NgModule({
    imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    routing,
    MatSidenavModule,
    MatListModule
        
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,

        NavMenuComponent,      
        SideMenu,
        TourComponent
    ],
    providers: [
        AuthGuard,
        AlertService,
        AuthenticationService,
        ManagerService,
        OrderService,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
                
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }
