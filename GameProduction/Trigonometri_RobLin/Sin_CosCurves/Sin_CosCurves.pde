//Sin & Cos Waves
int amountOfDots = 101;
int spaceBetweenDots = width/100;
int amplifyer = 30;
int xPos;
float t = 0;
float r;
float g;
float b;
float offset = 0.5;

//Circle
int numberOfObjects = 20;
float angle = 0;
float slice = PI * 2/ numberOfObjects;
float rotationSpeed = 0.01f;

float smallCircleRadiusMod = 2.5;
float middleCircleRadiusMod = 5;
float bigCircleRadiusMod = 6;

//For everyone
float midX;
float midY;

void setup() {
	size(800, 400);

	midX = width / 2;
	midY = height / 2;
	strokeWeight(10);
	frameRate(30);
}

void draw() {
	background(0);

	circle();
	smallerCircles();
	spiral();
	sinWave();
	cosWave();

	rotationSpeed += 0.01f;
}


void sinWave() {
	for(int i = 0; i < amountOfDots; i++) {
		g = random(0, abs(midX - xPos));
		b = random(0, abs(midX - xPos));
		xPos = i * 10;

		stroke(0 , g, b);
		point(xPos, (height / 8) + sin(t) * amplifyer);
		t++;
	}
}


void cosWave() {
	for(int i = 0; i < amountOfDots; i++) {
		g = random(0, abs(midX - xPos));
		b = random(0, abs(midX - xPos));
		xPos = i * 10;

		stroke(0, g ,b);
		point(xPos, (height /1.125) + cos(t) * amplifyer);
		t++;
	}
}


void circle() {
	strokeWeight(10);
	float radius = (numberOfObjects * middleCircleRadiusMod);

	for(int i = 0; i < numberOfObjects; i++) {

		angle = i * slice + rotationSpeed;
		float x = midX + cos(angle) * radius;
		float y = midY + sin(angle) * radius;

		stroke(255);
		point(x, y);
	}
}


void smallerCircles() {
	strokeWeight(5);
	
	for(int i = 0; i < numberOfObjects; i++) {

		float radius = numberOfObjects * smallCircleRadiusMod;
		angle = i * slice + rotationSpeed;

		float x = midX / 2 + sin(angle) * radius;
		float y = midY + cos(angle) * radius;

		point(x, y);
		point(x + midX, y);

		radius = numberOfObjects * bigCircleRadiusMod;
		x = midX + sin(angle) * radius;
		y = midY + cos(angle) * radius;

		point(x,y);
	}
}


void spiral() {
	int radius = 0;
	stroke(255);

	for(int i = 0; i < numberOfObjects + 1; i++) {

		angle = i * slice - rotationSpeed;
		float x = midX + cos(angle) * radius;
		float y = midY + sin(angle) * radius;

		point(x, y);
		radius += 5;
	}
}
