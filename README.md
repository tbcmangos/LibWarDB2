# LibWarDB2
## What is LibWarDB2?
LibWarDB2 is a cross-platform .NET Standard 2.1 library that allows you to read and write DB2 files created by Blizzard Activison.

## Currently supported
- [ ] WDB2
- [ ] WDB3
- [ ] WDB4
- [ ] WDB5
- [ ] WDB6
- [ ] WDC1
- [ ] WDC2
- [X] WDC3

## How to use
### Loading a WDC3 DB2
```csharp
var wdc = new WDC3();
wdc.Load("<path>/item.db2");
```

### Saving a WDC3 DB2
```csharp
wdc.Save("<path>/item.db2");
```