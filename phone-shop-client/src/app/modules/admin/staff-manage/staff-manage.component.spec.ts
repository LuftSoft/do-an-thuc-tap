/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StaffManageComponent } from './staff-manage.component';

describe('StaffManageComponent', () => {
  let component: StaffManageComponent;
  let fixture: ComponentFixture<StaffManageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StaffManageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StaffManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
