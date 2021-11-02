import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HomeBodyComponent } from './home-body.component';

describe('HomeBodyComponent', () => {
  let component: HomeBodyComponent;
  let fixture: ComponentFixture<HomeBodyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeBodyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
