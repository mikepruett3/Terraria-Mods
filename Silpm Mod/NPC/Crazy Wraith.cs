public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool underworld= (y > Main.maxTilesY-190);
	bool undergroundHoly = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneHoly;
	
	if (undergroundHoly && ModWorld.CrazerKilled && Main.rand.Next(18)==1)
		{
		return true;
		} else
		{
		return false;
		}
	return false;
	}
	