import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HomeMobileComponent } from './home-mobile.component';

describe('HomeMobileComponent', () => {
  let component: HomeMobileComponent;
  let fixture: ComponentFixture<HomeMobileComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeMobileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeMobileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
