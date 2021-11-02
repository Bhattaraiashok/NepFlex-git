import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HomeSearchOverlayComponent } from './home-search-overlay.component';

describe('HomeSearchOverlayComponent', () => {
  let component: HomeSearchOverlayComponent;
  let fixture: ComponentFixture<HomeSearchOverlayComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeSearchOverlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeSearchOverlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
