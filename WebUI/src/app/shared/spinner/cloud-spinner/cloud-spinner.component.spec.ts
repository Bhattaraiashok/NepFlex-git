import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CloudSpinnerComponent } from './cloud-spinner.component';

describe('CloudSpinnerComponent', () => {
  let component: CloudSpinnerComponent;
  let fixture: ComponentFixture<CloudSpinnerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CloudSpinnerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CloudSpinnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
