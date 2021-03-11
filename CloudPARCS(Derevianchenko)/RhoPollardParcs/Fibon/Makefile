all: run

clean:
	rm -f src/*.class out/Fibon.jar

Fibon.jar: out/parcs.jar src/*.java
	@javac -cp out/parcs.jar src/*.java
	@jar cf out/Fibon.jar -C src .
	@rm -f src/*.class

run: Fibon.jar
	@cd out && java -cp 'parcs.jar:Fibon.jar' Fibon
