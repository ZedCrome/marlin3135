class Zombie extends Character {


	Zombie(float x, float y) {
		super(x,y);

		characterSize = 15;

		//random velocity.
	    velocity.x = random(-20, 21);
	    velocity.y = random(-20, 21);
	}

	void draw() {
		push();
		translate(position.x, position.y);
		fill(0, 255, 0);
		ellipse(0, 0, characterSize, characterSize);
		pop();
	}

}