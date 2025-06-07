namespace Ideas;

public enum ValidationDecision
{
    Accept,                 // ✅ Принять
    AcceptWithTechDebt,     // ⚠️ Принять с техдолгом
    AcceptWithRefactor,     // ⚠️ Принять с рефакторингом
    Reject                  // ❌ Отклонить
}
