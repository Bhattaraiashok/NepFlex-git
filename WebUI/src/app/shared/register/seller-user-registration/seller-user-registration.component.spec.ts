import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerUserRegistrationComponent } from './seller-user-registration.component';

describe('SellerUserRegistrationComponent', () => {
  let component: SellerUserRegistrationComponent;
  let fixture: ComponentFixture<SellerUserRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerUserRegistrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerUserRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
