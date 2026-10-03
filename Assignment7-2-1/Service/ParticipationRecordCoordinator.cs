namespace Assignment7_2_1.Service;

using System;
using System.Collections.Generic;
using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

public class ParticipationRecordCoordinator
{
    private readonly IParticipationRecorder _recorder;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEnumerable<IParticipationAcceptanceRule> _rules;

    public ParticipationRecordCoordinator(
        IParticipationRecorder recorder,
        IDateTimeProvider dateTimeProvider,
        IEnumerable<IParticipationAcceptanceRule> rules)
    {
        _recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));
    }
    
    public ParticipationRecord CreateRecord(Guid studentId, Guid categoryId, string? notes)
    {
        var student = _recorder.GetStudent(studentId);
        var categories = _recorder.GetCategories();

        ParticipationCategory? category = null;
        foreach (var cat in categories)
        {
            if (cat.Id == categoryId)
            {
                category = cat;
                break;
            }
        }

        if (category == null)
        {
            throw new KeyNotFoundException($"Category {categoryId} was not found.");
        }
        
        var record = new ParticipationRecord(
            id: Guid.NewGuid(),
            student: student,
            category: category,
            occurredAt: _dateTimeProvider.UtcNow().DateTime,
            notes: notes
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
        
        _recorder.Save(record);
        return record;
    }
}