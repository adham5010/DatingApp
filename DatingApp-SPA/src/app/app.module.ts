import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValuesComponentComponent } from './ValuesComponent/ValuesComponent.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AuthService } from './_Services/Auth.service';

@NgModule({
   declarations: [
      AppComponent,
      ValuesComponentComponent,
      NavBarComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
     AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
