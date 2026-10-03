using Assignment7_2_1.Domain;

namespace Assignment7_2_1.Contracts;

public interface IParticipationAcceptanceRule
{
    RuleEvaluationResult Check(ParticipationRecord record);
}