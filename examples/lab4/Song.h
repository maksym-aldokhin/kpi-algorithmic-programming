#pragma once

#include <ostream>
#include <string>
#include <unordered_map>
#include <vector>

struct Song
{
	std::string trackId;
	std::string trackName;
	std::string trackArtist;
	std::string trackPopularity;
	std::string trackAlbumId;
	std::string trackAlbumName;
	std::string trackAlbumReleaseDate;
	std::string playlistName;
	std::string playlistId;
	std::string playlistGenre;
	std::string playlistSubgenre;
	double danceability{0.};
	double energy{0.};
	int key{0};
	double loudness{0.};
	int mode{0};
	double speechiness{0.};
	double acousticness{0.};
	double instrumentalness{0.};
	double liveness{0.};
	double valence{0.};
	double tempo{0.};
	int durationMs{0};
};

using Songs = std::vector<Song>;

inline std::ostream &operator<<(std::ostream &os, const Song &s)
{
	return os << std::string("name: \"") << s.trackName << std::string("\" artist: \"")
	          << s.trackArtist << std::string("\" album name: \"") << s.trackAlbumName
	          << std::string("\"\n");
};

inline std::ostream &operator<<(std::ostream &os, const Songs &songs)
{
	for (const auto s : songs) {
		os << s;
	}
	return os;
};

inline bool operator<(const Song &l, const Song &r)
{
	return l.trackName < r.trackName;
}

inline bool operator==(const Song &l, const Song &r)
{
	const auto lt = std::make_tuple(l.trackId, l.trackAlbumId, l.trackName, l.trackArtist);
	const auto rt = std::make_tuple(r.trackId, r.trackAlbumId, r.trackName, r.trackArtist);
	return lt == rt;
}

Song createSongFromText(const std::unordered_map<std::string, std::string> &data);
Songs createSongsFromText(const std::vector<std::unordered_map<std::string, std::string>> &data);

std::unordered_map<std::string, std::string> textFromSong(const Song &song);
std::vector<std::unordered_map<std::string, std::string>> textFromSongs(const Songs &songs);
