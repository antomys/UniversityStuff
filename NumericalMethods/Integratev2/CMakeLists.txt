cmake_minimum_required(VERSION 3.14)
project(Integrating)

set(CMAKE_CXX_STANDARD 14)

add_executable(Integrating main.cpp)

include_directories(Integrating)
set(SOURCE_LIB Integrating/poly34.cpp)
set(SOURCE_LIB Integrating/poly34.h)