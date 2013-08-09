public bool SpawnNPC(int x, int y, int playerID)
	{
	return false;
	}
	
public void AI()
	{
	npc.ai[0]++;
	if (npc.ai[0]>3000)
		{
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Ho-Oh",0);
		NPC.NewNPC((int)npc.position.X+10,(int)npc.position.Y,"Ho-Oh",50);
		NPC.NewNPC((int)npc.position.X-10,(int)npc.position.Y,"Ho-Oh",100);
		npc.position.X=0;
		npc.StrikeNPC(1000,0,0);
		}
	
	
	}