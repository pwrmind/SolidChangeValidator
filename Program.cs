namespace Ideas;

using System;

class Program
{
    static void Main(string[] args)
    {
        var validator = new SolidChangeValidator();

        // Пример контекста с исключениями
        var exceptionContext = new ChangeContext
        {
            IsCriticalBugFix = true,
            IsCoreSystemComponent = true
        };

        // Пример изменения с нарушениями
        var problematicChange = new CodeChange(
            "CHG-2023-456",
            "Refactor payment processing",
            string.Empty,
            string.Empty
        );

        // Тестовые сценарии
        var scenarios = GetScenarios();

        // Путь к файлу с исходным кодом, который нужно проверить
        var codeFilePath = "Program.cs";

        // Создание экземпляра детектора нарушений SOLID-принципов
        var violationDetector = new SolidViolationDetector();

        // Вызов метода для обнаружения нарушений
        var violatedPrinciples = violationDetector.DetectViolations(codeFilePath);

        // Вывод результатов
        if (violatedPrinciples.Count > 0)
        {
            Console.WriteLine("SOLID violations detected:");
            foreach (var principle in violatedPrinciples)
            {
                Console.WriteLine($"- {principle}");
            }
        }
        else
        {
            Console.WriteLine("No SOLID violations detected.");
        }

        // Проверка сценариев
        foreach (var scenario in scenarios)
        {
            Console.WriteLine($"\nScenario: {scenario.Name}");
            Console.WriteLine($"Change: {scenario.Change.Description}");

            var result = validator.ValidateChange(scenario.Change, scenario.Context);
            Console.WriteLine(result);
            Console.WriteLine($"Expected: {scenario.ExpectedDecision}, Actual: {result.Decision}");
            Console.WriteLine(new string('-', 50));
        }
    }

    
    public static IEnumerable<SolidChangeValidatorTestScenario> GetScenarios()
    {
        return new[]
        {
            new SolidChangeValidatorTestScenario(
                "Critical bug fix",
                new ChangeContext
                {
                    IsCriticalBugFix = true,
                    IsCoreSystemComponent = true
                },
                new CodeChange(
                    "CHG-2023-456",
                    "Refactor payment processing",
                    string.Empty,
                    string.Empty),
                ValidationDecision.AcceptWithTechDebt
            ),
            new SolidChangeValidatorTestScenario(
                "No violations",
                new ChangeContext(),
                new CodeChange(
                    "CHG-2023-789",
                    "Update documentation",
                    string.Empty,
                    string.Empty),
                ValidationDecision.Accept
            ),
            new SolidChangeValidatorTestScenario(
                "Minor violation in non-core",
                new ChangeContext { IsCoreSystemComponent = false },
                new CodeChange("CHG-2023-101",
                    "Add logging",
                    string.Empty,
                    string.Empty),
                ValidationDecision.AcceptWithRefactor
            ),
            new SolidChangeValidatorTestScenario(
                "Critical violation in core",
                new ChangeContext { IsCoreSystemComponent = true },
                new CodeChange(
                    "CHG-2023-202",
                    "Modify core algorithm",
                    string.Empty,
                    string.Empty),
                ValidationDecision.Reject
            )
        };
    }
}