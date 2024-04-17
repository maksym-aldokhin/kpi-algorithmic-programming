#include <iostream>
#include <random>

constexpr std::size_t FIRST_NUMBER_SIZE = 1, SECOND_NUMBER_SIZE = 1;

class list {
private:
    struct node_t {
        std::int8_t data;
        node_t* next, *previous;
    };
    node_t* head, *tail;
public:
    list() : head(nullptr), tail(nullptr) {}
    ~list() {
        while (head != nullptr) {
            node_t* temp = head;
            head = head->next;
            delete temp;
        }
    }
    std::int8_t
    get_data() {
        return head->data;
    }
    list&
    operator++() {
        if (head != nullptr) {
            head = head->next;
        }
        return *this;
    }
    void
    add(const std::int8_t& value) {
        auto* node = new node_t;
        node->data = value;
        node->next = nullptr;
        if (head == nullptr) {
            node->previous = nullptr;
            head = tail = node;
        } else {
            node->previous = tail;
            tail->next = node;
            tail = node;
        }
    }
    void
    display() {
        node_t* current = head;
        while (current != nullptr) {
            std::cout << +current->data;
            current = current->next;
        }
        std::cout << std::endl;
    }
    std::uint8_t
    size() {
        node_t* current = head;
        uint8_t size = 0;
        while (current != nullptr) {
            ++size;
            current = current->next;
        }
        return size;
    }
    std::uint8_t
    is_bigger_number(list& other) {
        if (size() > other.size()) {
            return 1;
        } else if (size() < other.size()) {
            return 0;
        }
        node_t* current = head;
        node_t* other_current = other.head;
        while (current != nullptr) {
            if (current->data > other_current->data) {
                return 1;
            } else if (current->data < other_current->data) {
                return 0;
            }
            current = current->next;
            other_current = other_current->next;
        }
        return 2;
    }
    std::int8_t
    subtract(const list& other) {
        std::int8_t borrow = 0, sign = 1;
        node_t* current = tail;
        node_t* other_current = other.tail;
        if (is_bigger_number(const_cast<list&>(other)) == 0) {
            std::swap(current, other_current);
            sign = -1;
        }
        while (current != nullptr) {
            std::int8_t other_data = 0;
            if (other_current != nullptr) {
                other_data = other_current->data;
                other_current = other_current->previous;
            }
            auto result = current->data - other_data - borrow;
            if (result < 0) {
                borrow = 1;
                result += 10;
            } else {
                borrow = 0;
            }
            current->data = static_cast<std::int8_t>(result);
            current = current->previous;
        }
        return sign;
    }
};

std::int32_t
main() {
    list first_number, second_number;
    std::int8_t sign;
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<std::int8_t> dis(1, 9);
    first_number.add(dis(gen));
    second_number.add(dis(gen));
    dis = std::uniform_int_distribution<std::int8_t>(0, 9);
    for (std::size_t i = 0; i < FIRST_NUMBER_SIZE - 1; ++i) {
        first_number.add(dis(gen));
    }
    for (std::size_t i = 0; i < SECOND_NUMBER_SIZE - 1; ++i) {
        second_number.add(dis(gen));
    }
    first_number.display();
    second_number.display();
    sign = first_number.subtract(second_number);
    std::cout << "Result: " << (sign == -1 ? "-" : "");
    if (sign == 1) {
        if (FIRST_NUMBER_SIZE != 1) {
            while (first_number.get_data() == 0) {
                ++first_number;
            }
        }
        first_number.display();
    } else {
        if (SECOND_NUMBER_SIZE != 1) {
            while (second_number.get_data() == 0) {
                ++second_number;
            }
        }
        second_number.display();
    }
    return 0;
}
