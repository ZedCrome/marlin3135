
void reset() {
	for (int x = 0; x < width / cellSize; x++) {
		for (int y = 0; y < height / cellSize; y++) {

			float chanceAlive = random(100);

			if(chanceAlive <= aliveAtStart) {
				chanceAlive = 1;
				cells[x][y] = int(chanceAlive);
			}
			else{
				chanceAlive = 0;
				cells[x][y] = int(chanceAlive);
			}
		}
	}
	reset = false;
}