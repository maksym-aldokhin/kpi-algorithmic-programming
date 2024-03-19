using Microsoft.VisualBasic.FileIO;

namespace Lab4;

public static class ListExtensions
{
    public static void ParseCsv(out List<TrackModel> arr, string path)
    {
        arr = new List<TrackModel>(32833);
        var entry = new TrackModel();
        
        using (var parser = new TextFieldParser(new StreamReader(path)))
        {
            parser.Delimiters = new string[] { "," };
            parser.HasFieldsEnclosedInQuotes = false;
            parser.TrimWhiteSpace = true;

            while (!parser.EndOfData)
            {
                var readFields = parser.ReadFields();
                if (readFields != null)
                {
                    entry = TrackModel.InitFileds(readFields);
                        
                    if (entry is { } && entry.TrackId != "track_id")
                    {
                        arr.Add(entry);
                    }
                }
            }
        }
    }
}