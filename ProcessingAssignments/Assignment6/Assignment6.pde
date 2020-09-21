boolean gameRunning = true; 
BallManager ballManager;
PVector direction;
Player myPlayer;
float deltaTime;
float time;

void setup() {
  size(512, 512);
  myPlayer = new Player(width/2, height/2);
  ballManager = new BallManager();
  ballManager.setup();

}
void draw() {
  //provides a Game Over screen when the player collides with a ball.
  if(!gameRunning) {
    textSize(84);
    textAlign(CENTER);
    fill(255);
    text("Game Over", width/2, height/3);
    return;
  }
  background(0);

  //calculate time.
  long currentTime = millis();
  deltaTime = (currentTime - time) * 0.001f;
  time = currentTime;

  ballManager.draw();
  myPlayer.draw();
}

