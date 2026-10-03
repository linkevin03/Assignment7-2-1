namespace Assignment7_2_1.Service;

using System;
using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

public class ParticipationService
{
    private readonly IStudentRepository _students;
    private readonly IParticipationCategoryRepository _categories;
    private readonly IParticipationRecordRepository _records;
    private readonly IEnumerable<IParticipationAcceptanceRule> _rules;

    public ParticipationService(
        IStudentRepository students,
        IParticipationCategoryRepository categories,
        IParticipationRecordRepository records,
        IEnumerable<IParticipationAcceptanceRule> rules)
    {
        _students = students ?? throw new ArgumentNullException(nameof(students));
        _categories = categories ?? throw new ArgumentNullException(nameof(categories));
        _records = records ?? throw new ArgumentNullException(nameof(records));
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));
    }

    public void CreateParticipationRecord(Guid studentId, Guid categoryId, string details)
    {
        var student = _students.GetById(studentId);
        var category = _categories.GetById(categoryId);

        if (student == null || category == null)
        {
            throw new InvalidOperationException("Student or Category not found.");
        }

        var record = new ParticipationRecord(
            id: Guid.NewGuid(),
            student: student,
            category: category,
            occurredAt: DateTime.Now,
            notes: details
        );

        // Check all rules against the proposed records before adding
        foreach (var rule in _rules)
        {
            var result = rule.Check(record);
            if (!result.IsSuccess)
            {
                throw new InvalidOperationException(result.RejectionReason);
            }
        }
        
        _records.Add(record);
    }
}