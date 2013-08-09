public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
	
	if (underground && ModWorld.CrazerKilled && Main.rand.Next(7)==1)
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
	Gore.NewGore(npc.position,npc.velocity,"Kawl Warrior Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Kawl Warrior Hand",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Kawl Warrior Leg",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Kawl Warrior Sword",1f,-1);
	}