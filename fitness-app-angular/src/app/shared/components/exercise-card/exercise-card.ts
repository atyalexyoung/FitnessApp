import { Component, Input, SimpleChanges } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { Exercise } from '../../models/exercise';
import { CommonModule, } from '@angular/common';

@Component({
  selector: 'app-exercise-card',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, CommonModule],
  templateUrl: './exercise-card.html',
  styleUrls: ['./exercise-card.scss']
})
export class ExerciseCard {
  @Input() exercise!: Exercise;

  imageLoaded = false;
  imageSrc!: string;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['exercise'] && this.exercise) {
      // Only use the first image URL; no placeholder fallback needed
      this.imageSrc = this.exercise.imageUrls?.[0] || '';
      this.imageLoaded = false; // reset loading state
    }
  }

  onImageLoad() {
    this.imageLoaded = true;
  }
}
