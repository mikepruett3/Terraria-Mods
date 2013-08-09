public bool SpawnNPC(int x, int y, int playerID)
{
	return false;
}

public void NPCLoot()
{
	ModWorld.CrazerKilled=true;
	Main.NewText("Crazy monsters are comming to your world!");
	NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+30,(int)npc.position.Y,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+30,(int)npc.position.Y+30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+30,(int)npc.position.Y-30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-30,(int)npc.position.Y,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-30,(int)npc.position.Y+30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-30,(int)npc.position.Y-30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X,(int)npc.position.Y+30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X,(int)npc.position.Y-30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+40,(int)npc.position.Y,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+40,(int)npc.position.Y+30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X+40,(int)npc.position.Y-30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-40,(int)npc.position.Y,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-40,(int)npc.position.Y+30,"Crazy Bird",0);
	NPC.NewNPC((int)npc.position.X-40,(int)npc.position.Y-30,"Crazy Bird",0);
}