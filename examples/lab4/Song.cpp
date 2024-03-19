#include "Song.h"

Song createSongFromText(const std::unordered_map<std::string, std::string> &data)
{
	Song song;
	for (const auto &d : data) {
		if (d.second.empty()) {
			continue;
		}

		if (d.first == "track_id") {
			song.trackId = d.second;
		} else if (d.first == "track_name") {
			song.trackName = d.second;
		} else if (d.first == "track_artist") {
			song.trackArtist = d.second;
		} else if (d.first == "track_popularity") {
			song.trackPopularity = d.second;
		} else if (d.first == "track_album_id") {
			song.trackAlbumId = d.second;
		} else if (d.first == "track_album_name") {
			song.trackAlbumName = d.second;
		} else if (d.first == "track_album_release_date") {
			song.trackAlbumReleaseDate = d.second;
		} else if (d.first == "playlist_name") {
			song.playlistName = d.second;
		} else if (d.first == "playlist_id") {
			song.playlistId = d.second;
		} else if (d.first == "playlist_genre") {
			song.playlistGenre = d.second;
		} else if (d.first == "playlist_subgenre") {
			song.playlistSubgenre = d.second;
		} else if (d.first == "danceability") {
			song.danceability = std::atof(d.second.c_str());
		} else if (d.first == "energy") {
			song.energy = std::atof(d.second.c_str());
			;
		} else if (d.first == "key") {
			song.key = std::atoi(d.second.c_str());
			;
		} else if (d.first == "loudness") {
			song.loudness = std::atof(d.second.c_str());
			;
		} else if (d.first == "mode") {
			song.mode = std::atoi(d.second.c_str());
			;
		} else if (d.first == "speechiness") {
			song.speechiness = std::atoi(d.second.c_str());
			;
		} else if (d.first == "instrumentalness") {
			song.instrumentalness = std::atof(d.second.c_str());
			;
		} else if (d.first == "acousticness") {
			song.acousticness = std::atof(d.second.c_str());
			;
		} else if (d.first == "liveness") {
			song.liveness = std::atof(d.second.c_str());
			;
		} else if (d.first == "valence") {
			song.valence = std::atof(d.second.c_str());
			;
		} else if (d.first == "tempo") {
			song.tempo = std::atof(d.second.c_str());
			;
		} else if (d.first == "duration_ms") {
			song.durationMs = std::atoi(d.second.c_str());
			;
		}
	}
	return song;
}

Songs createSongsFromText(const std::vector<std::unordered_map<std::string, std::string>> &data)
{
	Songs songs;
	for (const auto &d : data) {
		songs.push_back(createSongFromText(d));
	}
	return songs;
}

std::unordered_map<std::string, std::string> textFromSong(const Song &song)
{
	std::unordered_map<std::string, std::string> res;
	res.insert({"track_id", song.trackId});
	res.insert({"track_name", song.trackName});
	res.insert({"track_artist", song.trackArtist});
	res.insert({"track_popularity", song.trackPopularity});
	res.insert({"track_album_id", song.trackAlbumId});
	res.insert({"track_album_name", song.trackAlbumName});
	res.insert({"track_album_release_date", song.trackAlbumReleaseDate});
	res.insert({"playlist_name", song.playlistName});
	res.insert({"playlist_id", song.playlistId});
	res.insert({"playlist_genre", song.playlistGenre});
	res.insert({"playlist_subgenre", song.playlistSubgenre});
	res.insert({"danceability", std::to_string(song.danceability)});
	res.insert({"energy", std::to_string(song.energy)});
	res.insert({"key", std::to_string(song.key)});
	res.insert({"loudness", std::to_string(song.loudness)});
	res.insert({"mode", std::to_string(song.mode)});
	res.insert({"speechiness", std::to_string(song.speechiness)});
	res.insert({"instrumentalness", std::to_string(song.instrumentalness)});
	res.insert({"acousticness", std::to_string(song.acousticness)});
	res.insert({"liveness", std::to_string(song.liveness)});
	res.insert({"valence", std::to_string(song.valence)});
	res.insert({"tempo", std::to_string(song.tempo)});
	res.insert({"duration_ms", std::to_string(song.durationMs)});
	return res;
}

std::vector<std::unordered_map<std::string, std::string>> textFromSongs(const Songs &songs)
{
	std::vector<std::unordered_map<std::string, std::string>> data;
	for (const auto &s : songs) {
		data.push_back(textFromSong(s));
	}
	return data;
}
