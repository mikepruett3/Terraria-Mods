public void UseItem(Player player, int playerID) { 
    Main.NewText("Crazer is spawned!");
	NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Crazer",0);
}


public bool CanUse(Player player,int PlayerID)
	{
	if (!Main.dayTime)
		{
		if(!ModWorld.CrazerKilled)
			{
			return true;
			}
		else
			{
			Main.NewText("Crazer was killed on this world.");
			return false;
			}
		}
	else
		{
		Main.NewText("You can use it only at night.");
		return false;
		}
	}