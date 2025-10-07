import { Component, inject, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ExerciseService } from '../../core/services/exercise';
import { Exercise } from '../../shared/models/exercise';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { Location } from '@angular/common';
import { MatButtonModule } from '@angular/material/button'; // for your Back button

@Component({
  selector: 'app-exercise-details',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatButtonModule
  ],
  templateUrl: './exercise-details.html',
  styleUrls: ['./exercise-details.scss']
})
export class ExerciseDetails {
  private route = inject(ActivatedRoute);
  private exerciseService = inject(ExerciseService);
  exercise?: Exercise;

  imageLoaded = false;
  imageSrc!: string;

  constructor(private location: Location) {
    const exerciseId = this.route.snapshot.paramMap.get('id');
    if (!exerciseId) {
      console.error('No exercise ID provided in route');
      return;
    }
    this.exercise = this.exerciseService.getExerciseById(exerciseId);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['exercise'] && this.exercise) {
      this.imageLoaded = false;
      this.imageSrc = this.exercise.imageUrls?.[0] || '';
      if (!this.imageSrc) this.imageLoaded = true;
    }
  }

  onImageLoad() {
    this.imageLoaded = true;
  }

  goBack() {
    this.location.back();
  }
}
