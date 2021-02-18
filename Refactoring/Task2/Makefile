all: parser

clean:
	rm -f dataset.yy.cpp dataset.tab.{cpp,hpp} parser example.txt

dataset.yy.cpp: dataset.l
	flex -o dataset.yy.cpp dataset.l

dataset.tab.cpp: dataset.y
	yacc -o dataset.tab.cpp -d dataset.y

parser: dataset.yy.cpp dataset.tab.cpp
	g++ -D_GLIBCXX_USE_CXX11_ABI=0 -w -o parser dataset.yy.cpp dataset.tab.cpp
