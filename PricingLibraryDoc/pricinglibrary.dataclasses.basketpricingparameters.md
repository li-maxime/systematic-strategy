# BasketPricingParameters

Namespace: PricingLibrary.DataClasses

Class containing the information to price the considered option.

```csharp
public class BasketPricingParameters
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BasketPricingParameters](./pricinglibrary.dataclasses.basketpricingparameters.md)

## Properties

### **Volatilities**

The volatilities of the underlyings.

```csharp
public Double[] Volatilities { get; set; }
```

#### Property Value

[Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Correlations**

The correlations between the underlyings.

```csharp
public Double[][] Correlations { get; set; }
```

#### Property Value

[Double[][]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **BasketPricingParameters()**

```csharp
public BasketPricingParameters()
```
