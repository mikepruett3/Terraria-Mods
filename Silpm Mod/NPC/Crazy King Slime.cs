public bool SpawnNPC(int x, int y, int playerID)
{
	return false;
}

public void AI()
{
	npc.AI(true);
	npc.TargetClosest(true);
	if (npc.ai[3]==0)
		{
		npc.ai[2]=Main.rand.Next(250) + 200;
		npc.ai[3]=1;
		}
	npc.ai[0]++;
	if (npc.ai[0]==npc.ai[2])
		{
		npc.ai[3]=0;
		npc.ai[0]=0;
		npc.velocity.Y=-8;
		if (Main.player[npc.target].position.X < npc.position.X)
			{
			npc.velocity.X = -18;
			}
		if (Main.player[npc.target].position.X > npc.position.X)
			{
			npc.velocity.X = 18;
			}
		}
	//spawning crazy slimes
	if(npc.justHit)
		{
		NPC.NewNPC((int)npc.position.X,(int)npc.position.Y,"Crazy Slime",0);
		}
	
	
	/*STAGES -> npc.ai[1]
	npc.ai[1]=	0 - 10k to 8k
				1 - Knights spawned
				2 - Knights killed, 8k - 5k
				3 - Flying slime mother spawned
				4 - Flying slime mother killed, 5k-1k
				5 - Flying slime mother & knights spawned
				6 - Flying slime mother & knights killed 1k-0*/
	//8k hp
	if ((npc.life<=8000) && (npc.ai[1]==0))
		{
		Main.NewText("Crazy King Slime spawned two knights!");
		npc.ai[1]=1;
		Main.PlaySound(4,-1,-1,1);
		ModWorld.Knights=2;
		NPC.NewNPC((int)npc.position.X+100,(int)npc.position.Y-100,"Knight of Crazy King Slime",0);
		NPC.NewNPC((int)npc.position.X-100,(int)npc.position.Y,"Knight of Crazy King Slime",0);
		npc.dontTakeDamage=true;
		}
	if ((ModWorld.Knights==0) && (npc.ai[1]==1))
		{
		Main.NewText("Knights defeated!");
		npc.dontTakeDamage=false;
		npc.ai[1]=2;
		}
	if ((npc.life<=5000) && (npc.ai[1]==2))
		{
		Main.NewText("Crazy King Slime spawned flying slime mother!");
		npc.ai[1]=3;
		Main.PlaySound(4,-1,-1,1);
		ModWorld.SlimeMother=true;
		NPC.NewNPC((int)npc.position.X+100,(int)npc.position.Y-100,"Flying Slime Mother",0);
		npc.dontTakeDamage=true;
		}
	if ((ModWorld.SlimeMother==false)&& (npc.ai[1]==3))
		{
		npc.dontTakeDamage=false;
		npc.ai[1]=4;
		}
	if ((npc.life<=1000) && (npc.ai[1]==4))
		{
		Main.NewText("Crazy King Slime spawned... Everything he have!");
		npc.ai[1]=5;
		Main.PlaySound(4,-1,-1,1);
		ModWorld.SlimeMother=true;
		NPC.NewNPC((int)npc.position.X+100,(int)npc.position.Y-100,"Flying Slime Mother",0);
		ModWorld.Knights=2;
		NPC.NewNPC((int)npc.position.X+100,(int)npc.position.Y-100,"Knight of Crazy King Slime",0);
		NPC.NewNPC((int)npc.position.X-100,(int)npc.position.Y,"Knight of Crazy King Slime",0);
		npc.dontTakeDamage=true;
		}
	if ((ModWorld.SlimeMother==false) && (ModWorld.Knights==0) && (npc.ai[1]==5))
		{
		Main.NewText("Ohh... Just finish him!");
		npc.dontTakeDamage=false;
		npc.ai[1]=6;
		}
}


public void NPCLoot()
{

}