# DynamicCsv
Read and write records without having to know the fields at compile time with John Close's CsvHelper

## Reading

```csharp
using (var reader = new DynamicCsvReader(path))
{
    while (reader.Read())
    {
        var record = reader.ReadRecord();
        Console.Log(reader["Name"] + ": " + reader["Phone"]);
    }
}
```

## Writing

```csharp
using (var writer = new DynamicCsv.DynamicCsvWriter(path)) {
    var record = new Dictionary<string, object>();
    record["Name"] = "Francis Bacon";
    record["Phone"] = "+1212 804 60003";
    writer.WriteRecord(record);
}
```
