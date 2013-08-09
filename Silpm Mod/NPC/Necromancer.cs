public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
	bool underworld= (y > Main.maxTilesY-190); 
	bool cavern = nospecialbiome && !sky && !surface && !underground && !underworld && (y <= Main.rockLayer *25) && !Main.player[Main.myPlayer].zoneJungle;
	bool undergroundEvil = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneEvil;
	if (((cavern) || (undergroundEvil)) && ModWorld.CrazerKilled && Main.rand.Next(30)==1)
		{
		return true;
		} else
		{
		return false;
		}
	return false;
	}
	
public void AI()
	{
	npc.AI(true);
	npc.TargetClosest(true);
	//npc.ai[0]++;
	if (Main.rand.Next(90)==0)
		{
		//npc.ai[0]=0;
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,21,0);
		Main.PlaySound(2,(int)npc.position.X,(int)npc.position.Y,21);
		}
	}