import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AutoCompleteSearchboxComponent } from './auto-complete-searchbox.component';

describe('AutoCompleteSearchboxComponent', () => {
  let component: AutoCompleteSearchboxComponent;
  let fixture: ComponentFixture<AutoCompleteSearchboxComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoCompleteSearchboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoCompleteSearchboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
