namespace Assignment7_2_1.Domain;

public class RuleEvaluationResult
{
    public bool IsSuccess { get; }
    public string? RejectionReason { get; }

    private RuleEvaluationResult(bool isSuccess, string? rejectionReason)
    {
        IsSuccess = isSuccess;
        RejectionReason = rejectionReason;
    }

    public static RuleEvaluationResult Success() => new RuleEvaluationResult(true, null);
    public static RuleEvaluationResult Failure(string reason) => new RuleEvaluationResult(false, reason);
}