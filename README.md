# SOLID Change Validator

The SOLID Change Validator is a C# library that helps developers ensure that code changes adhere to the SOLID principles of object-oriented design. It provides a set of tools to detect violations of the SOLID principles and classify them based on the project context.

## Features

- Detects violations of the following SOLID principles:
  - Single Responsibility Principle (SRP)
  - Open/Closed Principle (OCP)
  - Liskov Substitution Principle (LSP)
  - Interface Segregation Principle (ISP)
  - Dependency Inversion Principle (DIP)
- Classifies violations as "minor" or "critical" based on the project context
- Allows for exceptions to SOLID principles in certain situations (e.g., critical bug fixes, performance-critical changes)
- Provides a clear and concise report of the detected violations

## Usage

To use the SOLID Change Validator, follow these steps:

1. Install the `SolidChangeValidator` package from NuGet.
2. Create an instance of the `SolidChangeValidator` class.
3. Call the `ValidateChange` method, passing in a `CodeChange` object and a `ChangeContext` object.
4. Inspect the `ValidationResult` object to determine the decision (accept, accept with tech debt, accept with refactor, or reject) and the list of violated SOLID principles.

Here's an example:

```csharp
var validator = new SolidChangeValidator();
var change = new CodeChange("CHG-2023-789", "Update documentation");
var context = new ChangeContext();
var result = validator.ValidateChange(change, context);

Console.WriteLine(result.ToString());

```

```mermaid
graph TD
    A[Начало: Анализ изменения] --> B[Проверка принципов SOLID]
    
    %% SOLID проверки
    B --> SRP{SRP: >1 ответственности?}
    B --> OCP{OCP: Изменение кода вместо расширения?}
    B --> LSP{LSP: Нарушение контракта?}
    B --> ISP{ISP: Зависимость от неиспользуемых методов?}
    B --> DIP{DIP: Зависимость от реализации?}
    
    %% Обработка нарушений
    SRP -->|Нарушение| С{Анализ контекста}
    OCP -->|Нарушение| С
    LSP -->|Нарушение| С
    ISP -->|Нарушение| С
    DIP -->|Нарушение| С
    С --> C1{Критический баг?}
    C1 -->|Да| VALID[Допустимо]
    C1 -->|Нет| C2{Небольшой проект?}
    C2 -->|Да| VALID
    C2 -->|Нет| C3{Временное решение?}
    C3 -->|Да| VALID
    C3 -->|Нет| C4{Производительность критична?}
    C4 -->|Да| VALID
    C4 -->|Нет| C5{Технические ограничения?}
    C5 -->|Да| VALID
    C5 -->|Нет| INVALID[Недопустимо]
    
    %% Решения
    VALID --> TECHDEBT[Принять с техдолгом]
    INVALID --> REJECT[Отклонить]
    
    %% Нет нарушений
    SRP -->|Нет| APPROVE
    OCP -->|Нет| APPROVE
    LSP -->|Нет| APPROVE
    ISP -->|Нет| APPROVE
    DIP -->|Нет| APPROVE
    APPROVE[Принять изменение]
```