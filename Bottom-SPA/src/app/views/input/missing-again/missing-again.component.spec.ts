/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MissingAgainComponent } from './missing-again.component';

describe('MissingAgainComponent', () => {
  let component: MissingAgainComponent;
  let fixture: ComponentFixture<MissingAgainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MissingAgainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MissingAgainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
