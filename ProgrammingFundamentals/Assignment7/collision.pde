//Check collision, 2 circles objects
//function will return true (collision) or false (no collision)


  boolean roundCollision(Character one, Character two) {
    float maxDistance = one.characterSize/2 + two.characterSize/2;

    //first a quick check to see if we are too far away in x or y direction
    //if we are far away we don't collide so just return false and be done.
    if(abs(one.position.x - two.position.x) > maxDistance ||  
     abs(one.position.y - two.position.y) > maxDistance)
    {
      return false;
    }
    //we then run the slower distance calculation
    //dist uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
    else if(dist(one.position.x, one.position.y, two.position.x, two.position.y) > maxDistance)
    {
      return false;
    }
    else
    {
     return true;
    }
  }

