Character character;
Character[] characters;
boolean gameRunning = true;
int amountOfZombies = 1;
int numberOfCharacters = 100;
Zombie zombie;
Human human;

class CharacterManager {

	void setup() {
		characters = new Character[numberOfCharacters];
		character = new Character(random(15, width-15), random(15, height-15));
		zombie = new Zombie(random(15, width-15), random(15, height-15));
		human = new Human(random(15, width-15), random(15, height-15));

		for (int i = 0; i < characters.length; ++i) {
			if (i <= 98) {
				characters[i] = (human = new Human(random(15, width-15), random(15, height-15)));
			} 
			if (i == 99){
				characters[i] = (zombie = new Zombie(random(15, width-15), random(15, height-15)));
			}
		}
	}


	void draw() {
		for (int i = 0; i < characters.length; ++i) {

			characters[i].update();
			if(characters[i] instanceof Zombie) {

				for (int j = 0; j < characters.length; ++j) {
					if(roundCollision(characters[i], characters[j]) && characters[j] instanceof Human) {
						float x = characters[j].position.x;
						float y = characters[j].position.y;

						characters[j] = new Zombie(x, y);
						amountOfZombies++;
						if(amountOfZombies >= numberOfCharacters) {
							gameRunning = false;
						}
					}
				}

			}
			characters[i].draw();
		}
	}
}