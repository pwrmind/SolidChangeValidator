namespace Ideas;

public class SolidChangeValidatorTestScenario
{
    public string Name { get; }
    public ChangeContext Context { get; }
    public CodeChange Change { get; }
    public ValidationDecision ExpectedDecision { get; }

    public SolidChangeValidatorTestScenario(
        string name,
        ChangeContext context,
        CodeChange change,
        ValidationDecision expectedDecision)
    {
        Name = name;
        Context = context;
        Change = change;
        ExpectedDecision = expectedDecision;
    }
}
