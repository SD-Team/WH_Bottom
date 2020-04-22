/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OutputPrintQrcodeAgainComponent } from './output-print-qrcode-again.component';

describe('OutputPrintQrcodeAgainComponent', () => {
  let component: OutputPrintQrcodeAgainComponent;
  let fixture: ComponentFixture<OutputPrintQrcodeAgainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutputPrintQrcodeAgainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutputPrintQrcodeAgainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
