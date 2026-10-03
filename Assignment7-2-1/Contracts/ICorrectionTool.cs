using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Contracts;

/// <summary>
/// 
/// </summary>
public interface ICorrectionTool
{
    ParticipationRecord FindRecord(Guid id);
    
    void UpdateRecordNotes(Guid id, string? newNotes);
    
    void DeleteRecord(Guid id);
    
    List<ParticipationRecord> GetRecordsByStudent(string studentId);
}