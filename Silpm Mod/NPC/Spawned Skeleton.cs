public bool SpawnNPC(int x, int y, int playerID)
	{
	return false;
	}
	
	
	
public void AI()
	{
	npc.TargetClosest(true);
	npc.direction=Main.player[npc.target].direction;
	if (Main.player[npc.target].direction == -1 )
		{
		if (npc.velocity.X > -2) npc.velocity.X -= 0.22f;
		}
	if (Main.player[npc.target].direction == 1 )
		{
		if (npc.velocity.X < 2) npc.velocity.X += 0.22f;
		}
	if (Main.player[npc.target].controlUp && npc.velocity.Y==0)
		{
		npc.velocity.Y-=9;
		}
	for(int i=0; i<Main.npc.Length; i++)
		{
		float difX = ((Main.npc[i].position.X) - this.npc.position.X);
		float difY = ((Main.npc[i].position.Y) - this.npc.position.Y);
		if( (difX < 30f) && (difX > -30f) && (difY < 30f) && (difY > -30f) )
			{
			if ( Main.npc[i].type!=this.npc.type && Main.npc[i].townNPC == false)
				{
				//if(npc.justHit)
					//{
					Main.npc[i].StrikeNPC(30,5, this.npc.direction);
					//}
				}
			}
		}
	}