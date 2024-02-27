#include <chrono>
#include <iostream>

std::int32_t
pow_iterative(std::int32_t number, std::int32_t pow) {
    std::int32_t result = 1;
    for (std::int32_t i = 0; i < pow; ++i) {
        result *= number;
    }
    return result;
}

std::int32_t
pow_recursive(std::int32_t number, std::int32_t pow) {
    if (pow <= 0) {
        return 1;
    } else {
        return number * pow_recursive(number, pow - 1);
    }
}

long double
calculate_formula_iterative(std::int32_t x, std::int32_t n) {
    long double y = 1;
    std::int32_t two_to_n, x_to_n;
    two_to_n = pow_iterative(2, n);
    x_to_n = pow_iterative(x, n);
    for (std::int32_t t = x; t <= n; ++t) {
        y *= static_cast<long double>(t * t + two_to_n) / (x_to_n + 1);
    }
    return y;
}

long double
calculate_formula_recursive(std::int32_t x, std::int32_t n, std::int32_t t, long double y, std::int32_t two_to_n, std::int32_t x_to_n) {
    if (t > n) {
        return y;
    } else {
        y *= static_cast<long double>(t * t + two_to_n) / (x_to_n + 1);
        return calculate_formula_recursive(x, n, ++t, y, two_to_n, x_to_n);
    }
}

int
main() {
    long double y;
    std::int32_t n, x;
    std::cout << "Enter a number to start from (x): ";
    std::cin >> x;
    do {
        std::cout << "Enter an upper limit (n > x): ";
        std::cin >> n;
    } while (n <= x);
    std::cout << std::endl << "Non-recursive calculation" << std::endl;
    auto start = std::chrono::high_resolution_clock::now();
    y = calculate_formula_iterative(x, n);
    std::cout << "Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    std::cout << "The result is: " << y << std::endl << std::endl;
    std::cout << "Recursive calculation" << std::endl;
    start = std::chrono::high_resolution_clock::now();
    y = calculate_formula_recursive(x, n, x, 1, pow_recursive(2, n), pow_recursive(x, n));
    std::cout << "Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    std::cout << "The result is: " << y << std::endl;
    return 0;
}
