public static bool SpawnNPC(int x, int y, int playerID)
	{
	bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
	bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
	bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
	bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
	bool underworld= (y > Main.maxTilesY-190); 
	bool cavern = nospecialbiome && !sky && !surface && !underground && !underworld && (y <= Main.rockLayer *25) && !Main.player[Main.myPlayer].zoneJungle;
	bool undergroundEvil = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneEvil;
	bool undergroundHoly = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneHoly;
	if (((cavern) || (undergroundEvil) || (undergroundHoly)) && ModWorld.CrazerKilled && Main.rand.Next(10)==1)
		{
		return true;
		} else
		{
		return false;
		}
	return false;
	}
public void NPCLoot()
	{
	Gore.NewGore(npc.position,npc.velocity,"Kawl Soldier Head",1f,-1);
	}
	
public void AI()
	{
	npc.AI(true);
	npc.TargetClosest(true);
	if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
		{
		if (Main.rand.Next(75)==1)
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
				int damage = 35;
				int type = 81;
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
				Main.projectile[num54].aiStyle=1;
                Main.PlaySound(2, (int) npc.position.X, (int) npc.position.Y, 0x11);
				}
            npc.netUpdate=true;
            }
		}
	}
