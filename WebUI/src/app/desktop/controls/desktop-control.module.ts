import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from 'app/shared/shared.module';
import { DesktopHeaderComponent } from 'app/desktop/controls/desktop-header/desktop-header.component';
import { DesktopFooterComponent } from 'app/desktop/controls/desktop-footer/desktop-footer.component';
import { SearchBoxComponent } from './search-box/search-box.component';
import { SearchResultsOverlayComponent } from './search-box/search-results-overlay/search-results-overlay.component';
import { FilterBoxComponent } from './search-box/filter-box/filter-box.component';
import { HomeSearchOverlayComponent } from './home-search-overlay/home-search-overlay.component';
import { InfoBoxComponent } from './info-box/info-box.component';
import { ItemContainerComponent } from './item-container/item-container.component';
import { MyAngularMaterialModule } from 'app/shared/angular-material/angularMaterial.module';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    MyAngularMaterialModule
  ],
  declarations: [
    DesktopHeaderComponent,
    DesktopFooterComponent,
    SearchBoxComponent,
    SearchResultsOverlayComponent,
    FilterBoxComponent,
    HomeSearchOverlayComponent,
    InfoBoxComponent,
    ItemContainerComponent
  ],
  exports: [
    SharedModule,
    InfoBoxComponent,
    SearchBoxComponent,
    DesktopHeaderComponent,
    DesktopFooterComponent,
    ItemContainerComponent
  ],
  providers: [ItemContainerComponent]
})
export class DesktopControlModule {}
