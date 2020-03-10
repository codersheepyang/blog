import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeftAdminComponent } from './left-admin.component';

describe('LeftAdminComponent', () => {
  let component: LeftAdminComponent;
  let fixture: ComponentFixture<LeftAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeftAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeftAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
