class Player {
  float acceleration = 0f;
  float speed = 1f;
  int ballSize = 40;
  PVector position;
  PVector velocity;


  Player(float x, float y) {
    velocity = new PVector(0, 0);
    position = new PVector(x, y);
    direction = new PVector(0, 0);
  }


  void draw() {
    fill(255, 89, 120);
    ellipse(position.x, position.y, ballSize, ballSize);
    direction();
    move();
    deaccelerate();
    edgeDetection();
    position.add(velocity.limit(10));
  }


  //Checks what direction the player should be voving in, based on what buttons are pressed.
  void direction() {
    if (up) {
      direction.y = -speed;
    }
    if (down) {
      direction.y = speed;
    }
    if (left) {
      direction.x = -speed;
    }
    if (right) {
      direction.x = speed;
    }
  } 


//calculates velocity, direction, acceleration and combines it into the velocity vector.
  void move() {
    if (up || down || left || right) {
      direction.normalize();
      direction.mult(acceleration * deltaTime);

      velocity.add(direction.mult(deltaTime));
      if(acceleration < 2000) {
        acceleration += 400;
      }
    }
  }


  //deaccelerates player while no input key is pressed.
  void deaccelerate() {
    if(!up && !down && !left && !right) {
      velocity.x *= 0.8;
      velocity.y *= 0.8;
      acceleration = 0;
    }
  }


  //Keeps the player inside of the viewing window
  void edgeDetection() {
    if (position.x < 0) {
      position.x = width;
    } else if (position.x > width) {
      position.x = 0;
    } else if (position.y < ballSize/2) {
      position.y = ballSize/2;
    } else if (position.y > (height - ballSize/2)) {
      position.y = (height - ballSize/2);
    }
  }

}
