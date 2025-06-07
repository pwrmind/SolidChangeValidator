namespace Ideas;

using System.Collections.Generic;
using System.Linq;

public class SolidChangeValidator
{
    public ValidationResult ValidateChange(CodeChange change, ChangeContext context)
    {
        // 1. Проверка контекстных исключений
        if (HasContextExceptions(context))
        {
            return new ValidationResult(
                ValidationDecision.AcceptWithTechDebt,
                "Change accepted due to context exceptions",
                change.ViolatedPrinciples
            );
        }

        // 2. Проверка SOLID-принципов
        if (!change.ViolatedPrinciples.Any())
        {
            return new ValidationResult(
                ValidationDecision.Accept,
                "No SOLID violations detected",
                new List<SolidPrinciple>()
            );
        }

        // 3. Классификация нарушений
        if (IsMinorViolation(change, context))
        {
            return new ValidationResult(
                ValidationDecision.AcceptWithRefactor,
                "Minor SOLID violations detected, requires refactoring",
                change.ViolatedPrinciples
            );
        }

        return new ValidationResult(
            ValidationDecision.Reject,
            "Critical SOLID violations detected",
            change.ViolatedPrinciples
        );
    }

    private bool HasContextExceptions(ChangeContext context)
    {
        return context.IsCriticalBugFix ||
               context.IsSmallProject ||
               context.IsTemporarySolution ||
               context.IsPerformanceCritical ||
               context.HasTechnicalLimitations;
    }

    private bool IsMinorViolation(CodeChange change, ChangeContext context)
    {
        // Незначительные нарушения:
        // - Нарушен только один принцип
        // - Изменение не затрагивает ядро системы
        return change.ViolatedPrinciples.Count == 1 &&
               !context.IsCoreSystemComponent;
    }
}
