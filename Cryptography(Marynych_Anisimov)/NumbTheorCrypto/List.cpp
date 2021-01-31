//
// Created by Igor Volokhovych on 10/31/2019.
//

#include "Long.h"

Long::List::List(): _start(NULL), _end(NULL), _size(0) {}

Long::List::List(const Long::List &other): _start(NULL), _end(NULL), _size(0) {
    copy(other);
}

Long::List& Long::List::operator=(const Long::List &other) {
    clear();
    copy(other);

    return *this;
}

Long::List::~List() {
    clear();
}

void Long::List::addLast(T l) {
    Node *node = new Node(l, NULL, _end);
    if (_end) {
        _end->next = node;
        _end = node;
    }
    else {
        _end = node;
        _start = node;
    }

    ++_size;
}

void Long::List::addFirst(T l) {
    Node *node = new Node(l, _start, NULL);
    if (_start) {
        _start->prev = node;
        _start = node;
    }
    else {
        _end = node;
        _start = node;
    }

    ++_size;
}

T Long::List::last() const {
    return _end ? _end->value : 0;
}

T Long::List::first() const {
    return _start ? _start->value : 0;
}

bool Long::List::removeLast() {
    if (isEmpty())
        return false;

    Node *node = _end;
    _end = _end->prev;

    if (_end)
        _end->next = NULL;
    else
        _start = NULL;

    delete node;

    --_size;

    return true;
}

bool Long::List::removeFirst() {
    if (isEmpty())
        return false;

    Long::List::Node *node = _start;
    _start = _start->next;

    if (_start)
        _start->prev = NULL;
    else
        _end = NULL;

    delete node;

    --_size;

    return true;
}

bool Long::List::isEmpty() const {
    return !_start;
}

int Long::List::size() const {
    return _size;
}

void Long::List::copy(const List &other) {
    Node *node = other._start;
    while (node) {
        addLast(node->value);
        node = node->next;
    }
}

void Long::List::clear() {
    while (_start) {
        Node *node = _start;
        _start = _start->next;
        delete node;
    }

    _size = 0;
    _end = NULL;
    _start = NULL;
}

Long::List::Node* Long::List::start() const {
    return _start;
}

Long::List::Node* Long::List::end() const {
    return _end;
}
