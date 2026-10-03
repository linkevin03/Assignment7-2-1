using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Service;

public class ActiveStudentRule : IParticipationAcceptanceRule
{
    private readonly IStudentRepository _studentRepository;

    public ActiveStudentRule(IStudentRepository studentRepository)
    {
        _studentRepository =  studentRepository;
    }

    public RuleEvaluationResult Check(ParticipationRecord record)
    {
        try
        {
            var student = _studentRepository.GetById(record.Student.Id);
            if (student == null || !student.IsActive)
            {
                return RuleEvaluationResult.Failure("Student is not active.");
            }
        }
        catch (KeyNotFoundException)
        {
            return RuleEvaluationResult.Failure("Student does not exist.");
        }

        return RuleEvaluationResult.Success();
    }
}