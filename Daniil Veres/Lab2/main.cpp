#include <chrono>
#include <iostream>

long double
calculate_formula(std::int32_t x, std::int32_t n) {
    long double y = 1;
    std::int32_t two_to_n = 1, x_to_n = 1;
    for (std::int32_t t = x; t <= n; ++t) {
        two_to_n *= 2;
        x_to_n *= x;
        y *= static_cast<long double>(t * t + two_to_n) / (x_to_n + 1);
    }
    return y;
}

long double
calculate_formula_recursive(std::int32_t x, std::int32_t n, std::int32_t t, long double y = 1, std::int32_t two_to_n = 1, std::int32_t x_to_n = 1) {
    if (t > n) {
        return y;
    } else {
        two_to_n *= 2;
        x_to_n *= x;
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
    y = calculate_formula(x, n);
    std::cout <<"Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    std::cout << "The result is: " << y << std::endl << std::endl;
    std::cout << "Recursive calculation" << std::endl;
    start = std::chrono::high_resolution_clock::now();
    y = calculate_formula_recursive(x, n, x);
    std::cout <<"Time elapsed: " << std::chrono::duration<double, std::milli>(std::chrono::high_resolution_clock::now() - start).count() << " ms" << std::endl;
    std::cout << "The result is: " << y << std::endl;
    return 0;
}
