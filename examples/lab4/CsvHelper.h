#pragma once

#include <string>
#include <unordered_map>
#include <vector>

std::vector<std::unordered_map<std::string, std::string>> readFile(const std::string &pathToFile);
void writeFile(
    const std::string &pathToWriteFile,
    const std::vector<std::unordered_map<std::string, std::string>> &data,
    const std::vector<std::string> &colomnOrder);
