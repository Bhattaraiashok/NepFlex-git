import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { PagenotfoundComponent } from './shared/pages/pagenotfound/pagenotfound.component';
import { HomeComponent } from './shared/pages/home/home.component';
import { DesktopModule } from 'app/desktop/desktop.module';
import { MobileModule } from 'app/mobile/mobile.module';
import { SharedModule } from 'app/shared/shared.module';
import { AppRoutingModule } from 'app/app-routing.module';
import { CoreModule } from 'app/core/core.module';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CanDeactivateGuardService } from "app/shared/guards/can-deactivate-guard.service";
import { CanActivateGuardService } from "app/shared/guards/can-activate-guard.service";
import { DesktopControlModule } from "app/desktop/controls/desktop-control.module";

@NgModule({
  declarations: [AppComponent, PagenotfoundComponent, HomeComponent],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CoreModule,
    RouterModule,
    MobileModule,
    DesktopModule,
    SharedModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    DesktopControlModule
  ],
  providers: [
    CanDeactivateGuardService,
    CanActivateGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
