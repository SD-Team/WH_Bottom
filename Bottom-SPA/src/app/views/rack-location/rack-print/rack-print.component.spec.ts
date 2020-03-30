/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RackPrintComponent } from './rack-print.component';

describe('RackPrintComponent', () => {
  let component: RackPrintComponent;
  let fixture: ComponentFixture<RackPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
