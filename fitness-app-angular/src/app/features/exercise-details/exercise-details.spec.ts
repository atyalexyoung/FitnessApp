import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExerciseDetails } from './exercise-details';

describe('ExerciseDetails', () => {
  let component: ExerciseDetails;
  let fixture: ComponentFixture<ExerciseDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExerciseDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExerciseDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
