#include <chrono>
#include <iomanip>
#include <iostream>
#include <random>
#include <vector>

constexpr std::size_t MAX_MATRIX_SIZE = 500;

void
print_matrix(const std::vector<std::vector<int>> &matrix) {
    for (const auto &row : matrix) {
        for (const auto &element : row) {
            std::cout << std::setw(4) << element << " ";
        }
        std::cout << std::endl;
    }
}

std::chrono::duration<double, std::milli>
fill_matrix_with_random_numbers(std::vector<std::vector<int>> &matrix, const std::size_t size) {
    const auto start = std::chrono::high_resolution_clock::now();
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> dis(-100, 100);
    for (std::size_t i = 0; i < size; ++i) {
        std::vector<int> row;
        for (std::size_t j = 0; j < size; ++j) {
            row.push_back(dis(gen));
        }
        matrix.push_back(row);
    }
    return std::chrono::high_resolution_clock::now() - start;
}

std::chrono::duration<double, std::milli>
zero_rows_with_more_positive_than_negative(std::vector<std::vector<int>> &matrix) {
    const auto start = std::chrono::high_resolution_clock::now();
    for (auto &row : matrix) {
        std::size_t positive_count = 0;
        std::size_t negative_count = 0;
        for (const auto &element : row) {
            if (element > 0) {
                ++positive_count;
            } else if (element < 0) {
                ++negative_count;
            }
        }
        if (positive_count > negative_count) {
            for (auto &element : row) {
                element = 0;
            }
        }
    }
    return std::chrono::high_resolution_clock::now() - start;
}

std::chrono::duration<double, std::milli>
swap_first_and_third_quarters(std::vector<std::vector<int>> &matrix) {
    const auto start = std::chrono::high_resolution_clock::now();
    for (std::size_t i = matrix.size() / 2; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size() / 2; ++j) {
            std::swap(matrix[i][j], matrix[i - matrix.size() / 2][j + matrix.size() / 2]);
        }
    }
    return std::chrono::high_resolution_clock::now() - start;
}

int
main() {
    std::vector<std::vector<int>> matrix;
    std::size_t matrix_size;
    std::uint16_t task;
    do {
        std::cout << "Enter a number of the task you want to see (1/2): ";
        std::cin >> task;
    } while (task != 1 && task != 2);
    do {
        std::cout << "Enter a size of the square matrix (<=" << MAX_MATRIX_SIZE << "): ";
        std::cin >> matrix_size;
    } while (matrix_size > MAX_MATRIX_SIZE);
    std::cout << std::endl << "Generating " << matrix_size << "x" << matrix_size << " matrix for the " << (task == 1 ? "first" : "second") << " task..." << std::endl;
    auto time_elapsed = fill_matrix_with_random_numbers(matrix, matrix_size);
    print_matrix(matrix);
    std::cout << "Time elapsed: " << time_elapsed.count() << " ms" << std::endl << std::endl;
    time_elapsed = task == 1 ? zero_rows_with_more_positive_than_negative(matrix) : swap_first_and_third_quarters(matrix);
    std::cout << "Processed matrix" << std::endl;
    print_matrix(matrix);
    std::cout << "Time elapsed: " << time_elapsed.count() << " ms" << std::endl;
    return 0;
}
