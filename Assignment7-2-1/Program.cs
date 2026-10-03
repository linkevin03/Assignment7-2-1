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
        IParticipationRecorder recorder = new ParticipationRecordRepository();
        
        // There were issues saving a record due to UTC and how ParticipationRecord checked time
        IDateTimeProvider clock = new LocalClock();

        IStudentRepository studentRepo = new StudentRepository();
        IParticipationCategoryRepository categoryRepo = new ParticipationCategoryRepository();
        IParticipationRecordRepository recordRepo = new ParticipationRecordRepository();

        List<IParticipationAcceptanceRule> rules = new List<IParticipationAcceptanceRule>()
        {
            new ActiveStudentRule(studentRepo),
            new DailyLimitRule(recordRepo),
            new SameCategoryRule(recordRepo),
            new StudentAndCategoryExistsRule(studentRepo, categoryRepo),
        };

        var coordinator = new ParticipationRecordCoordinator(
            recorder,
            clock,
            rules);

        Console.WriteLine("Participation System is composed.");
        
        // Testing acceptance
        try
        {
            Guid validStudentId = Guid.NewGuid();
            Guid validCategoryId = Guid.NewGuid();
            
            var testStudent = new Student(validStudentId, "Fred", "fred@example.com");
            var testCategory = new ParticipationCategory(validCategoryId, "Class",
                "3112 CS", ParticipationType.AnswerQuestion, 
                new PointPolicy(2, "Correct answer"));
            
            studentRepo.Add(testStudent);
            categoryRepo.Add(testCategory);

            var dummyRecord = new ParticipationRecord(
                Guid.NewGuid(),
                testStudent,
                testCategory,
                DateTime.Now.AddMinutes(-5),
                null);
            recorder.Save(dummyRecord);

            var record = coordinator.CreateRecord(validStudentId, validCategoryId, "Attended class");
            Console.WriteLine("Record was accepted and stored in the repository");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected success but was rejected {ex.Message}");
        }
        
        // Testing rejection
        Console.WriteLine("\nTesting rejection.");
        try
        {
            Guid validStudentId = Guid.NewGuid();
            Guid validCategoryId = Guid.NewGuid();
            
            var testStudent = new Student(validStudentId, "Fred", "fred@example.com");
            testStudent.IsActive = false;
            
            var testCategory = new ParticipationCategory(validCategoryId, "Class",
                "3112 CS", ParticipationType.AnswerQuestion, 
                new PointPolicy(2, "Correct answer"));
            
            studentRepo.Add(testStudent);
            categoryRepo.Add(testCategory);
            
            var dummyRecord = new ParticipationRecord(
                Guid.NewGuid(),
                testStudent,
                testCategory,
                DateTime.Now.AddMinutes(-5),
                null);
            recorder.Save(dummyRecord);

            var record = coordinator.CreateRecord(validStudentId, validCategoryId, "Should fail");
            Console.WriteLine("Expected rejection, but record was made");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Correctly rejected, {ex.Message}");
        }
        
    }
    
    class LocalClock : IDateTimeProvider
    {
        public DateTimeOffset UtcNow() => new DateTimeOffset(DateTime.Now);
    }
}
