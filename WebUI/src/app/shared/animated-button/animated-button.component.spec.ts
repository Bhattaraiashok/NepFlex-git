import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AnimatedButtonComponent } from './animated-button.component';

describe('AnimatedButtonComponent', () => {
  let component: AnimatedButtonComponent;
  let fixture: ComponentFixture<AnimatedButtonComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AnimatedButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnimatedButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
