# [Перейти до української версії](README.md)

# QR Code Generator

An application for creating, customizing, and saving QR codes with a graphical WPF interface. The user can change QR code colors, insert a logo in the center, toggle between dark/light themes, and store a history of generated codes.

---

## Features

* Generate QR code from arbitrary text
* Preview before saving
* Change QR code and background color
* Add a logo (PNG with transparency supported)
* Save QR code to a chosen location
* Automatically save to temporary folder for history
* History of generated QR codes
* Support for dark/light theme with auto-detection
* Theme toggle button
* Remove logo button

---

## Running Locally

1. Clone the repository:

   ```bash
   git clone https://github.com/EduardKrutii/QRCodeGenerator.git
   ```

2. Open the `QRCodeGenerator.sln` solution in Visual Studio 2022 or later.

3. Install required NuGet packages if not installed automatically:

   * `QRCoder`
   * `System.Drawing.Common`
   * `Newtonsoft.Json`
   * `xUnit` (for testing)

4. Run the `QRCodeGenerator.UI` project as the startup project.

---

## Programming Principles

| Principle                                 | Description                                                                               |
| ----------------------------------------- | ----------------------------------------------------------------------------------------- |
| **SRP (Single Responsibility Principle)** | Each class has one responsibility (e.g., `QrCodeGeneratorService` generates images only). |
| **OCP (Open-Closed Principle)**           | Code is open for extension (e.g., themes) but closed for modification.                    |
| **DIP (Dependency Inversion Principle)**  | The main program depends on interfaces (e.g., `IQRCodeStorageService`).                   |
| **LSP (Liskov Substitution Principle)**   | Interfaces are used correctly without breaking logic.                                     |
| **DRY (Don't Repeat Yourself)**           | Avoid duplication in saving logic, rendering, etc.                                        |

---

## Design Patterns

| Pattern                       | File                        | Purpose                                                             |
| ----------------------------- | --------------------------- | ------------------------------------------------------------------- |
| **Service Layer**             | `QrCodeGeneratorService.cs` | Isolates QR code generation logic                                   |
| **Dependency Injection (DI)** | All services                | Services like `IQRCodeStorageService` are injected via constructors |

---

## Refactoring Techniques

| Technique                      | Usage                                                       |
| ------------------------------ | ----------------------------------------------------------- |
| **Extract Method**             | QR generation logic moved into a separate method            |
| **Rename for Clarity**         | Variable names improved (e.g., `isTemp`, `SaveQrCodeImage`) |
| **Move Method**                | Logic moved from UI to `QrCodeGeneratorService`             |
| **Encapsulate Field**          | Fields made private with access via properties              |
| **Introduce Parameter Object** | `QRCodeRecord` used to pass multiple parameters             |

---

## Project Structure

```
QRCodeGenerator/
├── QRCodeGenerator.UI/         # WPF UI
├── QRCodeGenerator.Core/       # Business logic (generation, themes)
├── QRCodeGenerator.Data/       # QR code storage and history
├── QRCodeGenerator.Tests/      # xUnit tests (limited)
```

---

## License

This project was created as part of an academic course for a lab assignment.
