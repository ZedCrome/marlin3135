class Human extends Character {
	int r = int(random(140, 180));
	int g = int(random(62, 102));
	int b = int(random(25, 65));

	Human(float x, float y) {
		super(x,y);
		characterSize = int(random(15, 25));
		
	}

	void draw() {
		push();
		translate(position.x, position.y);
		fill(r, g, b);
		ellipse(0, 0, characterSize, characterSize);
		pop();
	}
}