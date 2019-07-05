/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Member_listComponent } from './member_list.component';

describe('Member_listComponent', () => {
  let component: Member_listComponent;
  let fixture: ComponentFixture<Member_listComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Member_listComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Member_listComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
