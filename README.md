# CNPJValidatorV2

Uma biblioteca C# moderna e extensível para **validação**, **formatação** e **cálculo de dígitos verificadores** de CNPJs — agora com suporte a **CNPJs alfanuméricos**, de acordo com as diretrizes previstas pela Receita Federal do Brasil.

---

## ✨ Funcionalidades

- ✅ Validação de CNPJs com ou sem formatação
- 🔤 Suporte a CNPJs **alfanuméricos** (letras maiúsculas e dígitos)
- 🧮 Cálculo preciso dos dígitos verificadores (DV), baseado no **algoritmo oficial**
- 🧼 Sanitização de entrada (remove caracteres inválidos)
- 🧾 Formatação no padrão `00.000.000/0000-00`

---

## 📐 Algoritmo de cálculo (Receita Federal)

O cálculo dos dígitos verificadores segue o manual técnico da Receita Federal:

1. O CNPJ (agora alfanumérico) possui 12 caracteres iniciais e 2 dígitos verificadores numéricos.
2. Cada caractere é convertido para um valor numérico, subtraindo **48 do valor ASCII**.
3. A sequência é processada da direita para a esquerda, multiplicando-se cada valor por **pesos de 2 a 9**, de forma cíclica.
4. Soma-se os produtos e aplica-se o **módulo 11**:
   - Se o resto for menor que 2, o DV é 0
   - Caso contrário, o DV é `11 - resto`

Referência: Manual de Especificações Técnicas do CNPJ – Receita Federal do Brasil

---

## 🚀 Instalação

Via NuGet:

```bash
dotnet add package CNPJValidatorV2
```

---

## 🔍 Exemplo de uso

```csharp
using CNPJValidatorV2.Core;

// Verifica se um CNPJ é válido
bool valido1 = CnpjValidator.IsValid("12.345.678/0001-95"); // true
bool valido2 = CnpjValidator.IsValid("12.ABC.345/01DE-35"); // true
bool valido3 = CnpjValidator.IsValid("12.aBC.345/01DE-35"); // false — apenas letras maiúsculas são aceitas

// Sanitiza um CNPJ
string cnpj = "12.345.678/0001-95".SanitizeCnpj(); // "12345678000195"

// Formata um CNPJ simples
string formatado = "12345678000195".FormatCnpj(); // "12.345.678/0001-95"

// Calcula o DV a partir de um número base (alfanumérico)
string cnpjComDV = CnpjValidator.CalculateDv("12ABC34501DE"); // "12ABC34501DE35"

// Calcula e já formata
string formatadoComDV = CnpjValidator.CalculateDv("12ABC34501DE").FormatCnpj(); // "12.ABC.345/01DE-35"
```

---

## ✅ Testes automatizados

Este projeto possui testes com [xUnit](https://xunit.net/) que cobrem:

- Validação de CNPJs numéricos e alfanuméricos
- Cálculo exato dos dígitos verificadores
- Detecção de formatos inválidos
- Testes de formatação e sanitização
- Casos especiais com letras e números mistos

Execute os testes com:

```bash
dotnet test
```

---

## ⚠️ Erros tratados

O método `CalculateDv` lança exceção se o CNPJ sanitizado não tiver exatamente 12 caracteres válidos:

```csharp
Assert.Throws<ArgumentException>(() => CNPJValidator.CalculateDv("123"));
```

---

## 🧾 Compatibilidade futura
Esta biblioteca está pronta para suportar mudanças legais futuras, como a adoção oficial de CNPJs alfanuméricos. O algoritmo foi desenvolvido com base na documentação técnica da Receita Federal, já considerando essas adaptações.

---

## 📄 Licença

Este projeto está licenciado sob a licença MIT.

© 2025 Tiago Ávila Saldanha
