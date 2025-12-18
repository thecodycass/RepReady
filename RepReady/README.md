## Database Schemas
__Users__
- id: int
- firstName: string
- lastName: string
- email: string
- createdAt: DateTime

__ExerciseCategories__
- id: int
- name: string

__Exercises__
- id: int
- name: string
- category: string
- createdAt: DateTime

__WorkoutTemplates__
- id: int
- userId: int
- name: string
- description: string
- createdAt: string

__TemplateExercises__
- id: int
- workoutTemplateId: int
- exerciseId: int
- sortOrder: int
- defaultSets: int
- defaultReps: int

__WorkoutSessions__
- id: int
- userId: int
- workoutTemplateId: int
- sessionDate: DateTime
- status: string
- createdAt: DateTime

_(Composite Index for __WorkoutSessions__)_
- userId
- sessionDate

__SessionSets__
- id:  int
- workoutSessionId: int
- exerciseId: int
- setNumber: int
- reps: int
- weight: decimal
- createdAt: DateTime