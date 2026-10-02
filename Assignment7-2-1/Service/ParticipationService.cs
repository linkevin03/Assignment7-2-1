namespace Assignment7_2_1.Service;

using System;
using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

public class ParticipationService
{
    private readonly IStudentRepository _students;
    private readonly IParticipationCategoryRepository _categories;
    private readonly IParticipationRecordRepository _records;

    public ParticipationService(
        IStudentRepository students,
        IParticipationCategoryRepository categories,
        IParticipationRecordRepository records)
    {
        _students = students ?? throw new ArgumentNullException(nameof(students));
        _categories = categories ?? throw new ArgumentNullException(nameof(categories));
        _records = records ?? throw new ArgumentNullException(nameof(records));
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

        _records.Add(record);
    }
}