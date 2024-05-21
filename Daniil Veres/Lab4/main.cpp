#include <algorithm>
#include <cstdint>
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

constexpr std::string_view TABLE_NAME = "spotify_songs.csv";
constexpr std::string_view SORTED_TABLE = "spotify_songs_sorted.csv";
constexpr std::string_view TABLE_HEADER = "track_id,track_name,track_artist,track_popularity,track_album_id,track_album_name,track_album_release_date,playlist_name,playlist_id,playlist_genre,playlist_subgenre,danceability,energy,key,loudness,mode,speechiness,acousticness,instrumentalness,liveness,valence,tempo,duration_ms";
constexpr std::string_view WORD_TO_SEARCH_LINEAR = "you";
constexpr std::string_view LINE_TO_SEARCH_FIBONACCI = "My Nigga";
constexpr std::string_view LINEAR_SEARCH = "linear_search.txt";
constexpr std::string_view FIBONACCI_SEARCH = "fibonacci_search.txt";
constexpr char SEPARATOR = ',';

std::size_t ID_LENGTH;

enum class error_code {
    FILE_NOT_FOUND = 1,
    TABLE_HEADER_NOT_CORRECT
};

void
print_string(const std::string& string, std::string_view file_name) {
    std::ofstream output_file(std::string(file_name), std::ios_base::app);
    std::cout << string << std::endl;
    output_file << string << std::endl;
}

std::string
parse_name(const std::string& line) {
    std::string song_name;
    std::size_t pos = ID_LENGTH;
    if (line[pos] == '"') {
        while (pos < line.length()) {
            song_name += line[pos];
            if (line[pos] == '"' && line[pos + 1] == SEPARATOR) {
                break;
            }
            ++pos;
        }
    } else {
        while (pos < line.length() && line[pos] != SEPARATOR) {
            song_name += line[pos++];
        }
    }
    return song_name;
}

void
linear_search(const std::string& word_to_search, std::ifstream& table) {
    std::ofstream output_file((std::string(LINEAR_SEARCH)));
    if (!output_file.is_open()) {
        std::cerr << "Error: File not found" << std::endl;
        exit(static_cast<std::int32_t>(error_code::FILE_NOT_FOUND));
    }
    std::string line;
    std::getline(table, line);
    ID_LENGTH = line.find(SEPARATOR) + 1;
    std::size_t word_pos = 0, current_line = 2, occurrences = 0;
    std::size_t word_length = word_to_search.length();
    std::cout << "Word \"" << word_to_search << "\" found in the next lines:" << std::endl;
    while (std::getline(table, line)) {
        const auto song_name = parse_name(line);
        for (std::size_t pos = 0; pos < song_name.length(); ++pos) {
            if (tolower(song_name[pos]) == word_to_search[word_pos] || toupper(song_name[pos]) == word_to_search[word_pos]) {
                if (word_pos + 1 == word_length && (song_name[pos + 1] == ' ' || song_name[pos + 1] == '"' || pos + 1 == song_name.length())) {
                    std::cout << '(' << current_line << ") ";
                    print_string(song_name, LINEAR_SEARCH);
                    ++occurrences;
                    break;
                } else if (word_pos + 1 == word_length && isalpha(song_name[pos + 1])) {
                    word_pos = 0;
                    continue;
                }
                ++word_pos;
                continue;
            } else if (word_pos != 0) {
                word_pos = 0;
            }
        }
        word_pos = 0;
        ++current_line;
    }
    std::cout << "Total occurrences: " << occurrences << std::endl << std::endl;
}

void
sort_table(std::ifstream& table, std::string_view sorted_table) {
    std::vector<std::string> lines;
    std::string line;
    std::getline(table, line);
    while (std::getline(table, line)) {
        lines.push_back(line);
    }
    std::sort(lines.begin(), lines.end(), [](const std::string& a, const std::string& b) {
        std::string item_a, item_b;
        item_a = parse_name(a);
        item_b = parse_name(b);
        return item_a < item_b;
    });
    std::ofstream output_file((std::string(sorted_table)));
    if (!output_file.is_open()) {
        std::cerr << "Error: File not found" << std::endl;
        exit(static_cast<std::int32_t>(error_code::FILE_NOT_FOUND));
    }
    output_file << TABLE_HEADER << std::endl;
    for (const auto& sorted_line : lines) {
        output_file << sorted_line << std::endl;
    }
    std::cout << "Table sorted successfully" << std::endl << std::endl;
}

std::uint8_t
fibonacci_search_helper(const std::vector<std::string>& lines, const std::string& line_to_search) {
    std::int32_t fib2 = 0, fib1 = 1, fib = fib1 + fib2, offset = -1;
    auto n = static_cast<std::int32_t>(lines.size());
    while (fib < n) {
        fib2 = fib1;
        fib1 = fib;
        fib = fib1 + fib2;
    }
    while (fib > 1) {
        std::int32_t i = std::min(offset + fib2, n - 1);
        std::stringstream ss(lines[i]);
        std::string item;
        item = parse_name(lines[i]);
        if (item == line_to_search) {
            std::cout << "(" << i + 2 << ") ";
            print_string(parse_name(lines[i]), FIBONACCI_SEARCH);
            return 1;
        } else if (item < line_to_search) {
            fib = fib1;
            fib1 = fib2;
            fib2 = fib - fib1;
            offset = i;
        } else {
            fib = fib2;
            fib1 -= fib2;
            fib2 = fib - fib1;
        }
    }
    return 0;
}

void
fibonacci_search(const std::string& line_to_search, std::ifstream& table) {
    std::ofstream output_file((std::string(FIBONACCI_SEARCH)));
    if (!output_file.is_open()) {
        std::cerr << "Error: File not found" << std::endl;
        exit(static_cast<std::int32_t>(error_code::FILE_NOT_FOUND));
    }
    std::vector<std::string> lines;
    std::string line;
    std::getline(table, line);
    while (std::getline(table, line)) {
        lines.push_back(line);
    }
    std::cout << "Line \"" << line_to_search << "\" found:" << std::endl;
    if (!fibonacci_search_helper(lines, line_to_search)) {
        std::cout << "Line \"" << line_to_search << "\" not found." << std::endl;
    }
}

std::int32_t
main() {
    std::ifstream table((std::string(TABLE_NAME)));
    if (!table.is_open()) {
        std::cerr << "Error: File not found" << std::endl;
        exit(static_cast<std::int32_t>(error_code::FILE_NOT_FOUND));
    }
    std::string line;
    std::getline(table, line);
    if (line != TABLE_HEADER) {
        std::cerr << "Error: Table header is not correct" << std::endl;
        exit(static_cast<std::int32_t>(error_code::TABLE_HEADER_NOT_CORRECT));
    }
    std::cout << "Linear search" << std::endl << "----------------" << std::endl;
    linear_search(std::string(WORD_TO_SEARCH_LINEAR), table);
    table.clear();
    table.seekg(0);
    sort_table(table, SORTED_TABLE);
    table.close();
    table.open(std::string(SORTED_TABLE));
    if (!table.is_open()) {
        std::cerr << "Error: File not found" << std::endl;
        exit(static_cast<std::int32_t>(error_code::FILE_NOT_FOUND));
    }
    std::cout << "Fibonacci search" << std::endl << "----------------" << std::endl;
    fibonacci_search(std::string(LINE_TO_SEARCH_FIBONACCI), table);
    table.close();
    return 0;
}
