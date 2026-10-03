using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Service;

public class StudentAndCategoryExistsRule : IParticipationAcceptanceRule
{
    private readonly IStudentRepository _studentRepository;
    private readonly IParticipationCategoryRepository _categoryRepository;

    public StudentAndCategoryExistsRule(
        IStudentRepository studentRepository,
        IParticipationCategoryRepository categoryRepository)
    {
        _studentRepository = studentRepository;
        _categoryRepository = categoryRepository;
    }

    public RuleEvaluationResult Check(ParticipationRecord record)
    {
        try
        {
            _studentRepository.GetById(record.Student.Id);
        }
        catch (KeyNotFoundException)
        {
            return RuleEvaluationResult.Failure("Student does not exist.");
        }

        try
        {
            _categoryRepository.GetById(record.Category.Id);
        }
        catch (KeyNotFoundException)
        {
            return RuleEvaluationResult.Failure("Participation category does not exist.");
        }

        return RuleEvaluationResult.Success();
    }
}