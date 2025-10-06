import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { BodyPart, Exercise, ExerciseTypeTag } from '../../shared/models/exercise';
import { ExerciseCard } from "../../shared/components/exercise-card/exercise-card";
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-home',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ExerciseCard,
    CommonModule,
    MatGridListModule,
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class HomeComponent {
  exerciseList: Exercise[] = [
    {
      id: 'bench-press',
      name: 'Bench Press',
      description: 'A compound pushing exercise that primarily targets the chest, with secondary emphasis on triceps and shoulders.',
      exerciseTags: [ExerciseTypeTag.Strength, ExerciseTypeTag.Powerlifting],
      bodyParts: [BodyPart.Chest, BodyPart.Triceps, BodyPart.Deltoids],
      imageUrls: ['https://via.placeholder.com/400x300?text=Bench+Press'],
      videoUrls: []
    },
    {
      id: 'squat',
      name: 'Barbell Back Squat',
      description: 'The king of leg exercises. A compound movement targeting the entire lower body.',
      exerciseTags: [ExerciseTypeTag.Strength, ExerciseTypeTag.Powerlifting],
      bodyParts: [BodyPart.Quads, BodyPart.Glutes, BodyPart.Hamstrings],
      imageUrls: ['assets/exercises/squat.jpg'],
      videoUrls: []
    },
    {
      id: 'deadlift',
      name: 'Deadlift',
      description: 'A full-body compound exercise focusing on posterior chain development.',
      exerciseTags: [ExerciseTypeTag.Strength, ExerciseTypeTag.Powerlifting],
      bodyParts: [BodyPart.Hamstrings, BodyPart.Glutes, BodyPart.LowerBack, BodyPart.Trapezius],
      imageUrls: ['assets/exercises/deadlift.jpg'],
      videoUrls: []
    },
    {
      id: 'pull-up',
      name: 'Pull-Up',
      description: 'A bodyweight exercise that builds back width and arm strength.',
      exerciseTags: [ExerciseTypeTag.Strength, ExerciseTypeTag.Bodyweight],
      bodyParts: [BodyPart.Lats, BodyPart.Biceps, BodyPart.Rhomboids],
      imageUrls: ['assets/exercises/pull-up.jpg'],
      videoUrls: []
    },
    {
      id: 'overhead-press',
      name: 'Overhead Press',
      description: 'A vertical pushing movement that builds shoulder strength and stability.',
      exerciseTags: [ExerciseTypeTag.Strength],
      bodyParts: [BodyPart.Deltoids, BodyPart.Triceps],
      imageUrls: ['assets/exercises/overhead-press.jpg'],
      videoUrls: []
    },
    {
      id: 'barbell-row',
      name: 'Barbell Row',
      description: 'A horizontal pulling exercise for back thickness and strength.',
      exerciseTags: [ExerciseTypeTag.Strength],
      bodyParts: [BodyPart.Lats, BodyPart.Rhomboids, BodyPart.Trapezius, BodyPart.Biceps],
      imageUrls: ['assets/exercises/barbell-row.jpg'],
      videoUrls: []
    },
    {
      id: 'dumbbell-curl',
      name: 'Dumbbell Bicep Curl',
      description: 'An isolation exercise for bicep development.',
      exerciseTags: [ExerciseTypeTag.Strength],
      bodyParts: [BodyPart.Biceps, BodyPart.Forearms],
      imageUrls: ['assets/exercises/dumbbell-curl.jpg'],
      videoUrls: []
    },
    {
      id: 'tricep-dip',
      name: 'Tricep Dips',
      description: 'A bodyweight exercise targeting the triceps and chest.',
      exerciseTags: [ExerciseTypeTag.Strength, ExerciseTypeTag.Bodyweight],
      bodyParts: [BodyPart.Triceps, BodyPart.Chest, BodyPart.Deltoids],
      imageUrls: ['assets/exercises/tricep-dip.jpg'],
      videoUrls: []
    },
    {
      id: 'leg-press',
      name: 'Leg Press',
      description: 'A machine-based compound exercise for lower body mass building.',
      exerciseTags: [ExerciseTypeTag.Strength],
      bodyParts: [BodyPart.Quads, BodyPart.Glutes, BodyPart.Hamstrings],
      imageUrls: ['assets/exercises/leg-press.jpg'],
      videoUrls: []
    },
    {
      id: 'lat-pulldown',
      name: 'Lat Pulldown',
      description: 'A machine-based pulling exercise for lat development.',
      exerciseTags: [ExerciseTypeTag.Strength],
      bodyParts: [BodyPart.Lats, BodyPart.Biceps, BodyPart.Rhomboids],
      imageUrls: ['assets/exercises/lat-pulldown.jpg'],
      videoUrls: []
    },
    {
      id: 'plank',
      name: 'Plank',
      description: 'An isometric core strengthening exercise.',
      exerciseTags: [ExerciseTypeTag.Core, ExerciseTypeTag.Bodyweight],
      bodyParts: [BodyPart.Abdominals, BodyPart.Obliques],
      imageUrls: ['assets/exercises/plank.jpg'],
      videoUrls: []
    },
    {
      id: 'burpees',
      name: 'Burpees',
      description: 'A full-body cardio exercise that builds endurance and burns calories.',
      exerciseTags: [ExerciseTypeTag.Cardio, ExerciseTypeTag.HIIT, ExerciseTypeTag.Bodyweight],
      bodyParts: [BodyPart.Chest, BodyPart.Quads, BodyPart.Glutes, BodyPart.Abdominals],
      imageUrls: ['assets/exercises/burpees.jpg'],
      videoUrls: []
    },
    {
      id: 'running',
      name: 'Running',
      description: 'Cardiovascular exercise for building endurance and leg strength.',
      exerciseTags: [ExerciseTypeTag.Cardio, ExerciseTypeTag.Endurance],
      bodyParts: [BodyPart.Quads, BodyPart.Hamstrings, BodyPart.Calves, BodyPart.Glutes],
      imageUrls: ['assets/exercises/running.jpg'],
      videoUrls: []
    },
    {
      id: 'jump-rope',
      name: 'Jump Rope',
      description: 'A high-intensity cardio exercise that improves coordination and agility.',
      exerciseTags: [ExerciseTypeTag.Cardio, ExerciseTypeTag.Agility, ExerciseTypeTag.Speed],
      bodyParts: [BodyPart.Calves, BodyPart.Forearms],
      imageUrls: ['assets/exercises/jump-rope.jpg'],
      videoUrls: []
    },
    {
      id: 'yoga-flow',
      name: 'Yoga Flow',
      description: 'A series of yoga poses that improve flexibility, balance, and mobility.',
      exerciseTags: [ExerciseTypeTag.Mobility, ExerciseTypeTag.Stretching, ExerciseTypeTag.Balance],
      bodyParts: [BodyPart.Hamstrings, BodyPart.Glutes, BodyPart.LowerBack, BodyPart.Deltoids],
      imageUrls: ['assets/exercises/yoga-flow.jpg'],
      videoUrls: []
    }
  ]
}
