#include <algorithm>
#include <cstdint>
#include <iostream>
#include <map>
#include <random>

class sparse_matrix {
private:
    std::map<std::pair<std::size_t, std::size_t>, std::int32_t> matrix;
    std::size_t rows;
    std::size_t cols;
public:
    explicit sparse_matrix(std::size_t rows, size_t cols) : rows(rows), cols(cols) {}
    [[nodiscard]] std::uint8_t
    is_empty() const {
        return cols == 0 && rows == 0;
    }
    void
    fill_matrix(const std::size_t& fill_percentage) {
        const std::size_t fill_count = (rows * cols * fill_percentage) / 100;
        std::random_device rd;
        std::mt19937 gen(rd());
        std::uniform_int_distribution<> dis(-100, 100);
        std::vector<std::pair<std::size_t, std::size_t>> all_positions;
        for (std::size_t i = 0; i < rows; ++i) {
            for (std::size_t j = 0; j < cols; ++j) {
                all_positions.emplace_back(i, j);
            }
        }
        std::shuffle(all_positions.begin(), all_positions.end(), gen);
        for (std::size_t i = 0; i < fill_count; ++i) {
            const std::size_t row = all_positions[i].first;
            const std::size_t col = all_positions[i].second;
            const std::int32_t value = dis(gen);
            matrix[{row, col}] = value;
        }
    }
    void
    swap_upper_lower() {
        std::map<std::pair<std::size_t, std::size_t>, std::int32_t> temp_matrix;
        for (auto& [key, value] : matrix) {
            std::size_t new_row;
            if (rows % 2 != 0 && key.first == rows / 2) {
                new_row = key.first;
            } else if (key.first < rows / 2) {
                new_row = key.first + (rows + 1) / 2;
            } else {
                new_row = key.first - (rows + 1) / 2;
            }
            temp_matrix[{new_row, key.second}] = value;
        }
        matrix = temp_matrix;
    }
    sparse_matrix
    operator*(sparse_matrix& other) {
        if (cols != other.rows) {
            return sparse_matrix(0, 0);
        }
        sparse_matrix result(rows, other.cols);
        std::map<std::size_t, std::vector<std::pair<std::size_t, std::int32_t>>> other_rows;
        for (const auto& [key, value] : other.matrix) {
            other_rows[key.first].emplace_back(key.second, value);
        }
        for (const auto& [key1, value1] : matrix) {
            const auto row = key1.first;
            const auto col = key1.second;
            for (const auto& [other_col, value2] : other_rows[col]) {
                result.matrix[{row, other_col}] += value1 * value2;
            }
        }
        return result;
    }
    friend std::ostream&
    operator<<(std::ostream& os, const sparse_matrix& sm) {
        for (std::int32_t i = 0; i < sm.rows; ++i) {
            for (std::int32_t j = 0; j < sm.cols; ++j) {
                if (sm.matrix.find({i, j}) != sm.matrix.end()) {
                    os << sm.matrix.at({i, j}) << " ";
                } else {
                    os << "0 ";
                }
            }
            os << std::endl;
        }
        return os;
    }
    void
    print_compressed() {
        std::uint8_t is_empty = 1;
        for (const auto& [key, value] : matrix) {
            std::cout << "(" << key.first << ", " << key.second << "): " << value << std::endl;
            is_empty = 0;
        }
        if (is_empty) {
            std::cerr << "Matrix is empty, nothing to print" << std::endl;
        }
    }
};

std::int32_t
main() {
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> dis(0, 100);
    sparse_matrix matrix1(3, 3);
    matrix1.fill_matrix((dis(gen)));
    std::cout << "First task" << std::endl;
    std::cout << "Original matrix:" << std::endl;
    std::cout << matrix1;
    std::cout << "Compressed matrix:" << std::endl;
    matrix1.print_compressed();
    matrix1.swap_upper_lower();
    std::cout << "Swapped upper and lower parts:" << std::endl;
    std::cout << matrix1;
    std::cout << "Compressed matrix:" << std::endl;
    matrix1.print_compressed();
    std::cout << std::endl << "Second task" << std::endl;
    sparse_matrix matrix2(3, 3);
    matrix2.fill_matrix((dis(gen)));
    std::cout << "First matrix:" << std::endl;
    std::cout << matrix1;
    std::cout << "Second matrix:" << std::endl;
    std::cout << matrix2;
    sparse_matrix result = matrix1 * matrix2;
    if (result.is_empty()) {
        std::cerr << "Matrices can't be multiplied" << std::endl;
        return 1;
    }
    std::cout << "Result matrix:" << std::endl;
    std::cout << result;
    std::cout << "Compressed matrix:" << std::endl;
    result.print_compressed();
    return 0;
}
