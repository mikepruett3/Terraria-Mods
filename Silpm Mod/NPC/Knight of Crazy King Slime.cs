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
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Crazy Slime",0);
		}
}

public void NPCLoot()
{
	ModWorld.Knights-=1;
	if (ModWorld.Knights==1)
		{
		Main.NewText("Knights defeated: 1/2");
		}
	if (ModWorld.Knights==0)
		{
		Main.NewText("Knights defeated: 2/2");
		}
}