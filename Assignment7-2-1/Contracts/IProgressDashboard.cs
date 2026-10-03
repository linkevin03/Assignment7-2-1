using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Contracts;

public interface IProgressDashboard
{
    List<ParticipationRecord> GetRecordsForStudent(Guid studentId);
    
    int CalculateTotalPoints(Guid studentId);
    
}