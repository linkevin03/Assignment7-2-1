using System;
using System.Collections.Generic;
using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;
using Assignment7_2_1.Service;

namespace Assignment7_2_1;

class Program
{
    static void Main(string[] args)
    {
        // Create concrete repo
        IStudentRepository studentRepo = new StudentRepository();
        IParticipationCategoryRepository categoryRepo = new ParticipationCategoryRepository();

        ParticipationRecordRepository recordRepository = new ParticipationRecordRepository();

        // Participation record repo implements both interfaces needed by coordinator and rules
        IParticipationRecorder recorder = recordRepository;
        IParticipationRecordRepository recordRepo = recordRepository;

        // Part 8
        IDateTimeProvider clock = new FixedClock(
            new DateTimeOffset(2026, 10, 3, 8, 0, 0, TimeSpan.Zero));

        // Part 7
        // IDateTimeProvider clock = new SystemClock();

        // Acceptance rules
        List<IParticipationAcceptanceRule> rules =
            new List<IParticipationAcceptanceRule>
            {
                new ActiveStudentRule(studentRepo),
                new DailyLimitRule(recordRepo),
                new SameCategoryRule(recordRepo),
                new StudentAndCategoryExistsRule(
                    studentRepo,
                    categoryRepo)
            };

        var coordinator = new ParticipationRecordCoordinator(
            recorder,
            clock,
            rules);

        Console.WriteLine("Participation System is composed.");

        // Test Accepted Record
        Console.WriteLine("\nTesting acceptance.");

        try
        {
            Guid studentId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();

            var student = new Student(
                studentId,
                "Fred",
                "fred@example.com");

            var category = new ParticipationCategory(
                categoryId,
                "Class",
                "3112 CS",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Correct answer"));

            studentRepo.Add(student);
            categoryRepo.Add(category);

            // Current coordinator gets the student and category
            // through IParticipationRecorder. Therefore, we populate the
            // record repo with an older record.
            var oldRecord = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            recorder.Save(oldRecord);

            // This record should be accepted because the old record
            // is from a previous date
            coordinator.CreateRecord(
                studentId,
                categoryId,
                "Attended class");

            Console.WriteLine(
                "PASS: Record was accepted and stored.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"FAIL: Expected success but was rejected: {ex.Message}");
        }

        // Test 2 Inactive Student
        Console.WriteLine("\nTesting rejection of inactive student.");

        try
        {
            Guid studentId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();

            var student = new Student(
                studentId,
                "Inactive Student",
                "inactive@example.com");

            student.IsActive = false;

            var category = new ParticipationCategory(
                categoryId,
                "Class",
                "3112 CS",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Correct answer"));

            studentRepo.Add(student);
            categoryRepo.Add(category);

            // Populate the record repo so the coordinator can find
            // this student and category
            var oldRecord = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            recorder.Save(oldRecord);

            // This should be rejected by ActiveStudentRule
            coordinator.CreateRecord(
                studentId,
                categoryId,
                "Should fail");

            Console.WriteLine(
                "FAIL: Inactive student was accepted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"PASS: Inactive student was rejected: {ex.Message}");
        }

        // Test 3 Daily Limit
        Console.WriteLine("\nTesting three-record daily limit.");

        try
        {
            Guid studentId = Guid.NewGuid();

            var student = new Student(
                studentId,
                "Daily Limit Student",
                "daily@example.com");

            studentRepo.Add(student);

            // Four different categories prevent SameCategoryRule
            // from being the reason the fourth record is rejected.
            var category1 = new ParticipationCategory(
                Guid.NewGuid(),
                "Class 1",
                "CS101",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Answer"));

            var category2 = new ParticipationCategory(
                Guid.NewGuid(),
                "Class 2",
                "CS102",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Answer"));

            var category3 = new ParticipationCategory(
                Guid.NewGuid(),
                "Class 3",
                "CS103",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Answer"));

            var category4 = new ParticipationCategory(
                Guid.NewGuid(),
                "Class 4",
                "CS104",
                ParticipationType.AnswerQuestion,
                new PointPolicy(2, "Answer"));

            categoryRepo.Add(category1);
            categoryRepo.Add(category2);
            categoryRepo.Add(category3);
            categoryRepo.Add(category4);

            // The current ParticipationRecordCoordinator looks up
            // students and categories through the record repository.
            //
            // Therefore, populate one old record for each category.
            // These records are from the previous date, so they do not
            // count toward today's daily limit.
            
            var oldRecord1 = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category1,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            var oldRecord2 = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category2,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            var oldRecord3 = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category3,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            var oldRecord4 = new ParticipationRecord(
                Guid.NewGuid(),
                student,
                category4,
                new DateTime(2026, 10, 2, 8, 0, 0),
                "Previous day");

            recorder.Save(oldRecord1);
            recorder.Save(oldRecord2);
            recorder.Save(oldRecord3);
            recorder.Save(oldRecord4);

            Console.WriteLine(
                "Creating first participation record...");

            coordinator.CreateRecord(
                studentId,
                category1.Id,
                "First participation");

            Console.WriteLine(
                "PASS: Record 1 accepted.");

            Console.WriteLine(
                "Creating second participation record...");

            coordinator.CreateRecord(
                studentId,
                category2.Id,
                "Second participation");

            Console.WriteLine(
                "PASS: Record 2 accepted.");

            Console.WriteLine(
                "Creating third participation record...");

            coordinator.CreateRecord(
                studentId,
                category3.Id,
                "Third participation");

            Console.WriteLine(
                "PASS: Record 3 accepted.");

            Console.WriteLine(
                "Creating fourth participation record...");

            // This should be rejected by DailyLimitRule
            coordinator.CreateRecord(
                studentId,
                category4.Id,
                "Fourth participation");

            Console.WriteLine(
                "FAIL: Fourth record was accepted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"PASS: Fourth record was rejected: {ex.Message}");
        }
    }
}

class SystemClock : IDateTimeProvider
{
    public DateTimeOffset UtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}

class FixedClock : IDateTimeProvider
{
    private readonly DateTimeOffset _fixedTime;

    public FixedClock(DateTimeOffset fixedTime)
    {
        _fixedTime = fixedTime;
    }

    public DateTimeOffset UtcNow()
    {
        return _fixedTime;
    }
}