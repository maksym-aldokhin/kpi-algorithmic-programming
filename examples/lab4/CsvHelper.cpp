#include "CsvHelper.h"

#include <fstream>
#include <iostream>
#include <sstream>

namespace {

constinit char separator{','};
constinit char separatorSentence{'"'};

}

std::vector<std::unordered_map<std::string, std::string>> readFile(const std::string &pathToFile)
{
	std::fstream fin;
	fin.open(pathToFile, std::ios::in);

	if (!fin.is_open()) {
		return {};
	}

	std::string word, line, temp;

	std::vector<std::string> header;

	// read header
	{
		std::getline(fin, line);
		std::stringstream s(line);
		while (std::getline(s, word, separator)) {
			header.push_back(word);
		}
	}

	std::vector<std::unordered_map<std::string, std::string>> res;
	// read main
	while (std::getline(fin, line)) {
		std::unordered_map<std::string, std::string> row;

		std::stringstream s(line);

		int i = 0;
		while (std::getline(s, word, separator)) {
			// if text have separator
			if (!word.empty() && word[0] == separatorSentence) {
				std::string sentence;
				std::getline(s, sentence, separatorSentence);
				word.erase(0, 1);
				word += separator;
				word += sentence;
				std::string emptyComa;
				std::getline(s, emptyComa, separator);
			}
			if (i < header.size()) {
				row.insert({header[i], word});
			}
			++i;
		}
		res.push_back(row);
	}
	fin.close();
	return res;
}

void writeFile(
    const std::string &pathToWriteFile,
    const std::vector<std::unordered_map<std::string, std::string>> &data,
    const std::vector<std::string> &colomnOrder)
{
	auto writeSeparator = [](bool isEndLine) {
		if (isEndLine) {
			return '\n';
		} else {
			return separator;
		}
	};

	std::fstream fout;
	fout.open(pathToWriteFile, std::ios::out | std::ios::app);

	if (!fout.is_open()) {
		return;
	}

	// write header
	for (auto it = colomnOrder.begin(); it != colomnOrder.end(); ++it) {
		fout << *it;
		fout << writeSeparator(it + 1 == colomnOrder.end());
	}

	for (const auto &song : data) {
		for (auto it = colomnOrder.begin(); it != colomnOrder.end(); ++it) {
			if (!song.contains(*it)) {
				fout << writeSeparator(it + 1 == colomnOrder.end());
			}
			auto element = song.at(*it);

			if (element.find(separator) != std::string::npos) {
				element = separatorSentence + element + separatorSentence;
			}

			fout << element;
			fout << writeSeparator(it + 1 == colomnOrder.end());
		}
	}
	fout.close();
}
