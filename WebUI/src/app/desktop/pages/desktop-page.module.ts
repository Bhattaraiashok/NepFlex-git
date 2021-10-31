import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HomeDesktopComponent } from 'app/desktop/pages/home-desktop/home-desktop.component';
import { DesktopControlModule } from 'app/desktop/controls/desktop-control.module';
import { SharedModule } from 'app/shared/shared.module';
import { HomeBodyComponent } from './home-desktop/home-body/home-body.component';
// import { SearchComponent } from './search/search.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { TermsOfUseComponent } from './terms-of-use/terms-of-use.component';
import { FaqComponent } from './faq/faq.component';
import { ReportUsComponent } from './report-us/report-us.component';
import { DetailComponent } from './detail/detail.component';
import { ListComponent } from './list/list.component';
import { LoginPageComponent } from './login/login.component';
import { RegularProfileComponent } from './profile/regular-profile/regular-profile.component';
import { SellerProfileComponent } from './profile/seller-profile/seller-profile.component';
import { RegisterUserComponent } from "app/desktop/pages/register-user/register-user.component";

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        RouterModule,
        DesktopControlModule,
        SharedModule
    ],
    declarations: [
        HomeDesktopComponent,
        HomeBodyComponent,
        // SearchComponent,
        AboutUsComponent,
        TermsOfUseComponent,
        FaqComponent,
        ReportUsComponent,
        DetailComponent,
        ListComponent,
        LoginPageComponent,
        RegisterUserComponent,
        RegularProfileComponent,
        SellerProfileComponent
    ],
    exports: [HomeDesktopComponent]
})
export class DesktopPageModule { }
