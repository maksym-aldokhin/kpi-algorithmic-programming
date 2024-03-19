#pragma once

#include <string>
#include <vector>

#include "Song.h"

std::vector<Song> searchSongNameLine(const Songs &songs, const std::string &lookingNamePart);
std::vector<Song> searchSongNameBinary(const Songs &songs, const std::string &lookingNamePart);
std::vector<Song> fibonacciSearch(const Songs &songs, const std::string &lookingNamePart);
std::vector<Song> jumpSearch(const Songs &songs, const std::string &lookingNamePart);
