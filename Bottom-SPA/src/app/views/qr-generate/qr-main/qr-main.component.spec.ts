/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { QrMainComponent } from './qr-main.component';

describe('QrMainComponent', () => {
  let component: QrMainComponent;
  let fixture: ComponentFixture<QrMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QrMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QrMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
