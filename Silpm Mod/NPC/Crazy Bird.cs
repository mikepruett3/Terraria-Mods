public bool SpawnNPC(int x, int y, int playerID)
{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	if (surface && Main.dayTime && ModWorld.CrazerKilled)
		{
		if (Main.rand.Next(2)==1)
			{
			return true;
			} else return false;
		} else return false;
}


public void AI()
	{
	npc.AI(true);
	Color color=new Color();
    int dust = Dust.NewDust(new Vector2((float) npc.position.X-10, (float) npc.position.Y-10), 20, 20, 27, (npc.velocity.X * 0.2f) + (npc.direction * 3), npc.velocity.Y * 0.2f, 100, color, 1.9f);
	Main.dust[dust].noGravity = true;
	}
	
public void NPCLoot(Player player)
	{
	Gore.NewGore(npc.position,npc.velocity,"Crazy Bird Body",1f,-1);
	if (Main.rand.Next(250)==1)
		{
		NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Crazy Wraith",0);
		}
	}