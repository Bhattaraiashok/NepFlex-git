import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ChildDropdownComponent } from './child-dropdown.component';

describe('ChildDropdownComponent', () => {
  let component: ChildDropdownComponent;
  let fixture: ComponentFixture<ChildDropdownComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ChildDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
