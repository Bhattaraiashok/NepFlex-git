import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { SharedModule } from 'app/shared/shared.module';
import { DesktopPageModule } from 'app/desktop/pages/desktop-page.module';
import { HomeDesktopComponent } from 'app/desktop/pages/home-desktop/home-desktop.component';
import { DesktopControlModule } from 'app/desktop/controls/desktop-control.module';
import { RegisterUserComponent } from './register-user/register-user.component';

@NgModule({
  imports: [
    BrowserModule,
    RouterModule,
    SharedModule,
    DesktopPageModule,
    DesktopControlModule
  ],
  declarations: [RegisterUserComponent],
  exports: [HomeDesktopComponent],
  providers: []
})
export class DesktopModule {}
