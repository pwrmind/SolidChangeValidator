namespace Ideas;

using System;
using System.Collections.Generic;
using System.Linq;

public class ValidationResult
{
    public ValidationDecision Decision { get; }
    public string Message { get; }
    public List<SolidPrinciple> ViolatedPrinciples { get; }
    public DateTime ValidationDate { get; } = DateTime.UtcNow;

    public ValidationResult(ValidationDecision decision, string message,
                            List<SolidPrinciple> violatedPrinciples)
    {
        Decision = decision;
        Message = message;
        ViolatedPrinciples = violatedPrinciples;
    }

    public override string ToString()
    {
        var status = Decision switch
        {
            ValidationDecision.Accept => "✅ ACCEPTED",
            ValidationDecision.AcceptWithTechDebt => "⚠️ ACCEPTED (TECH DEBT)",
            ValidationDecision.AcceptWithRefactor => "⚠️ ACCEPTED (NEEDS REFACTOR)",
            ValidationDecision.Reject => "❌ REJECTED",
            _ => "UNKNOWN"
        };

        return $"[{status}] {Message}\n" +
               $"Violations: {(ViolatedPrinciples.Any() ?
                   string.Join(", ", ViolatedPrinciples) : "None")}\n" +
               $"Validated at: {ValidationDate:yyyy-MM-dd HH:mm}";
    }
}
