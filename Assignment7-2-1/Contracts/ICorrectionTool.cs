using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Contracts;

public interface ICorrectionTool
{
    ParticipationRecord findRecord(Guid id);
    void UpdateRecordNotes(Guid id, string? newNotes);
    DeleteRecord
}