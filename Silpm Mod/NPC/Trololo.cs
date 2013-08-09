
public static bool SpawnNPC(int x, int y, int playerID) {
	return false;
}




public void NPCLoot()
{
	ModWorld.SmallTroll--;
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
}

public void AI()
{
	npc.TargetClosest(true);
	if (Main.player[npc.target].position.X < npc.position.X)
	{
		if (npc.velocity.X > -8) npc.velocity.X -= 0.22f;
	}
	if (Main.player[npc.target].position.X > npc.position.X)
	{
		if (npc.velocity.X < 8 ) npc.velocity.X += 0.22f;
	}
	
	npc.ai[2]++;
	if (npc.ai[2] >= 100)
	{
		npc.velocity.Y = -10;
		npc.ai[2]=0;
	}
}