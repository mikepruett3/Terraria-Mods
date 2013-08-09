public void UseStyle(Player player) {
	Vector2 vector = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
	float num27 = ModWorld.playerCursor[player.whoAmi].X - vector.X;
	float num28 = ModWorld.playerCursor[player.whoAmi].Y - vector.Y;
	
	if (ModWorld.playerCursor[player.whoAmi].X > player.position.X)
		player.direction = 1;
	else
		player.direction = -1;
	
	float ang = (float)Math.Atan2((double)(num28 * (float)player.direction), (double)(num27 * (float)player.direction));
	
	float aOffset = ang + ((player.direction==-1)?3.14f:0.0f);
	float xOffset = (float)Math.Cos(aOffset)*-15.0f;
	float yOffset = (float)Math.Sin(aOffset)*-10.0f;
	
	player.itemLocation.X = player.position.X + (float)player.width * 0.5f - (float)Main.itemTexture[player.inventory[player.selectedItem].type].Width * 0.5f + xOffset;// - (float)(player.direction * 10.0f);
	player.itemLocation.Y = player.position.Y + (float)player.height * 0.5f - (float)Main.itemTexture[player.inventory[player.selectedItem].type].Height * 0.5f + yOffset;
	player.itemRotation = ang;

}