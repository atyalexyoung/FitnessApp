export interface Exercise {
    id: string;
    name: string;
    description?: string;
    exerciseTags: ExerciseTypeTag[];
    bodyParts: BodyPart[];
    imageUrls: string[];
    videoUrls: string[];
}

export enum ExerciseTypeTag {
    Cardio = 'Cardio',
    Strength = 'Strength',
    Powerlifting = 'Powerlifting',
    OlympicLifting = 'OlympicLifting',
    HIIT = 'HIIT',
    Bodyweight = 'Bodyweight',
    Mobility = 'Mobility',
    Stretching = 'Stretching',
    Balance = 'Balance',
    Core = 'Core',
    Endurance = 'Endurance',
    Speed = 'Speed',
    Agility = 'Agility',
    Rehab = 'Rehab'
}

export enum BodyPart {
    // Arms
    Biceps = 'Biceps',
    Triceps = 'Triceps',
    Forearms = 'Forearms',

    // Shoulders
    Deltoids = 'Deltoids',

    // Chest
    Chest = 'Chest',

    // Back
    Lats = 'Lats',
    Rhomboids = 'Rhomboids',
    Trapezius = 'Trapezius',
    LowerBack = 'LowerBack',

    // Core
    Abdominals = 'Abdominals',
    Obliques = 'Obliques',

    // Legs
    Glutes = 'Glutes',
    Quads = 'Quads',
    Hamstrings = 'Hamstrings',
    Calves = 'Calves'
}

export enum BodyPartType {
    Arms = 'Arms',
    Shoulders = 'Shoulders',
    Chest = 'Chest',
    Back = 'Back',
    Core = 'Core',
    Legs = 'Legs'
}

// Helper function to get body part type from body part
export function getBodyPartType(bodyPart: BodyPart): BodyPartType {
    switch (bodyPart) {
        case BodyPart.Biceps:
        case BodyPart.Triceps:
        case BodyPart.Forearms:
            return BodyPartType.Arms;

        case BodyPart.Deltoids:
            return BodyPartType.Shoulders;

        case BodyPart.Chest:
            return BodyPartType.Chest;

        case BodyPart.Lats:
        case BodyPart.Rhomboids:
        case BodyPart.Trapezius:
        case BodyPart.LowerBack:
            return BodyPartType.Back;

        case BodyPart.Abdominals:
        case BodyPart.Obliques:
            return BodyPartType.Core;

        case BodyPart.Glutes:
        case BodyPart.Quads:
        case BodyPart.Hamstrings:
        case BodyPart.Calves:
            return BodyPartType.Legs;
    }
}

export const bodyPartTypeMap: Record<BodyPartType, BodyPart[]> = {
    [BodyPartType.Arms]: [BodyPart.Biceps, BodyPart.Triceps, BodyPart.Forearms],
    [BodyPartType.Shoulders]: [BodyPart.Deltoids],
    [BodyPartType.Chest]: [BodyPart.Chest],
    [BodyPartType.Back]: [BodyPart.Lats, BodyPart.Rhomboids, BodyPart.Trapezius, BodyPart.LowerBack],
    [BodyPartType.Core]: [BodyPart.Abdominals, BodyPart.Obliques],
    [BodyPartType.Legs]: [BodyPart.Glutes, BodyPart.Quads, BodyPart.Hamstrings, BodyPart.Calves]
};
