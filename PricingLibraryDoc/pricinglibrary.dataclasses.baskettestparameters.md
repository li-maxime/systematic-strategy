# BasketTestParameters

Namespace: PricingLibrary.DataClasses

Class containing the necessary information to run a test, given a collection of market data.

```csharp
public class BasketTestParameters
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BasketTestParameters](./pricinglibrary.dataclasses.baskettestparameters.md)

## Properties

### **PricingParams**

The parameters to be used by the pricer

```csharp
public BasketPricingParameters PricingParams { get; set; }
```

#### Property Value

[BasketPricingParameters](./pricinglibrary.dataclasses.basketpricingparameters.md)<br>

### **BasketOption**

The description of the basket option under consideration

```csharp
public Basket BasketOption { get; set; }
```

#### Property Value

[Basket](./pricinglibrary.dataclasses.basket.md)<br>

### **RebalancingOracleDescription**

Information about the way the portfolio will be rebalanced

```csharp
public IRebalancingOracleDescription RebalancingOracleDescription { get; set; }
```

#### Property Value

[IRebalancingOracleDescription](./pricinglibrary.rebalancingoracledescriptions.irebalancingoracledescription.md)<br>

## Constructors

### **BasketTestParameters()**

```csharp
public BasketTestParameters()
```
