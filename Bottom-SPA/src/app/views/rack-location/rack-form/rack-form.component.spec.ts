/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RackFormComponent } from './rack-form.component';

describe('RackFormComponent', () => {
  let component: RackFormComponent;
  let fixture: ComponentFixture<RackFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
