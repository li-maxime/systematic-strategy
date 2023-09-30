# Basket

Namespace: PricingLibrary.DataClasses

Class representing a basket option.

```csharp
public class Basket
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Basket](./pricinglibrary.dataclasses.basket.md)

## Properties

### **Strike**

The option strike.

```csharp
public double Strike { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Maturity**

The option maturity.

```csharp
public DateTime Maturity { get; set; }
```

#### Property Value

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **UnderlyingShareIds**

The underlying shares for the option.

```csharp
public String[] UnderlyingShareIds { get; set; }
```

#### Property Value

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Weights**

The weights of the option.

```csharp
public Double[] Weights { get; set; }
```

#### Property Value

[Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **Basket()**

```csharp
public Basket()
```
