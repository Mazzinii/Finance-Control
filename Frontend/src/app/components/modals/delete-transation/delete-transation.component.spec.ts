import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteTransationComponent } from './delete-transation.component';

describe('DeleteTransationComponent', () => {
  let component: DeleteTransationComponent;
  let fixture: ComponentFixture<DeleteTransationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteTransationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteTransationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
