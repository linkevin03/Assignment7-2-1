using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Contracts;

public interface IParticipationRecorder
{
    Student GetStudent(Guid studentId);

    List<ParticipationCategory> GetCategories();
    
    List<ParticipationRecord> GetStudentRecord(Guid studentId);
    
    void Save(ParticipationRecord record);
}
    
    
    
    