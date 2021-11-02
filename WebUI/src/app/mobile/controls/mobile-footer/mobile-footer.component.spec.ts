import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MobileFooterComponent } from './mobile-footer.component';

describe('MobileFooterComponent', () => {
  let component: MobileFooterComponent;
  let fixture: ComponentFixture<MobileFooterComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MobileFooterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
