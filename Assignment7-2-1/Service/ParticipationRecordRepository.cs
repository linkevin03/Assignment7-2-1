using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Service;

/// <summary>
/// Stores participation records in memory and calculates totals from managed records.
/// </summary>
public class ParticipationRecordRepository : 
    IParticipationRecordRepository,
    ICorrectionTool,
    IProgressDashboard,
    IParticipationRecorder
{
    private readonly List<ParticipationRecord> _records = new();

    /// <inheritdoc />
    public void Add(ParticipationRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        if (_records.Any(existing => existing.Id == record.Id))
        {
            throw new InvalidOperationException($"Participation record {record.Id} is already managed.");
        }

        _records.Add(record);
    }
    
    public void Save(ParticipationRecord record) => Add(record);
    
    public ParticipationRecord FindRecord(Guid id) => GetById(id);

    public void UpdateRecordNotes(Guid id, string? newNotes)
    {
        ParticipationRecord record = FindRecord(id);
        record.UpdateNotes(newNotes);
    }

    public void DeleteRecord(Guid id)
    {
        ParticipationRecord record = FindRecord(id);
        _records.Remove(record);
    }

    public List<ParticipationRecord> GetRecordsByStudent(Guid studentId)
    {
        return GetRecordsForStudent(studentId);
    }

    public List<ParticipationRecord> GetRecordsForStudent(Guid studentId)
    {
        return _records.Where(r => r.Student.Id == studentId).ToList();
    }

    public int CalculateTotalPoints(Guid studentId)
    {
        if (studentId == Guid.Empty)
        {
            throw new ArgumentException("A student identifier is required.", nameof(studentId));
        }

        int total = 0;

        foreach (ParticipationRecord record in _records)
        {
            if (record.Student.Id == studentId)
            {
                total += record.AwardedPoints;
            }
        }

        return total;
    }

    public Student GetStudent(Guid studentId)
    {
        var record = _records.FirstOrDefault(r => r.Student.Id == studentId);
        if (record != null) return record.Student;
        throw new KeyNotFoundException($"Student {studentId} was not found.");
    }
    
    public List<ParticipationCategory> GetCategories()
    {
        return _records.Select(r => r.Category).Distinct().ToList();
    }

    public List<ParticipationRecord> GetStudentRecord(Guid studentId)
    {
        return GetRecordsForStudent(studentId);
    }

    /// <inheritdoc />
    public ParticipationRecord GetById(Guid id)
    {
        return _records.Find(record => record.Id == id)
            ?? throw new KeyNotFoundException($"Participation record {id} was not found.");
    }

    /// <inheritdoc />
    public List<ParticipationRecord> GetAll()
    {
        return new List<ParticipationRecord>(_records);
    }

    /// <inheritdoc />
    public int GetTotalPointsForStudent(Guid studentId)
    {
        return CalculateTotalPoints(studentId);
    }

    /// <inheritdoc />
    public void UpdateNotes(Guid id, string? notes)
    {
        UpdateRecordNotes(id, notes);
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        DeleteRecord(id);
    }
}
