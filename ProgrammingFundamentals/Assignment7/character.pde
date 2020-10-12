class Character {
  PVector position;
  PVector velocity;
  int characterSize = 30;


  public Character (float x, float y) {
    position = new PVector(x, y);
    velocity = new PVector();

    //random velocity.
    velocity.x = random(-30, 31);
    velocity.y = random(-30, 31);
  }


  void update() {
    //updates ball positions.
    position.x += velocity.x * deltaTime;
    position.y += velocity.y * deltaTime;

    edgeDetection();
  }
  

	//draws the ball.
  void draw() {
    fill(125, 200, 255);
    ellipse(position.x, position.y, characterSize, characterSize);
  }


  //Keeps the ball inside of the viewing window
  void edgeDetection() {
    if(position.x > width) {
      position.x = 0;
    }
    if(position.x < 0) {
      position.x = width;
    }
    if(position.y > height ) {
      position.y = 0;
    }
    if(position.y < 0) {
      position.y = height;
    }
  }
}