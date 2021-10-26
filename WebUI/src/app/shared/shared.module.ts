import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { OverlayComponent } from './overlay/overlay.component';
import { CarouselComponent } from './carousel/carousel.component';
import { HomeService } from 'app/shared/services/home.service';
import { OrderByPipe } from 'app/shared/pipes/order-by.pipe';
import { FilteringSearch } from 'app/shared/ResourceModels/FilteringSearch';
import { SearchService } from 'app/shared/services/search.service';
import { ReportService } from 'app/shared/services/report.service';
import { DefaultValuePipe } from 'app/shared/pipes/default-value.pipe';
import { BrowserModule } from '@angular/platform-browser';
import { SpinnerComponent } from 'app/shared/spinner/spinner.component';
import { CloudSpinnerComponent } from './spinner/cloud-spinner/cloud-spinner.component';
import { ModalComponent } from './modal/modal.component';
import { DetailService } from 'app/shared/services/detail.service';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CamelCase } from 'app/shared/pipes/camelcase.pipe';
import { AnimatedButtonComponent } from './animated-button/animated-button.component';
import { ButtonProperties } from 'app/shared/ResourceModels/ButtonProperties';
import { SendEmailComponent } from './send-email/send-email.component';
import { SendEmailService } from 'app/shared/services/send-email.service';
import { HTTPHeader } from './services/http-header';
import { HttpClientModule } from '@angular/common/http';
import { FilterByPipe } from './pipes/filter-by.pipe';
import { OrderByNestedPipe } from './pipes/order-by-nested.pipe';
import { UniqueValuePipe } from './pipes/unique-value.pipe';
import { SearchStringQuery } from './ResourceModels/SearchStringQuerys';
import { TextAreaComponent } from './text-area/text-area.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatBadgeModule } from '@angular/material/badge';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule, MAT_CHIPS_DEFAULT_OPTIONS } from '@angular/material/chips';
import { MatNativeDateModule, MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTreeModule } from '@angular/material/tree';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { DropdownComponent } from './dropdown/dropdown.component';
import { ChildDropdownComponent } from './dropdown/child-dropdown/child-dropdown.component';
import { AutoCompleteSearchboxComponent, SearchString } from './auto-complete-searchbox/auto-complete-searchbox.component';
import { TabLayoutComponent } from './tab-layout/tab-layout.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouteTo } from './interfaces/local-router';
import { TelephoneMaskDirective } from './directives/telephone-mask.directive';
import { AlertComponent } from './alert/alert.component';
import { NotifierComponent } from './notifier/notifier.component';
import { AlertMessageProperties } from "app/shared/ResourceModels/AlertMessages";
// import { CanDeactivateGuardService } from "app/shared/guards/can-deactivate-guard.service";
// import { CanActivateGuardService } from "app/shared/guards/can-activate-guard.service";

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    // angular -material
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    MatFormFieldModule,
    MatOptionModule
  ],
  declarations: [
    SpinnerComponent,
    DefaultValuePipe,
    OrderByPipe,
    FilterByPipe,
    OrderByNestedPipe,
    UniqueValuePipe,
    CamelCase,
    OverlayComponent,
    CarouselComponent,
    CloudSpinnerComponent,
    ModalComponent,
    AnimatedButtonComponent,
    SendEmailComponent,
    TextAreaComponent,
    DropdownComponent,
    ChildDropdownComponent,
    AnimatedButtonComponent,
    AutoCompleteSearchboxComponent,
    TabLayoutComponent,
    LoginComponent,
    RegisterComponent,
    TelephoneMaskDirective,
    AlertComponent,
    NotifierComponent
  ],
  providers: [
    NgbActiveModal,
    ReportService,
    SearchService,
    DetailService,
    SendEmailService,
    FilteringSearch,
    ButtonProperties,
    SearchStringQuery,
    OverlayComponent,
    SpinnerComponent,
    CarouselComponent,
    HomeService,
    SearchString,
    RouteTo,
    AlertMessageProperties,
    HTTPHeader,
    {
      provide: MAT_CHIPS_DEFAULT_OPTIONS,
      useValue: {
        separatorKeyCodes: [ENTER, COMMA]
      }
    }
  ],
  entryComponents: [SendEmailComponent],
  exports: [
    SpinnerComponent,
    CloudSpinnerComponent,
    DefaultValuePipe,
    OrderByPipe,
    FilterByPipe,
    OrderByNestedPipe,
    UniqueValuePipe,
    CamelCase,
    OverlayComponent,
    CarouselComponent,
    ModalComponent,
    AnimatedButtonComponent,
    SendEmailComponent,
    TextAreaComponent,
    DropdownComponent,
    ChildDropdownComponent,
    AnimatedButtonComponent,
    AutoCompleteSearchboxComponent,
    TabLayoutComponent,
    LoginComponent,
    RegisterComponent
  ]
})
export class SharedModule { }
