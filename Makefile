SRC := main.cpp
OBJ := main.o
OUT := Main

$(OUT) : $(OBJ)
	@g++ -o $@ $<

$(OBJ) : $(SRC)
	@g++ -o $@ -c $<