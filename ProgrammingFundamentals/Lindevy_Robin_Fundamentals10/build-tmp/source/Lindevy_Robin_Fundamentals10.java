import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class Lindevy_Robin_Fundamentals10 extends PApplet {

GameOfLife game;


public void setup() {
	
	game = new GameOfLife();
}


public void draw() {
	game.draw();
	
}
int cellSize = 8;
float aliveAtStart = 20;

int[][] cells;
int[][] cellSaveBeforeUpdate;

int dead = color(0, 0, 0);

int updateFrequency = 75;
int timeSinceLastUpdate = 0;

class GameOfLife {

	GameOfLife() {
		cells = new int [width / cellSize][height / cellSize];
		cellSaveBeforeUpdate = new int [width / cellSize][height / cellSize];

		for (int x = 0; x < width / cellSize; x++) {
			for (int y = 0; y < height / cellSize; y++) {
				float chanceAlive = random(100);

				if(chanceAlive <= aliveAtStart)
					chanceAlive = 1;
				else
					chanceAlive = 0;
				
			cells[x][y] = PApplet.parseInt(chanceAlive);
			}
		}
	}


	public void update() {
		if(millis() - timeSinceLastUpdate > updateFrequency) {
			savePositions();
			updateLife();
			timeSinceLastUpdate = millis();
		}
	}


	public void draw() {
		stroke(20);

		if(reset) {
			reset();
		}

		if(!pause && !reset) {
			for (int x = 0; x < width / cellSize; x++) {
				for (int y = 0; y < height / cellSize; y++) {

					int alive = color(random(0, 220), random(0, 220), random(0, 220));
					if(cells[x][y] == 1) {
						fill(alive);
						rect(cellSize * x, cellSize * y, cellSize, cellSize);
					}
					else {
						fill(dead);
						rect(cellSize * x, cellSize * y, cellSize, cellSize);
					}
				}	
			}
			update();
		}
	}

	
	public void savePositions() {
		for (int x = 0; x < width / cellSize; x++) {
			for (int y = 0; y < height / cellSize; y++) {
				cellSaveBeforeUpdate[x][y] = cells[x][y];
			}
		}
	}


	public void updateLife() {
		for (int x = 0; x < width / cellSize; x++) {
			for (int y = 0; y < height / cellSize; y++) {
			int neighbours = 0; 

				for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++) {
					for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++) { 

						if ((neighbourX >= 0) && (neighbourX < width / cellSize)) {
							if((neighbourY >= 0) && (neighbourY < height / cellSize)) {

								if (!((neighbourX == x) && (neighbourY == y))) { 
									if (cellSaveBeforeUpdate[neighbourX][neighbourY] == 1){
										neighbours ++; 
									}
								}
							} 
						} 
					} 
				}
				if (cellSaveBeforeUpdate[x][y] == 1) { 
					if (neighbours < 2 || neighbours > 3) {
						cells[x][y] = 0; 
					}
				} 
				else {   
					if (neighbours == 3 ) {
						cells[x][y] = 1; 
					}
				} 
			}
		}
	}
}

public void reset() {
	for (int x = 0; x < width / cellSize; x++) {
		for (int y = 0; y < height / cellSize; y++) {

			float chanceAlive = random(100);

			if(chanceAlive <= aliveAtStart) {
				chanceAlive = 1;
				cells[x][y] = PApplet.parseInt(chanceAlive);
			}
			else{
				chanceAlive = 0;
				cells[x][y] = PApplet.parseInt(chanceAlive);
			}
		}
	}
	reset = false;
}
boolean pause, reset = false;

public void keyPressed() {
	if(key == 'p' || key == 'P') {
		if(pause) {
			pause = false;
		}
		else {
			pause = true;
		}
	}

	if(key == 'r' || key == 'R') {
		reset = true;
	}

}
  public void settings() { 	size(1024, 1024); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "Lindevy_Robin_Fundamentals10" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
