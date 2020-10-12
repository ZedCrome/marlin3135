CharacterManager characterManager;
float time;
float deltaTime;
float totalTime;

void setup() {
	size(512, 512);
	characterManager = new CharacterManager();
	characterManager.setup();
}


void draw() {
	long currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;
	time = currentTime;

	background(180);

	if(gameRunning) {
		totalTime = currentTime;
		characterManager.draw();
	}
	if(!gameRunning) {
		textSize(84);
		textAlign(CENTER);
		fill(0);
		text("Game Over", width/2, height/3);
		textSize(24);
		text("All humans were infected!", width/2, height/2.6);
		text("Time alive: " + int(totalTime * 0.001) + " seconds.", width/2, height/2);
	}

}
