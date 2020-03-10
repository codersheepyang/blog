import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RightAdminComponent } from './right-admin.component';

describe('RightAdminComponent', () => {
  let component: RightAdminComponent;
  let fixture: ComponentFixture<RightAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RightAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RightAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
