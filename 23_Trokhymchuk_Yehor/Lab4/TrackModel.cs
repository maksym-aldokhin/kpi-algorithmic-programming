using System;

namespace Lab4
{
    public class TrackModel : IComparable<TrackModel>
    {
        public string TrackId = null!;
        public string TrackName = null!;
        public string TrackArtist = null!;
        public string TrackPopularity = null!;
        public string TrackAlbumId = null!;
        public string TrackAlbumName = null!;
        public DateTime TrackAlbumReleaseDate;
        public string PlaylistName = null!;
        public string PlaylistId = null!;
        public string PlaylistGenre = null!;
        public string PlaylistSubgenre = null!;
        public float Danceability;
        public float Energy;
        public byte Key;
        public float Loudness;
        public bool Mode;
        public float Speechiness;
        public float Acousticness;
        public float Instrumentalness;
        public float Liveness;
        public float Valence;
        public float Tempo;
        public int DurationMs;

        private string? _lowerTrackAlbumName;
        public string LowerTrackAlbumName
        {
            get
            {
                if (_lowerTrackAlbumName is null)
                {
                    if (TrackAlbumName[0] == '\"' && TrackAlbumName[TrackAlbumName.Length - 1] == '\"')
                    {
                        _lowerTrackAlbumName = TrackAlbumName.Substring(1, TrackAlbumName.Length - 1).ToLower();
                    }
                    else
                    {
                        _lowerTrackAlbumName = TrackAlbumName.ToLower();
                    }
                }

                return _lowerTrackAlbumName;
            }
        }
        
        public static TrackModel? InitFileds(string[] values)
        {
            TrackModel model = new();
            int i = 0;
            
            try
            {
                model.TrackId = values[i++];
                
                // e.g. values[0] = "Hell, values[1] = to, values [2] = world" is converted to "Hell to world"
                model.TrackName = values.ConcatBlocks(ref i);
                model.TrackArtist = values.ConcatBlocks(ref i);
                model.TrackPopularity = values[i++];
                model.TrackAlbumId = values[i++];
                model.TrackAlbumName = values.ConcatBlocks(ref i);

                if (values[i].Length > 10)
                {
                    model.TrackAlbumReleaseDate = DateTime.Parse(values[i]);
                }
                i++;

                model.PlaylistName = values.ConcatBlocks(ref i);
                model.PlaylistId = values[i++];
                model.PlaylistGenre = values[i++];
                model.PlaylistSubgenre = values[i++];
                model.Danceability = float.Parse(values[i++]);
                model.Energy = float.Parse(values[i++]);
                model.Key = byte.Parse(values[i++]);
                model.Loudness = float.Parse(values[i++]);
                model.Mode = values[i++] == "1";
                model.Speechiness = float.Parse(values[i++]);
                model.Acousticness = float.Parse(values[i++]);
                model.Instrumentalness = float.Parse(values[i++]);
                model.Liveness = float.Parse(values[i++]);
                model.Valence = float.Parse(values[i++]);
                model.Tempo = float.Parse(values[i++]);
                model.DurationMs = int.Parse(values[i]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }

            return model;
        }

        public int CompareTo(TrackModel? other)
        {
            if (other is null)
            {
                throw new NullReferenceException();
            }

            return string.Compare(LowerTrackAlbumName, other.LowerTrackAlbumName);
        }

        public override bool Equals(object? obj)
        {
            if (obj is TrackModel model)
            {
                return model.TrackId == TrackId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return TrackId.GetHashCode();
        }

        public override string ToString() =>
            $"{TrackId},{TrackName},{TrackArtist},{TrackPopularity},{TrackAlbumId},{TrackAlbumName}," +
            $" {TrackAlbumReleaseDate},{PlaylistName},{PlaylistId},{PlaylistGenre},{PlaylistSubgenre},{Danceability}," +
            $" {Energy},{Key},{Loudness},{Mode},{Speechiness},{Acousticness},{Instrumentalness},{Liveness},{Valence}," +
            $" {Tempo},{DurationMs}";
        
    }
}