import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagenotfoundComponent } from 'app/shared/pages/pagenotfound/pagenotfound.component';
import { HomeComponent } from 'app/shared/pages/home/home.component';
// import { SearchComponent } from 'app/desktop/pages/search/search.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AboutUsComponent } from 'app/desktop/pages/about-us/about-us.component';
import { TermsOfUseComponent } from 'app/desktop/pages/terms-of-use/terms-of-use.component';
import { FaqComponent } from 'app/desktop/pages/faq/faq.component';
import { ReportUsComponent } from 'app/desktop/pages/report-us/report-us.component';
import { componentFactoryName } from '@angular/compiler';
import { DetailComponent } from './desktop/pages/detail/detail.component';
import { ListComponent } from './desktop/pages/list/list.component';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { RegisterUserComponent } from './desktop/register-user/register-user.component';
import { ProfileComponent } from "app/desktop/pages/profile/profile.component";
import { CanDeactivateGuardService } from "app/shared/guards/can-deactivate-guard.service";
import { LoginPageComponent } from "app/desktop/pages/login/login.component";
import { CanActivateGuardService } from "app/shared/guards/can-activate-guard.service";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'faq', component: FaqComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  { path: 'detail/:itemId', component: DetailComponent, pathMatch: 'full' },
  { path: 'list', component: ListComponent, pathMatch: 'full' },
  { path: 'report', component: ReportUsComponent, pathMatch: 'full' },
  { path: 'about-us', component: AboutUsComponent, pathMatch: 'full' },
  { path: 'terms-of-use', component: TermsOfUseComponent, pathMatch: 'full' },
  { path: 'register', component: RegisterUserComponent, pathMatch: 'full', canDeactivate: [CanDeactivateGuardService] },
  { path: 'login', component: LoginPageComponent, pathMatch: 'full', canDeactivate: [CanDeactivateGuardService] },
  { path: 'profile', component: ProfileComponent, pathMatch: 'full', canActivate: [CanActivateGuardService] },
  { path: 'home/:windowView', component: HomeComponent, pathMatch: 'full' },

  { path: 'error', pathMatch: 'full', component: PagenotfoundComponent },
  { path: '**', pathMatch: 'full', component: PagenotfoundComponent }
];

@NgModule({
  imports: [BrowserModule, FormsModule, RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
  providers: [{ provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }]
})
export class AppRoutingModule { }
