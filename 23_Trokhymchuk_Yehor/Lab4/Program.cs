using System.Text;

namespace Lab4
{
    internal class Program
    {
        private static void UnsortedCsv(out List<TrackModel> entries, TimeLogger.TimeLogger logger)
        {
            logger.SetMessage("Parsing file").Start();
            ListExtensions.ParseCsv(out entries, "../../../Data/spotify_songs.csv");
            Console.Write("\nError with parsing 'track_album_release_date' is good error and it " +
                          "don't want to be fixed!\n\n");
            logger.Stop();
            logger.SetMessage("Sorting").Start();
            Sort.MergeRecAscVer1(entries);
            logger.Stop();
            Console.Write(logger);
            logger.Clear();
        }

        private static void SortedCsv(out List<TrackModel> entires)
        {
            Console.Write("\nParsing sorted file...\n");
            ListExtensions.ParseCsv(out entires, "../../../Data/spotify_songs_sorted.csv");
            Console.Write("Parsed.\n");
        }
        
        public static void Main(string[] args)
        {
            List<TrackModel> data = null!;
            TimeLogger.TimeLogger logger = new();
            var exportPath = "../../../Export/";
            var builder = new StringBuilder(200);

            while (true)
            {
                Console.Write("Enter track album name : ");
                string trackAlbumName = Console.ReadLine()!;

                if (data is null)
                {
                    //UnsortedCsv(out data, logger);
                    SortedCsv(out data);
                }

                Console.WriteLine("\n\nJump. Collisions:\n");
                logger.SetMessage("\n\nResults were found in ").Start(); // because of enumerable
                
                foreach (var item in Search.Jump(data, trackAlbumName))
                {
                    var line = $"Artist: {item.TrackArtist}, " +
                               $"Song: {item.TrackName}, Album: {item.TrackAlbumName}";
                    Console.WriteLine(line);
                    builder.AppendLine(line);
                }
                
                File.WriteAllText(Path.Combine(exportPath, "Jump.txt"), builder.ToString());
                builder.Clear();
                
                logger.Stop();
                Console.WriteLine(logger + "\n");
                logger.Clear();
                
                Console.WriteLine("\n\nLinear. Collisions:\n");
                logger.SetMessage("\n\nResults were found in ").Start(); // because of enumerable
                
                foreach (var item in Search.Linear(data, trackAlbumName))
                {
                    var line = $"Artist: {item.TrackArtist}, " +
                               $"Song: {item.TrackName}, Album: {item.TrackAlbumName}";
                    Console.WriteLine(line);
                    builder.AppendLine(line);
                }
                
                File.WriteAllText(Path.Combine(exportPath, "Linear.txt"), builder.ToString());
                builder.Clear();
                
                logger.Stop();
                Console.WriteLine(logger + "\n");
                logger.Clear();

                Console.WriteLine("\n\nResults were exported in two different text-files.\n");

                Console.WriteLine("To exit program type 'exit'\n");
                
                if (Console.ReadLine() == "exit")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                
                Console.Clear();
            }
        }
    }
}