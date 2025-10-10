import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ExerciseService } from '../../core/services/exercise';
import { Exercise } from '../../shared/models/exercise';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { Location } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Subscription } from 'rxjs';

interface MediaItem {
  type: 'image' | 'video';
  url: string;
  loaded?: boolean;
}

@Component({
  selector: 'app-exercise-details',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './exercise-details.html',
  styleUrls: ['./exercise-details.scss']
})
export class ExerciseDetails {
  private route = inject(ActivatedRoute);
  private exerciseService = inject(ExerciseService);

  exercise?: Exercise;
  private subscription?: Subscription;

  // Media carousel
  mediaItems: MediaItem[] = [];
  currentMediaIndex = 0;
  mediaLoaded = false;

  constructor(private location: Location) { }

  ngOnInit() {
    const exerciseId = this.route.snapshot.paramMap.get('id');
    if (exerciseId) {
      this.subscription = this.exerciseService.getExerciseById(exerciseId).subscribe(exercise => {
        this.exercise = exercise;

        // Combine images and videos into media items
        this.mediaItems = [
          ...(exercise.imageUrls?.map(url => ({ type: 'image' as const, url, loaded: false })) || []),
          ...(exercise.videoUrls?.map(url => ({ type: 'video' as const, url, loaded: false })) || [])
        ];

        // Set initial loaded state
        if (this.mediaItems.length === 0) {
          this.mediaLoaded = true;
        }
      });
    }
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  get currentMedia(): MediaItem | undefined {
    return this.mediaItems[this.currentMediaIndex];
  }

  get canGoPrevious(): boolean {
    return this.currentMediaIndex > 0;
  }

  get canGoNext(): boolean {
    return this.currentMediaIndex < this.mediaItems.length - 1;
  }

  previousMedia() {
    if (this.canGoPrevious) {
      this.currentMediaIndex--;
      this.mediaLoaded = false;
    }
  }

  nextMedia() {
    if (this.canGoNext) {
      this.currentMediaIndex++;
      this.mediaLoaded = false;
    }
  }

  onMediaLoad() {
    this.mediaLoaded = true;
    if (this.currentMedia) {
      this.currentMedia.loaded = true;
    }
  }

  // Touch/swipe support
  private touchStartX = 0;
  private touchEndX = 0;

  onTouchStart(event: TouchEvent) {
    this.touchStartX = event.changedTouches[0].screenX;
  }

  onTouchEnd(event: TouchEvent) {
    this.touchEndX = event.changedTouches[0].screenX;
    this.handleSwipe();
  }

  private handleSwipe() {
    const swipeThreshold = 50; // Minimum swipe distance
    const diff = this.touchStartX - this.touchEndX;

    if (Math.abs(diff) > swipeThreshold) {
      if (diff > 0) {
        // Swiped left - go to next
        this.nextMedia();
      } else {
        // Swiped right - go to previous
        this.previousMedia();
      }
    }
  }

  goBack() {
    this.location.back();
  }
}