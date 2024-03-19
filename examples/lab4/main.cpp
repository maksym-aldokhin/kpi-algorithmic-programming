#include <algorithm>
#include <iostream>
#include <vector>


#include "CsvHelper.h"
#include "Song.h"
#include "SongSearch.h"

namespace {

std::vector<std::string> orderToColomn{
    "track_id",
    "track_name",
    "track_artist",
    "track_popularity",
    "track_album_id",
    "track_album_name",
    "track_album_release_date",
    "playlist_name",
    "playlist_id",
    "playlist_genre",
    "playlist_subgenre",
    "danceability",
    "energy",
    "key",
    "loudness",
    "mode",
    "speechiness",
    "acousticness",
    "instrumentalness",
    "liveness",
    "valence",
    "tempo",
    "duration_ms",
};

}

int main()
{
	const auto data = readFile("<<path to file>>");

	const auto songs = createSongsFromText(data);

	std::string patern;
	char ch;
	while ((ch = std::cin.get()) != '\n') {
		patern += ch;
	}

	const auto finedSongsLine = searchSongNameLine(songs, patern);
	std::cout << "found songs with line:\n" << finedSongsLine;

	auto songsSorted = songs;
	// you must to use your function
	std::sort(songsSorted.begin(), songsSorted.end());

	const auto finedSongsBinary = searchSongNameBinary(songsSorted, patern);
	std::cout << "found songs with binary:\n" << finedSongsBinary;

	const auto finedSongsFibonacci = fibonacciSearch(songsSorted, patern);
	std::cout << "found songs with fibonacci:\n" << finedSongsFibonacci;

	const auto finedSongsJump = jumpSearch(songsSorted, patern);
	std::cout << "found songs with jump:\n" << finedSongsJump;

	const auto dataToWrite = textFromSongs(songs);

	writeFile("<<path to file>>", dataToWrite, orderToColomn);

	return 0;
}
