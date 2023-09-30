# WeeklyOracleDescription

Namespace: PricingLibrary.RebalancingOracleDescriptions

Info for an oracle that should trigger a rebalancing a given day in the week (or the following one if no data is available on that day)

```csharp
public class WeeklyOracleDescription : IRebalancingOracleDescription
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [WeeklyOracleDescription](./pricinglibrary.rebalancingoracledescriptions.weeklyoracledescription.md)<br>
Implements [IRebalancingOracleDescription](./pricinglibrary.rebalancingoracledescriptions.irebalancingoracledescription.md)

## Properties

### **RebalancingDay**

The day of the week the rebalancing should be triggered.

```csharp
public DayOfWeek RebalancingDay { get; set; }
```

#### Property Value

[DayOfWeek](https://docs.microsoft.com/en-us/dotnet/api/system.dayofweek)<br>

### **Type**

The rebalancing oracle type

```csharp
public RebalancingOracleType Type { get; }
```

#### Property Value

[RebalancingOracleType](./pricinglibrary.rebalancingoracledescriptions.rebalancingoracletype.md)<br>

## Constructors

### **WeeklyOracleDescription()**

Constructor

```csharp
public WeeklyOracleDescription()
```
