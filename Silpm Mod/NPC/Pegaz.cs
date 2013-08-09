public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	if (sky && ModWorld.CrazerKilled && Main.rand.Next(14)==1)
		{
		return true;
		}
	if (Main.player[Main.myPlayer].zoneHoly && ModWorld.CrazerKilled && Main.rand.Next(20)==1)
		{
		return true;
		}
	return false;
	}
	
public void NPCLoot()
	{
    Gore.NewGore(npc.position,npc.velocity,"Pegaz Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Pegaz Body",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Pegaz Ass",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Pegaz Wings",1f,-1);
	}
	
