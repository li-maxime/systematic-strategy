# Pricer

Namespace: PricingLibrary.Computations

Class in charge of computing the price and deltas for the considered derivatives.

```csharp
public class Pricer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Pricer](./pricinglibrary.computations.pricer.md)

## Constructors

### **Pricer(BasketTestParameters)**

Constructor for the class

```csharp
public Pricer(BasketTestParameters testParams)
```

#### Parameters

`testParams` [BasketTestParameters](./pricinglibrary.dataclasses.baskettestparameters.md)<br>
The input test parameters

## Methods

### **Price(Double, Double[])**

Unique method of the class, which computes the price and deltas of a basket option.

```csharp
public PricingResults Price(double timeToMaturity, Double[] spots)
```

#### Parameters

`timeToMaturity` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The mathematical time distance between the current time and the maturity

`spots` [Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The spots of the underlyings at the current time

#### Returns

[PricingResults](./pricinglibrary.dataclasses.pricingresults.md)<br>
The price and deltas computed by the method.
