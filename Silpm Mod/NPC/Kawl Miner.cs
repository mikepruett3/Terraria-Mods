public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
	
	if (underground && ModWorld.CrazerKilled && Main.rand.Next(5)==1)
		{
		return true;
		} else
		{
		return false;
		}
	return false;
	}
public void NPCLoot()
	{
	Gore.NewGore(npc.position,npc.velocity,"Kawl Miner Head",1f,-1);
	}