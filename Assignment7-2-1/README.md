
# Greg Lowther

# Assignment 7-2-1 Reflection

Replace each placeholder with a concise response of approximately one to three
complete sentences. Cite evidence from the final implementation, such as specific
classes, interfaces, constructor parameters, method calls, guards, runtime
behavior, or substitution results.

## 1. Single Responsibility Principle

Identify two classes in the final implementation with different responsibilities.
Explain how their responsibilities give them different reasons to change.

Write your answer here.

## 2. Open/Closed Principle

Explain how another participation-acceptance rule can be added without modifying
the record-creation algorithm. Cite the abstraction and rule collection used by
the implementation.

Write your answer here.

## 3. Liskov Substitution Principle

State the behavioral expectation shared by participation-rule implementations and
explain why the coordinator can use any implementation without rule-specific
handling.

Write your answer here.

## 4. Interface Segregation Principle

Identify two software clients and the focused interfaces they depend on. Name at
least one operation deliberately excluded from each client's contract.

Write your answer here.

## 5. Dependency Inversion and Constructor Injection

Identify one high-level class, one low-level implementation, and the abstraction
between them. Explain how the constructor makes that dependency explicit.

Write your answer here.

## 6. Composition Root and Substitution Evidence

Identify where concrete implementations are created and connected. Describe one
implementation you substituted and explain what did not have to change as a
result.

Write your answer here.

# John Kim

# Assignment 7-2-1 Reflection

Replace each placeholder with a concise response of approximately one to three
complete sentences. Cite evidence from the final implementation, such as specific
classes, interfaces, constructor parameters, method calls, guards, runtime
behavior, or substitution results.

## 1. Single Responsibility Principle

Identify two classes in the final implementation with different responsibilities.
Explain how their responsibilities give them different reasons to change.

Write your answer here.

## 2. Open/Closed Principle

Explain how another participation-acceptance rule can be added without modifying
the record-creation algorithm. Cite the abstraction and rule collection used by
the implementation.

Write your answer here.

## 3. Liskov Substitution Principle

State the behavioral expectation shared by participation-rule implementations and
explain why the coordinator can use any implementation without rule-specific
handling.

Write your answer here.

## 4. Interface Segregation Principle

Identify two software clients and the focused interfaces they depend on. Name at
least one operation deliberately excluded from each client's contract.

Write your answer here.

## 5. Dependency Inversion and Constructor Injection

Identify one high-level class, one low-level implementation, and the abstraction
between them. Explain how the constructor makes that dependency explicit.

Write your answer here.

## 6. Composition Root and Substitution Evidence

Identify where concrete implementations are created and connected. Describe one
implementation you substituted and explain what did not have to change as a
result.

Write your answer here.

# Kevin Lin

# Assignment 7-2-1 Reflection

Replace each placeholder with a concise response of approximately one to three
complete sentences. Cite evidence from the final implementation, such as specific
classes, interfaces, constructor parameters, method calls, guards, runtime
behavior, or substitution results.

## 1. Single Responsibility Principle

Identify two classes in the final implementation with different responsibilities.
Explain how their responsibilities give them different reasons to change.

ParticipationRecordCoordinator is responsible for orchestrating the record-creation 
workflow and executing acceptance rules, whereas ActiveStudentRule is solely
responsible for enforcing that a student is active. These responsibilities
give them different reasons to change because the coordinator
would change if the overall workflow updates, while the rule would change
only if policy regarding whether a student is active changes.

## 2. Open/Closed Principle

Explain how another participation-acceptance rule can be added without modifying
the record-creation algorithm. Cite the abstraction and rule collection used by
the implementation.

A new participation-acceptance rule can be added without modifying the
record-creation algorithm by creating a class that implements the
IParticipationAcceptanceRule abstraction and adding it to the
List<IParticipationAcceptanceRule> collection in Program.cs. The 
ParticipationRecordCoordinator evaluates rules polymorphically through
this collection and the record-creation algorithm doesn't require any change.

## 3. Liskov Substitution Principle

State the behavioral expectation shared by participation-rule implementations and
explain why the coordinator can use any implementation without rule-specific
handling.

All the participation rule implementations share the behavioral expectation
of evaluating a ParticipationRecord and returning RuleEvaluationResult through
the IParticipationAcceptanceRule contract. The coordinator can use any implementation
without rule-specific handling because every rule follows this contract.

## 4. Interface Segregation Principle

Identify two software clients and the focused interfaces they depend on. Name at
least one operation deliberately excluded from each client's contract.

ParticipationRecordCoordinator depends on IParticipationRecorder, which provides
persistence (Save) and basic lookups while excluding repository management operations
like Delete that the coordinator doesn't need. While DailyLimitRule depends on
IParticipationRecordRepository to query data (GetAll()). However, it does expose CRUD operations
like (UpdateNotes and Delete) that the rule doesn't need.

## 5. Dependency Inversion and Constructor Injection

Identify one high-level class, one low-level implementation, and the abstraction
between them. Explain how the constructor makes that dependency explicit.

The high-level ParticipationRecordCoordinator depends on the low-level FixedClock
implementation through the IDateTimeProvider abstraction. The coordinator constructor
requires an IDateTimeProvider parameter making the dependency explicit.

## 6. Composition Root and Substitution Evidence

Identify where concrete implementations are created and connected. Describe one
implementation you substituted and explain what did not have to change as a
result.

Concrete implementations are created and connected inside Program.cs. 
One implementation that can be substituted is StudentRepository.
By depending on the IStudentRepository abstraction, it can be swapped out with another storage 
implementation without requiring any changes to the rest of the application.

