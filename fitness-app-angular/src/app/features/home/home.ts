import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { BodyPart, BodyPartType, Exercise, ExerciseTypeTag, bodyPartTypeMap } from '../../shared/models/exercise';
import { ExerciseCard } from "../../shared/components/exercise-card/exercise-card";
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import { ExerciseService } from '../../core/services/exercise';
import { FormsModule } from '@angular/forms';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ExerciseCard,
    CommonModule,
    MatGridListModule,
    FormsModule,
    MatChipsModule
  ],
  templateUrl: './home.html',
  styleUrls: ['./home.scss']
})
export class HomeComponent {
  exerciseList: Exercise[] = []
  exerciseService: ExerciseService = inject(ExerciseService);

  searchTerm: string = '';
  filteredExerciseList: Exercise[] = [];

  // Body Part Types (main categories)
  bodyPartTypes: string[] = ['Arms', 'Shoulders', 'Chest', 'Back', 'Core', 'Legs'];
  selectedBodyPartTypes: string[] = [];

  // Specific muscles (shown conditionally)
  availableSpecificMuscles: string[] = [];
  selectedSpecificMuscles: string[] = [];

  // Exercise tags
  exerciseTags: string[] = Object.values(ExerciseTypeTag);
  selectedTags: string[] = [];

  onBodyPartTypeChange() {
    // Update available specific muscles based on selected body part types
    this.availableSpecificMuscles = [];

    this.selectedBodyPartTypes.forEach(type => {
      const muscles = bodyPartTypeMap[type as BodyPartType];
      if (muscles) {
        this.availableSpecificMuscles.push(...muscles);
      }
    });

    // Clear specific muscle selections that are no longer available
    this.selectedSpecificMuscles = this.selectedSpecificMuscles.filter(
      muscle => this.availableSpecificMuscles.includes(muscle)
    );

    this.filterExercises();
  }

  ngOnInit() {
    this.exerciseService.getAllExercises().subscribe(exercises => {
      this.exerciseList = exercises;
      this.filteredExerciseList = exercises;
    });
  }

  ngOnDestroy(){
    
  }

  filterExercises() {
    const query = this.searchTerm.toLowerCase().trim();

    this.filteredExerciseList = this.exerciseList.filter(ex => {
      // Search filter
      const matchesSearch =
        !query ||
        ex.name.toLowerCase().includes(query) ||
        ex.exerciseTags?.some(tag => tag.toLowerCase().includes(query)) ||
        ex.bodyParts?.some(bp => bp.toLowerCase().includes(query));

      // Body part type filter (hierarchical)
      let matchesBodyParts = true;
      if (this.selectedBodyPartTypes.length > 0) {
        // If specific muscles are selected, use those
        if (this.selectedSpecificMuscles.length > 0) {
          matchesBodyParts = ex.bodyParts?.some(bp =>
            this.selectedSpecificMuscles.includes(bp)
          ) ?? false;
        } else {
          // Otherwise, check if exercise has any muscle from the selected body part types
          matchesBodyParts = ex.bodyParts?.some(bp => {
            return this.selectedBodyPartTypes.some(type => {
              const musclesInType = bodyPartTypeMap[type as BodyPartType];
              return musclesInType?.includes(bp as BodyPart);
            });
          }) ?? false;
        }
      }

      // Tags filter
      const matchesTags =
        this.selectedTags.length === 0 ||
        ex.exerciseTags?.some(tag => this.selectedTags.includes(tag));

      return matchesSearch && matchesBodyParts && matchesTags;
    });
  }
}