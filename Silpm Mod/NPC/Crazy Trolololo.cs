public bool SpawnNPC(int x, int y, int playerID)
{
	return false;
}

public void AI()
{
	//moving
	npc.TargetClosest(true);
	if (Main.player[npc.target].position.X < npc.position.X)
	{
		if (npc.velocity.X > -2) npc.velocity.X -= 0.22f;
	}
	if (Main.player[npc.target].position.X > npc.position.X)
	{
		if (npc.velocity.X < 2 ) npc.velocity.X += 0.22f;
	}

	npc.ai[2]++;
	if (npc.ai[2] >= 100)
	{
		npc.velocity.Y = -10;
		npc.ai[2]=0;
	}
	
	
	npc.ai[0]++;
	if (npc.ai[0] >= 300)
	{
		if (ModWorld.SmallTroll < 99){
		NPC.NewNPC((int)npc.position.X+120,(int)npc.position.Y-500,"Trololo",0);
		NPC.NewNPC((int)npc.position.X-120,(int)npc.position.Y-500,"Trololo",0);
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y-500,"Trololo",0);
		ModWorld.SmallTroll+=3;
		}
		npc.ai[0]=0;
	}
	
	
	
}


public void NPCLoot()
{
    ModWorld.SmallTroll=0;
	ModWorld.CrazyTrolololoFight=false;
	Main.musicVolume=ModWorld.qcurVolume;
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
}