boolean up, down,left, right = false;


  void keyPressed() {
    if (key == 'w') {
      up = true;

    } if (key == 's') {
      down = true;

    } if (key == 'a') {
      left = true;

    } if (key == 'd') {
      right = true;
    }
  }


  void keyReleased() {
    if(key == 'w') {
      up = false;
      direction.y = 0;
    }
    if (key == 's') {
      down = false;
      direction.y = 0;
    }
    if (key == 'a')  {
      left = false;
      direction.x = 0;
    }
    if (key == 'd')  {
      right = false;
      direction.x = 0;
    }
  }