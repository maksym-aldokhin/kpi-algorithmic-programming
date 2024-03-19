namespace Lab4;

public static class Search
{ 
    public static IEnumerable<TrackModel> Jump(List<TrackModel> arr, string trackAlbumName)
    {
        trackAlbumName = trackAlbumName.ToLower();
        
        int step = (int)Math.Sqrt(arr.Count),
            start = 0,
            end = 0;

        while (string.Compare(arr[end].LowerTrackAlbumName, trackAlbumName) < 0 && start < arr.Count)
        {
            start = end;
            end += step;
            
            if (end > arr.Count - 1)
            {
                end = arr.Count - 1;
            }
        }

        for (; end >= start; end--)
        {
            if (arr[end].LowerTrackAlbumName.Contains(trackAlbumName))
            {
                yield return arr[end];
            }
        }
    }
    
    public static IEnumerable<TrackModel> Linear(List<TrackModel> arr, string trackAlbumName)
    {
        trackAlbumName = trackAlbumName.ToLower();

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].LowerTrackAlbumName.Contains(trackAlbumName))
            {
                yield return arr[i];
            }
        }
    }
}