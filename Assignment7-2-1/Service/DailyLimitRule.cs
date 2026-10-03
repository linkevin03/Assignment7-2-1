using Assignment7_2_1.Contracts;
using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Service;

public class DailyLimitRule : IParticipationAcceptanceRule
{
    private readonly IParticipationRecordRepository _recordRepository;
    
    public DailyLimitRule(IParticipationRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }

    public RuleEvaluationResult Check(ParticipationRecord record)
    {
        var allRecords = _recordRepository.GetAll();
        var recordsToday = allRecords.Count(r =>
            r.Student.Id == record.Student.Id &&
            r.OccurredAt.Date == record.OccurredAt.Date);

        if (recordsToday >= 3)
        {
            return RuleEvaluationResult.Failure(
                "Student has reached the maximum of 3 participation records for today.");
        }
        
        return RuleEvaluationResult.Success();
    }
}