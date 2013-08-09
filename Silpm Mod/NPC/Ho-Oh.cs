// SPRITE FROM POKEMON BW

public static bool SpawnNPC(int x, int y, int playerID)	
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	
	if (ModWorld.CrazerKilled && Main.player[playerID].townNPCs <= 0f)
		{
		if (surface && Main.rand.Next(20)==1) return true;
		if (Main.player[Main.myPlayer].zoneJungle && Main.rand.Next(16)==1) return true;
		if (Main.player[Main.myPlayer].zoneHoly && Main.rand.Next(8)==1) return true;
		return false;
		}
	return false;
	}
	
public void NPCLoot()
	{
    Gore.NewGore(npc.position,npc.velocity,"Ho-Oh Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Ho-Oh Wing",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Ho-Oh Leg",1f,-1);
	}

public void AI()
	{
	npc.AI(true);
	npc.TargetClosest(true);
	//projectiles
	if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
		{
		if (Main.rand.Next(130)==1)
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
				int damage = 10;
				int type = Config.projectileID["Ho-Oh Attack"];
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
				Main.projectile[num54].aiStyle=1;
                Main.PlaySound(2, (int) npc.position.X, (int) npc.position.Y, 0x11);
				}
            npc.netUpdate=true;
            }
		}
	//eggs
	if (Main.rand.Next(300)==1)
		{
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, 16, 25, Config.itemDefs.byName["Ho-Oh Egg"].type, 1, false, 0);
		}
	
	
	}