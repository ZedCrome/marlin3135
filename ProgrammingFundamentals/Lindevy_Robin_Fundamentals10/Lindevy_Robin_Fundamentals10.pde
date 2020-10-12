GameOfLife game;


void setup() {
	size(1024, 1024);
	game = new GameOfLife();
}


void draw() {
	game.draw();
	
}
