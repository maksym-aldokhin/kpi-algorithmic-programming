#include <chrono>
#include <iostream>
#include <map>
#include <random>
#include <vector>

constexpr std::size_t MAX_MATRIX_SIZE = 500;

void
print_matrix(const std::vector<std::vector<std::int32_t>>& matrix) {
    for (const auto& row : matrix) {
        for (const auto& element : row) {
            std::cout << std::setw(4) << element << " ";
        }
        std::cout << std::endl;
    }
}

void
fill_matrix_with_random_numbers(std::vector<std::vector<std::int32_t>>& matrix, const std::size_t size) {
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<std::int32_t> dis(-100, 100);
    matrix.reserve(size);
    for (std::size_t i = 0; i < size; ++i) {
        std::vector<std::int32_t> row(size);
        std::generate(row.begin(), row.end(), [&]() { return dis(gen); });
        matrix.push_back(std::move(row));
    }
}

void
counting_sort(std::vector<std::int32_t>& column) {
    std::size_t row = 0;
    std::map<std::int32_t, std::int32_t> count;
    for (const auto& val : column) {
        ++count[val];
    }
    for (const auto& [key, value] : count) {
        for (std::size_t j = 0; j < value; ++j) {
            column[row] = key;
            ++row;
        }
    }
}

void
bucket_sort(std::vector<std::int32_t>& column) {
    std::size_t row = 0;
    std::size_t size = column.size();
    auto num_buckets = static_cast<size_t>(std::sqrt(size));
    std::vector<std::vector<std::int32_t>> buckets(num_buckets);
    std::int32_t min_val = *std::min_element(column.begin(), column.end());
    std::int32_t max_val = *std::max_element(column.begin(), column.end());
    std::int32_t range = max_val - min_val + 1;
    std::size_t bucket_range = range / num_buckets + 1;
    for (const auto& val : column) {
        std::size_t index = (val - min_val) / bucket_range;
        buckets[index].push_back(val);
    }
    for (auto& bucket : buckets) {
        std::sort(bucket.begin(), bucket.end());
        std::uint8_t swapped;
        do {
            swapped = false;
            for (std::size_t i = 1; i < bucket.size(); ++i) {
                if (bucket[i - 1] > bucket[i]) {
                    std::swap(bucket[i - 1], bucket[i]);
                    swapped = true;
                }
            }
        } while (swapped);
    }
    for (const auto& bucket : buckets) {
        for (const auto& val : bucket) {
            column[row] = val;
            ++row;
        }
    }
}

void
sort_matrix_columns(std::vector<std::vector<int>>& matrix, const std::string& sort_type) {
    std::size_t rows = matrix.size();
    std::size_t cols = matrix[0].size();
    if (sort_type == "counting sort") {
        auto start = std::chrono::high_resolution_clock::now();
        for (std::size_t j = 0; j < cols; ++j) {
            std::vector<std::int32_t> column;
            for (auto& row : matrix) {
                column.push_back(row[j]);
            }
            counting_sort(column);
            std::size_t i = 0;
            for (auto& val : column) {
                matrix[rows - i - 1][j] = val;
                ++i;
            }
        }
        std::cout << std::endl << "Counting sort" << std::endl << "Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    } else {
        auto start = std::chrono::high_resolution_clock::now();
        for (std::size_t j = 0; j < cols; ++j) {
            std::vector<std::int32_t> column;
            for (auto& row : matrix) {
                column.push_back(row[j]);
            }
            bucket_sort(column);
            std::size_t i = 0;
            for (auto& val : column) {
                matrix[rows - i - 1][j] = val;
                ++i;
            }
        }
        std::cout << std::endl << "Bucket sort" << std::endl << "Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    }

}

int
main() {
    std::vector<std::vector<std::int32_t>> matrix;
    std::size_t size;
    do {
        std::cout << "Enter the size of the matrix: ";
        std::cin >> size;
    } while (size > MAX_MATRIX_SIZE);
    fill_matrix_with_random_numbers(matrix, size);
    std::vector<std::vector<std::int32_t>> counting_sort = matrix, bucket_sort = matrix;
    std::cout << std::endl << "Original matrix:" << std::endl;
    print_matrix(matrix);
    sort_matrix_columns(counting_sort, "counting sort");
    std::cout << "Sorted matrix:" << std::endl;
    print_matrix(counting_sort);
    sort_matrix_columns(bucket_sort, "bucket sort");
    std::cout << "Sorted matrix:" << std::endl;
    print_matrix(bucket_sort);
    return 0;
}
