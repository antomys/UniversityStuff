all: run

clean:
	rm -f out/Main.jar out/RhoPollard.jar

out/Main.jar: out/parcs.jar src/Main.java src/Result.java
	@javac -cp out/parcs.jar src/Main.java src/Result.java
	@jar cf out/Main.jar -C src Main.class -C src Result.class
	@rm -f src/Main.class src/Result.class

out/RhoPollard.jar: out/parcs.jar src/RhoPollard.java src/Result.java
	@javac -cp out/parcs.jar src/RhoPollard.java src/Result.java
	@jar cf out/RhoPollard.jar -C src RhoPollard.class -C src Result.class
	@rm -f src/RhoPollard.class src/Result.class

build: out/Main.jar out/RhoPollard.jar

run: out/Main.jar out/RhoPollard.jar
	@cd out && java -cp 'parcs.jar:Main.jar' Main
