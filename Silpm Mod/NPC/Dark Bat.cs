public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
	bool underworld= (y > Main.maxTilesY-190); 
	bool cavern = nospecialbiome && !sky && !surface && !underground && !underworld && (y <= Main.rockLayer *25) && !Main.player[Main.myPlayer].zoneJungle; 
	if (cavern && ModWorld.CrazerKilled && Main.rand.Next(7)==1)
		{
		return true;
		} else
		{
		return false;
		}
	}
public void NPCLoot()
	{
	Gore.NewGore(npc.position,npc.velocity,"Dark Bat Body",1f,-1);
	}
