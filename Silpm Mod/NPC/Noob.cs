public bool SpawnNPC(int x, int y, int playerID) 
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer);
	if ((surface || underground) && ModWorld.CrazerKilled && Main.player[playerID].townNPCs <= 0f)
		{
		if (Main.rand.Next(14)==1)
			{
			return true;
			} else return false;
		} else
	return false;
	}




public void NPCLoot()
{
    Gore.NewGore(npc.position,npc.velocity,"Noob Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Noob Body",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Noob Arm",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Noob Arm",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Noob Leg",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Noob Leg",1f,-1);
}

