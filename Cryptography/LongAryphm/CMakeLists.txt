cmake_minimum_required(VERSION 3.14)
project(LongAryphm)

set(CMAKE_CXX_STANDARD 14)

set(SOURCE_EXE LongAryphm
        LongAryphm/main.cpp)
add_executable(LongAryphm
        LongAryphm/main.cpp)

include_directories(LongAryphm/Lib)

set(SOURCE_LIB LongAryphm/LongAryphm/LongInts.cpp)
set(SOURCE_LIB LongAryphm/LongAryphm/Lib/LongInts.h)
set(SOURCE_LIB LongAryphm/LongAryphm/Lib/chin_theor.h)
