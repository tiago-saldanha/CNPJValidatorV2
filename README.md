# CNPJValidatorV2

[![NuGet](https://img.shields.io/nuget/v/CNPJValidatorV2.svg)](https://www.nuget.org/packages/CNPJValidatorV2)
[![NuGet Downloads](https://img.shields.io/nuget/dt/CNPJValidatorV2.svg)](https://www.nuget.org/packages/CNPJValidatorV2)
[![Build](https://github.com/tiago-saldanha/CNPJValidatorV2/actions/workflows/coverage.yml/badge.svg)](https://github.com/tiago-saldanha/CNPJValidatorV2/actions)
[![Codecov](https://img.shields.io/codecov/c/github/tiago-saldanha/CNPJValidatorV2?branch=main)](https://codecov.io/gh/tiago-saldanha/CNPJValidatorV2)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Biblioteca .NET para validação, cálculo de dígitos verificadores e formatação de **CNPJ**, com suporte a:

- ✅ CNPJ numérico tradicional  
- ✅ CNPJ alfanumérico (com letras maiúsculas)  
- ✅ Entrada com ou sem máscara  
- ✅ Sanitização automática  

Compatível com **.NET Standard 2.0+**

---

## 📦 Instalação

Via CLI:

```bash
dotnet add package CNPJValidatorV2
```

Via Package Manager:

```powershell
Install-Package CNPJValidatorV2
```

Ou pelo Visual Studio → Manage NuGet Packages.

---

## 🚀 Compatibilidade

Por utilizar `.NET Standard 2.0`, o pacote é compatível com:

- .NET Framework 4.6.1+
- .NET Core 2.0+
- .NET 5+
- .NET 6+
- .NET 7+
- .NET 8+
- Xamarin
- Mono

---

## 🧪 Exemplos de Uso

### ✔ Validação

```csharp
using CNPJValidatorV2.Core;

bool valido = CnpjValidator.IsValid("12.345.678/0001-95");
```

---

### ✔ Sanitização

```csharp
string limpo = "12.345.678/0001-95".SanitizeCnpj();
// Resultado: 12345678000195
```

---

### ✔ Formatação

```csharp
string formatado = "12345678000195".FormatCnpj();
// Resultado: 12.345.678/0001-95
```

---

### ✔ Cálculo de Dígitos Verificadores

```csharp
string cnpjComDv = CnpjValidator.CalculateDv("12ABC34501DE");
```

---

## 🧩 Funcionalidades

- Validação de CNPJ com regra oficial de DV
- Suporte a letras maiúsculas
- Extensões para string
- Tratamento de entradas inválidas
- Cobertura de testes 100%

---

## 🧪 Testes

O projeto possui testes automatizados utilizando **xUnit**.

Para executar:

```bash
dotnet test
```

---

## 📦 Versionamento

Este projeto segue **Semantic Versioning (SemVer)**:

MAJOR.MINOR.PATCH

Exemplo:

- 1.3.0 → melhoria compatível
- 2.0.0 → mudança breaking

A publicação é automatizada via GitHub Actions ao criar uma tag:

```bash
git tag v1.3.0
git push origin v1.3.0
```

---

## 📄 Licença

Este projeto está licenciado sob a licença MIT.

© 2026 Tiago Ávila Saldanha


