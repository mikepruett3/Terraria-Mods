public void UseItem(Player player, int playerID) { 
	if (Main.dayTime == true)
		{
		Main.dayTime=false;
		} else
	if (Main.dayTime == false)
		{
		Main.dayTime=true;
		}
	//NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Big Bird",0);
	// I using it for testing npc :P
}