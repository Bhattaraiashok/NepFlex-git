import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StandardUserRegistrationComponent } from './standard-user-registration.component';

describe('StandardUserRegistrationComponent', () => {
  let component: StandardUserRegistrationComponent;
  let fixture: ComponentFixture<StandardUserRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StandardUserRegistrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StandardUserRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
