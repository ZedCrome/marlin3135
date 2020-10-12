int cellSize = 8;
float aliveAtStart = 20;

int[][] cells;
int[][] cellSaveBeforeUpdate;

color dead = color(0, 0, 0);

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
				
			cells[x][y] = int(chanceAlive);
			}
		}
	}


	void update() {
		if(millis() - timeSinceLastUpdate > updateFrequency) {
			savePositions();
			updateLife();
			timeSinceLastUpdate = millis();
		}
	}


	void draw() {
		stroke(20);

		if(reset) {
			reset();
		}

		if(!pause && !reset) {
			for (int x = 0; x < width / cellSize; x++) {
				for (int y = 0; y < height / cellSize; y++) {

					color alive = color(random(155, 220), random(155, 220), 0);
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

	
	void savePositions() {
		for (int x = 0; x < width / cellSize; x++) {
			for (int y = 0; y < height / cellSize; y++) {
				cellSaveBeforeUpdate[x][y] = cells[x][y];
			}
		}
	}


	void updateLife() {
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