namespace Ideas;

public class ChangeContext
{
    public bool IsCriticalBugFix { get; set; }
    public bool IsSmallProject { get; set; }
    public bool IsTemporarySolution { get; set; }
    public bool IsPerformanceCritical { get; set; }
    public bool HasTechnicalLimitations { get; set; }
    public bool IsCoreSystemComponent { get; set; }
}
