/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RackMainComponent } from './rack-main.component';

describe('RackMainComponent', () => {
  let component: RackMainComponent;
  let fixture: ComponentFixture<RackMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
