import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertisementAdminComponent } from './advertisement-admin.component';

describe('AdvertisementAdminComponent', () => {
  let component: AdvertisementAdminComponent;
  let fixture: ComponentFixture<AdvertisementAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvertisementAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertisementAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
