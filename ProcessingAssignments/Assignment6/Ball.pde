class Ball {
  PVector position;
  PVector velocity;
  int ballSize = 30;


  public Ball (float x, float y) {
    position = new PVector(x, y);
    velocity = new PVector();

    //random staring position inside of view area. 
    position.x = random(ballSize/2, width - ballSize/2);
    position.y = random(ballSize/2, height - ballSize/2);

    //random velocity.
    velocity.x = random(10) - 5;
    velocity.y = random(10) - 5;
  }


  void update() {
    //updates ball positions.
    position.x += velocity.x;
    position.y += velocity.y;

    edgeDetection();
  }
  

	//draws the ball.
  void draw() {
    fill(125, 200, 255);
    ellipse(position.x, position.y, ballSize, ballSize);

  }


  //Keeps the ball inside of the viewing window
  void edgeDetection() {
    if (position.x > (width - ballSize/2) || position.x < ballSize/2) {
      velocity.x *= -1;
    }else if (position.y > (height - ballSize/2) || position.y < ballSize/2) {
      velocity.y *= -1;
    }
  }
}

