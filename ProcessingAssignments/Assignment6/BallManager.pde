Ball myBall;
Ball[] balls;
Collision collision;
int numberOfBalls = 10;


class BallManager {
	float spawnTimer;


	void setup() {
		balls = new Ball[numberOfBalls];
		collision = new Collision();

		//create i amount of ball objects.
		for(int i = 0; i < balls.length; i++) {
			balls[i] = (myBall = new Ball(random(15, width-15), random(15, height-15)));
		}

	}


	void draw() {
		//check if balls collide with player. 
		for(int i = 0; i < balls.length; i++) {
			if(collision.roundCollision(int(myPlayer.position.x), int(myPlayer.position.y), myPlayer.ballSize/2, int(balls[i].position.x), int(balls[i].position.y), balls[i].ballSize/2)) {
    			gameRunning = false; //state of the game.
    		}
    		balls[i].update();
			balls[i].draw();
		}

		
	}
}