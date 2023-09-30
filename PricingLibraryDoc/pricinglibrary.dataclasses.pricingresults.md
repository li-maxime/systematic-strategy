# PricingResults

Namespace: PricingLibrary.DataClasses

Data class returned by the main method of the [Pricer](./pricinglibrary.computations.pricer.md) class.

```csharp
public class PricingResults
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PricingResults](./pricinglibrary.dataclasses.pricingresults.md)

## Properties

### **Price**

Readonly property containing the price of the option.

```csharp
public double Price { get; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Deltas**

Readonly property containing the deltas of the option.

```csharp
public Double[] Deltas { get; }
```

#### Property Value

[Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **PriceStdDev**

Readonly property containing the price standard deviation (if relevant, 0 otherwise)

```csharp
public double PriceStdDev { get; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **DeltaStdDev**

Readonly property containing the delta standard deviations (if relevant, 0 otherwise)

```csharp
public Double[] DeltaStdDev { get; }
```

#### Property Value

[Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
