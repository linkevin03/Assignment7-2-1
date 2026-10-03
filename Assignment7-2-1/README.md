
# Greg Lowther

# Assignment 7-2-1 Reflection

Replace each placeholder with a concise response of approximately one to three
complete sentences. Cite evidence from the final implementation, such as specific
classes, interfaces, constructor parameters, method calls, guards, runtime
behavior, or substitution results.

## 1. Single Responsibility Principle

Identify two classes in the final implementation with different responsibilities.
Explain how their responsibilities give them different reasons to change.

The two classes DailyLimitRule and SystemDateTransferProvider have different responsibilities.
DailyLimitRule deals with student participation, and changes if the participation limit changes.
SystemDateTimeProvider would change if the system or computers time changed.

## 2. Open/Closed Principle

Explain how another participation-acceptance rule can be added without modifying
the record-creation algorithm. Cite the abstraction and rule collection used by
the implementation.

The rules are separated from the ParticipationRecord class, which allows new rules to be
implemented through IParticipationAcceptanceRule. Because of this abstraction, the new rule
can be added with its own check() method. This doesn't modify the algorithm itself.

## 3. Liskov Substitution Principle

State the behavioral expectation shared by participation-rule implementations and
explain why the coordinator can use any implementation without rule-specific
handling.

Any use of IParticipationAcceptanceRule has to return a RuleEvaluationResult to be accepted. 
This allows those implementations to be substituted without specific handling for each change.

## 4. Interface Segregation Principle

Identify two software clients and the focused interfaces they depend on. Name at
least one operation deliberately excluded from each client's contract.

Creating participation rules depends on IParticipationAcceptanceRule. Record creators rely on IDateTimeProvider, but excludes access to any direct access to system operations like DateTime.Now.

## 5. Dependency Inversion and Constructor Injection

Identify one high-level class, one low-level implementation, and the abstraction
between them. Explain how the constructor makes that dependency explicit.

The high-level class is ParticipationRecordCoordinator, the low-level implementation is ParticipationRecordRepository, and the abstraction is IParticipationRecorder. This makes the dependency explicit by sending an IParticipationRecorder instead of creating a repository.

## 6. Composition Root and Substitution Evidence

Identify where concrete implementations are created and connected. Describe one
implementation you substituted and explain what did not have to change as a
result.

Concrete Implementations are made and connected in the Program.cs class, in Main. An implementation that is substituted is ParticipationRecordRepository by IParticipationRecorder, which means other record storages can be used without changing the rest of the classes.


# John Kim

# Assignment 7-2-1 Reflection

Replace each placeholder with a concise response of approximately one to three
complete sentences. Cite evidence from the final implementation, such as specific
classes, interfaces, constructor parameters, method calls, guards, runtime
behavior, or substitution results.

## 1. Single Responsibility Principle

Identify two classes in the final implementation with different responsibilities.
Explain how their responsibilities give them different reasons to change.

ParticipationService manages execution of saving participation. ParticipationService changes if saving workflow changes. ParticipationRecord changes if data validation changes.

## 2. Open/Closed Principle

Explain how another participation-acceptance rule can be added without modifying
the record-creation algorithm. Cite the abstraction and rule collection used by
the implementation.

The new rules can be added from implementing IParticipationAcceptanceRule, registering them in IReadOnlyList<IParticipationAcceptanceRule> in Program.cs. ParticipationService loops over the collection dynamically.

## 3. Liskov Substitution Principle

State the behavioral expectation shared by participation-rule implementations and
explain why the coordinator can use any implementation without rule-specific
handling.

Every rule implements IParticipationAcceptanceRule to evaluate the proposals. ParticipationService can get through all the results without needing special handling since all the rule classes use the same contract.

## 4. Interface Segregation Principle

Identify two software clients and the focused interfaces they depend on. Name at
least one operation deliberately excluded from each client's contract.

ParticipationService relies on IStudentRepository for GetById excluding delete. It also uses IParticipationRecordRepository for Add excluding ClearAll.

## 5. Dependency Inversion and Constructor Injection

Identify one high-level class, one low-level implementation, and the abstraction
between them. Explain how the constructor makes that dependency explicit.

ParticipationService depends on IStudentRepository interface instead of the class. IStudentRepository getting accepted as a constructor parameter has the dependency at compile time. 

## 6. Composition Root and Substitution Evidence

Identify where concrete implementations are created and connected. Describe one
implementation you substituted and explain what did not have to change as a
result.

All the concrete dependencies are instantiated with Program.cs. Swapping StudentRepository for a different one requires updating only Program.cs.

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

