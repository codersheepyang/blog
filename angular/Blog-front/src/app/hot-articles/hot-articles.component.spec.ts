import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HotArticlesComponent } from './hot-articles.component';

describe('HotArticlesComponent', () => {
  let component: HotArticlesComponent;
  let fixture: ComponentFixture<HotArticlesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HotArticlesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HotArticlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
