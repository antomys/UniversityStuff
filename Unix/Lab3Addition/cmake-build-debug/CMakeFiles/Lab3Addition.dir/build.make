# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.16

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /opt/clion-2020.1.1/bin/cmake/linux/bin/cmake

# The command to remove a file.
RM = /opt/clion-2020.1.1/bin/cmake/linux/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /home/nhva/CLionProjects/Lab3Addition

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /home/nhva/CLionProjects/Lab3Addition/cmake-build-debug

# Include any dependencies generated for this target.
include CMakeFiles/Lab3Addition.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/Lab3Addition.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/Lab3Addition.dir/flags.make

CMakeFiles/Lab3Addition.dir/main.cpp.o: CMakeFiles/Lab3Addition.dir/flags.make
CMakeFiles/Lab3Addition.dir/main.cpp.o: ../main.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/home/nhva/CLionProjects/Lab3Addition/cmake-build-debug/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/Lab3Addition.dir/main.cpp.o"
	/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/Lab3Addition.dir/main.cpp.o -c /home/nhva/CLionProjects/Lab3Addition/main.cpp

CMakeFiles/Lab3Addition.dir/main.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Lab3Addition.dir/main.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /home/nhva/CLionProjects/Lab3Addition/main.cpp > CMakeFiles/Lab3Addition.dir/main.cpp.i

CMakeFiles/Lab3Addition.dir/main.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Lab3Addition.dir/main.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /home/nhva/CLionProjects/Lab3Addition/main.cpp -o CMakeFiles/Lab3Addition.dir/main.cpp.s

# Object files for target Lab3Addition
Lab3Addition_OBJECTS = \
"CMakeFiles/Lab3Addition.dir/main.cpp.o"

# External object files for target Lab3Addition
Lab3Addition_EXTERNAL_OBJECTS =

Lab3Addition: CMakeFiles/Lab3Addition.dir/main.cpp.o
Lab3Addition: CMakeFiles/Lab3Addition.dir/build.make
Lab3Addition: CMakeFiles/Lab3Addition.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/home/nhva/CLionProjects/Lab3Addition/cmake-build-debug/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable Lab3Addition"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/Lab3Addition.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/Lab3Addition.dir/build: Lab3Addition

.PHONY : CMakeFiles/Lab3Addition.dir/build

CMakeFiles/Lab3Addition.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/Lab3Addition.dir/cmake_clean.cmake
.PHONY : CMakeFiles/Lab3Addition.dir/clean

CMakeFiles/Lab3Addition.dir/depend:
	cd /home/nhva/CLionProjects/Lab3Addition/cmake-build-debug && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/nhva/CLionProjects/Lab3Addition /home/nhva/CLionProjects/Lab3Addition /home/nhva/CLionProjects/Lab3Addition/cmake-build-debug /home/nhva/CLionProjects/Lab3Addition/cmake-build-debug /home/nhva/CLionProjects/Lab3Addition/cmake-build-debug/CMakeFiles/Lab3Addition.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/Lab3Addition.dir/depend

