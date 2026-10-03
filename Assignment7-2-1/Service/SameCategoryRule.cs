using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Service;

public class SameCategoryRule : IParticipationAcceptanceRule
{
    private readonly IParticipationRecordRepository _recordRepository;
    private readonly TimeSpan _timeSpan = TimeSpan.FromMinutes(10);

    public SameCategoryRule(IParticipationRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }

    public RuleEvaluationResult Check(ParticipationRecord record)
    {
        var allRecords = _recordRepository.GetAll();
        var recentDuplicate = allRecords.Any(r =>
            r.Student.Id == record.Student.Id &&
            r.Category.Id == record.Category.Id &&
            (record.OccurredAt - r.OccurredAt).Duration() < _timeSpan);

        if (recentDuplicate)
        {
            return RuleEvaluationResult.Failure("Cannot receive the same participation category within ten minutes.");
        }
        
        return RuleEvaluationResult.Success();
    }
}