import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RigthColumnComponent } from './rigth-column.component';

describe('RigthColumnComponent', () => {
  let component: RigthColumnComponent;
  let fixture: ComponentFixture<RigthColumnComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RigthColumnComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RigthColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
