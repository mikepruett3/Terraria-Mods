public bool SpawnNPC(int x, int y, int playerID)
{
	return false;
}

public void AI()
{
	npc.AI(true);
	npc.TargetClosest(true);
	if(npc.justHit)
		{
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Flying Crazy Slime",0);
		}
	npc.ai[0]++;
	if(npc.ai[0]==300)
		{
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Crazy Slime",0);
		npc.ai[0]=0;
		}
}

public void NPCLoot()
{
	ModWorld.SlimeMother=false;
	Main.NewText("Flying slime mother defeated!");
}