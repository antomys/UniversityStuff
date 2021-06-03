#Makefile for specter function project

specterBinary: specterFunc.o libspecter.a
	g++ -o specterBinary specterFunc.o -L. -lspecter

specterFunc.o: specterFunc.cpp
	g++ -c specterFunc.cpp

libspecter.a: functionItself.o linearIndex.o specter2HTML.o
	ar cr libspecter.a functionItself.o linearIndex.o specter2HTML.o

functionItself.o: functionItself.cpp
	g++ -c functionItself.cpp

linearIndex.o: linearIndex.cpp
	g++ -c linearIndex.cpp

specter2HTML.o: specter2HTML.cpp
	g++ -c specter2HTML.cpp