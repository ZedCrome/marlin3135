boolean pause, reset = false;

void keyPressed() {
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