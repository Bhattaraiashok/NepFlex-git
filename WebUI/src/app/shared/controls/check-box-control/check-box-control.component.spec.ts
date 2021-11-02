import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckBoxControlComponent } from './check-box-control.component';

describe('CheckBoxControlComponent', () => {
  let component: CheckBoxControlComponent;
  let fixture: ComponentFixture<CheckBoxControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckBoxControlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckBoxControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
