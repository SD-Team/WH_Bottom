/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InputMainComponent } from './input-main.component';

describe('InputMainComponent', () => {
  let component: InputMainComponent;
  let fixture: ComponentFixture<InputMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InputMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InputMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
