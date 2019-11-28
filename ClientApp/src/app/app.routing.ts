import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/'; 
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { TourComponent } from './tour/tour.component';
import { AuthGuard } from './_guards';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'tours', component: TourComponent, canActivate: [AuthGuard]},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
