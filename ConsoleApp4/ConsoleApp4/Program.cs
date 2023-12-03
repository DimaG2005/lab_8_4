public interface IPrototype<T>
{
    T Clone();
}
public class CsvData : IPrototype<CsvData>
{
    public string Content { get; set; }

    public CsvData Clone()
    {
        return new CsvData { Content = this.Content };
    }
}

public class XmlData : IPrototype<XmlData>
{
    public string Content { get; set; }

    public XmlData Clone()
    {
        return new XmlData { Content = this.Content };
    }
}

public class JsonData : IPrototype<JsonData>
{
    public string Content { get; set; }

    public JsonData Clone()
    {
        return new JsonData { Content = this.Content };
    }
}
public interface IDataAdapter<TFrom, TTo>
{
    TTo Convert(TFrom source);
}

public class CsvToJsonAdapter : IDataAdapter<CsvData, JsonData>
{
    public JsonData Convert(CsvData source)
    {
        // Логіка конвертації з CSV в JSON
        return new JsonData { Content = $"Converted JSON: {source.Content}" };
    }
}

public class XmlToJsonAdapter : IDataAdapter<XmlData, JsonData>
{
    public JsonData Convert(XmlData source)
    {
        // Логіка конвертації з XML в JSON
        return new JsonData { Content = $"Converted JSON: {source.Content}" };
    }
}
public class DataProcessor
{
    public TTo Process<TFrom, TTo>(IPrototype<TFrom> source, IDataAdapter<TFrom, TTo> adapter)
    {
        TFrom clonedSource = source.Clone();
        return adapter.Convert(clonedSource);
    }
}
class Program
{
    static void Main()
    {
        // Користувач обирає формат вихідних та цільових даних
        var sourceCsv = new CsvData { Content = "CSV Data" };
        var processor = new DataProcessor();

        // Користувач обирає адаптер (наприклад, CSV в JSON)
        var adapter = new CsvToJsonAdapter();

        // Система використовує адаптер для конвертації даних
        var resultJson = processor.Process(sourceCsv, adapter);

        // Результат
        Console.WriteLine(resultJson.Content);
    }
}
