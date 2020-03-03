/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { QrPrintComponent } from './qr-print.component';

describe('QrPrintComponent', () => {
  let component: QrPrintComponent;
  let fixture: ComponentFixture<QrPrintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QrPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QrPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
