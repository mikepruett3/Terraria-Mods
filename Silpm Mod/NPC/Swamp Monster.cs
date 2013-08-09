public static bool SpawnNPC(int x, int y, int playerID)
	{
	if ( Main.player[Main.myPlayer].zoneJungle && ModWorld.CrazerKilled
		&& Main.rand.Next(14)==1 )
		{
		return true;
		} else
		{
		return false;
		}
	return false;
	}