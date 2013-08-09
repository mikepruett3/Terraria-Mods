public bool SpawnNPC(int x, int y, int playerID)
	{
	return false;
	}
	
	
public void NPCLoot()
	{
    Gore.NewGore(npc.position,npc.velocity,"Big Bird Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Big Bird Wing",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Big Bird Wing",1f,-1);
	}
	
public void AI()
	{
	npc.AI(true);
	if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
		{
		npc.TargetClosest(true);
		}
	//moving to
	float npcposX = 1f;
	float npcposY = 1f;
	float birdspeed = 1f;
	if (npc.ai[0]==0) // left up
		{
		npcposX = Main.player[npc.target].position.X - 450;
		npcposY = Main.player[npc.target].position.Y - 300;
		birdspeed = 0.7f;
		}
	if (npc.ai[0]==1) // right up
		{
		npcposX = Main.player[npc.target].position.X + 250;
		npcposY = Main.player[npc.target].position.Y - 300;
		birdspeed = 0.7f;
		}
	if (npc.ai[0]==2) // attack player
		{
		npcposX = Main.player[npc.target].position.X-20;
		npcposY = Main.player[npc.target].position.Y-20;
		birdspeed = 2.1f;
		}
	
	if (npc.position.X < npcposX)
		{
		if (npc.velocity.X < 6 ) npc.velocity.X += birdspeed;
		}
	if (npc.position.X > npcposX)
		{
		if (npc.velocity.X > -6 ) npc.velocity.X -= birdspeed;
		}
	if (npc.position.Y < npcposY)
		{
		if (npc.velocity.Y < 6 ) npc.velocity.Y += birdspeed;
		}
	if (npc.position.Y > npcposY)
		{
		if (npc.velocity.Y > -6 ) npc.velocity.Y -= birdspeed;
		}
	if ( (npc.position.X > npcposX - 25)
		&& (npc.position.X < npcposX + 25) )npc.velocity.X = 0;
	if ( (npc.position.Y > npcposY - 25)
		&& (npc.position.Y < npcposY + 25) )npc.velocity.Y = 0;
	
	//change position
	if (Main.rand.Next(500)==1)
		{
		if (npc.ai[0]==0) npc.ai[0]=1; else if (npc.ai[0]==1) npc.ai[0]=0;
		}
	//attack player
	if (Main.rand.Next(500)==1)
		{
		npc.ai[0]=2;
		}
	if (npc.ai[0]==2)
		{
		npc.ai[1]++;
		if (npc.ai[1] > 75)
			{
			npc.ai[1]=0;
			if (Main.rand.Next(2)==1)
				{
				npc.ai[0]=1;
				} else
				{
				npc.ai[0]=0;
				}
			}
		}
	//shoot
	if (npc.ai[0]<2)
		{
		if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
			{
			if (Main.rand.Next(60)==1)
				{
				float num48 = 8f;
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height / 2));
				float speedX = ((Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)) - vector8.X) + Main.rand.Next(-20, 0x15);
				float speedY = ((Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)) - vector8.Y) + Main.rand.Next(-20, 0x15);
				if (((speedX < 0f) && (npc.velocity.X < 0f)) || ((speedX > 0f) && (npc.velocity.X > 0f)))
					{
					float num51 = (float) Math.Sqrt((double) ((speedX * speedX) + (speedY * speedY)));
					num51 = num48 / num51;
					speedX *= num51;
					speedY *= num51;
					int damage = 25;
					int type = Config.projectileID["Big Bird Attack"];
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
					Main.projectile[num54].aiStyle=1;
					Main.PlaySound(2, (int) npc.position.X, (int) npc.position.Y, 32);
					}
				}
			}
		}
	//eggs
	if (npc.ai[0]<2)
		{
		if (Main.rand.Next(600)==1)
			{
			NPC.NewNPC((int)npc.position.X+70,(int)npc.position.Y+100,"Big Bird Egg",0);
			}
		}
	//
	}