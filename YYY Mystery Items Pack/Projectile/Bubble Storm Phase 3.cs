public int randNum = 0;
public float rotator = 0f;
public Vector2 Origin;
public float DistanceBonus = 0f;
public void Initialize()
{
    projectile.velocity = new Vector2(0f,-100f);
    projectile.timeLeft = 120;
    this.rotator = (float)(Main.rand.Next(-3,4)/300f);
    projectile.velocity = RotateAboutOrigin(projectile.velocity,Vector2.Zero,this.rotator*-20f);
    this.Origin = new Vector2(projectile.position.X,projectile.position.Y);
    this.DistanceBonus = 0f;
}

public void AI()
{
    projectile.velocity = new Vector2(0,-Vector2.Distance(projectile.velocity,Vector2.Zero));
    this.DistanceBonus += 1f;
    this.Origin = Main.player[projectile.owner].position+new Vector2(Main.player[projectile.owner].width/2,0);
    this.Origin-=projectile.velocity;
    projectile.position = this.Origin;
    projectile.rotation += 0.05f;
}

public bool PreDraw(SpriteBatch sp)
{
  //  Texture2D mytex=Main.goreTexture[Config.goreID["Beam Source Void"]];

    Texture2D mytex=Main.goreTexture[Config.goreID["Bubble Storm Phase 3"]];
    float chainScalar = 1f;
    int chainTransper = 0;
    int FrameNum=4;
    if(this.randNum == -1 || (int)(Main.time) % 4 == 0)
        this.randNum++;
    if(this.randNum > FrameNum) this.randNum = 1;
    int randPart = this.randNum-1;
                    Vector2 FakeVelocity = new Vector2(projectile.velocity.X,projectile.velocity.Y-this.DistanceBonus);
                    FakeVelocity = RotateAboutOrigin(FakeVelocity,Vector2.Zero,projectile.rotation);
					Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num5 = FakeVelocity.X - vector2.X;
					float num6 = FakeVelocity.Y - vector2.Y;
		//			float rotation2 = (float)Math.Atan2((double)num6, (double)num5) - 1.57f;
                    float rotation2 =  -(float)Math.Atan2(FakeVelocity.X, FakeVelocity.Y);
                    int segmants = 13;
					for(int zzz = 0; zzz < segmants; zzz++)
                    {
                       // chainScalar -= 0f;
                        chainTransper+= 5;
                        randPart++;
                        if(randPart % FrameNum == 0)
                            randPart = 0;
#region laser light
                            float red  = 0.3f;
                            float green  = 0.2f;
                            float blue  = 0.2f;
                            Lighting.addLight((int)((vector2.X + (float)(projectile.width / 2)) / 16f), (int)((vector2.Y + (float)(projectile.height / 2)) / 16f), red, green, blue);
#endregion
#region laserdust

            if (Main.rand.Next(segmants*4) == 0) //needs to fix
            {
                Color newColor;
                Vector2 dustpos = projectile.position;
                int projwidth = projectile.width;
                int projheight = projectile.height;
                int dusttype = 63; // this controls a dust type
                float randspeedx = 0f;
                float randspeedy = 0f;
                int dustalpha = 150;
                float dustscale = 3f;
                newColor = default(Color);
                int zz = Dust.NewDust(vector2-new Vector2(projectile.width/2f,projectile.height/2f), projectile.width, projectile.height, dusttype, randspeedx, randspeedy, dustalpha, newColor, dustscale);
                Main.dust[zz].noGravity = true;
            }
#endregion
                            vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                            vector2 -= FakeVelocity;
                            num5 = FakeVelocity.X - vector2.X;
                            num6 = FakeVelocity.Y - vector2.Y;
                           // rotation2 = (float)Math.Atan2((double)num6, (double)num5) - 1.57f;
                            rotation2 = -(float)Math.Atan2(FakeVelocity.X, FakeVelocity.Y);
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
                            #region damaging


Rectangle rectangle = new Rectangle((int)vector2.X, (int)vector2.Y, projectile.width, projectile.height);
#region damage time - damage players as enemy
if (Main.netMode != 2 && projectile.hostile && Main.myPlayer < 255 && projectile.damage > 0)
    {
				int myPlayer2 = Main.myPlayer;
				if (Main.player[myPlayer2].active && !Main.player[myPlayer2].dead && !Main.player[myPlayer2].immune)
				{
					Rectangle value6 = new Rectangle((int)Main.player[myPlayer2].position.X, (int)Main.player[myPlayer2].position.Y, Main.player[myPlayer2].width, Main.player[myPlayer2].height);
					if (rectangle.Intersects(value6))
					{
						int hitDirection = projectile.direction;
						if (Main.player[myPlayer2].position.X + (float)(Main.player[myPlayer2].width / 2) < vector2.X + (float)(projectile.width / 2))
						{
							hitDirection = -1;
						}
						else
						{
							hitDirection = 1;
						}
						int num9 = Main.DamageVar((float)projectile.damage);
						if (!Main.player[myPlayer2].immune)
						{
							projectile.StatusPlayer(myPlayer2);
						}
						Main.player[myPlayer2].Hurt(num9 * 2, hitDirection, false, false, "Was Incenirated", false);
					}
				}
    }
#endregion
#region damage time - damage enemies and pvp players
if (projectile.owner == Main.myPlayer && projectile.friendly)
			{
				if (projectile.damage > 0)
				{
					for (int k = 0; k < 200; k++)
                    {
                        #region projectile
                        if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && (((!Main.npc[k].friendly || (Main.npc[k].type == 22 && projectile.owner < 255 && Main.player[projectile.owner].killGuide)) && projectile.friendly) || (Main.npc[k].friendly && projectile.hostile)) && (projectile.owner < 0 || Main.npc[k].immune[projectile.owner] == 0))
                        {
							bool flag = false;
							if (!flag && ((Main.npc[k].noTileCollide || !projectile.ownerHitCheck || Collision.CanHit(Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height))))
							{
								Rectangle value2 = new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height);
								if (rectangle.Intersects(value2))
                                {
									bool flag4 = false;
									if (projectile.melee && Main.rand.Next(1, 101) <= Main.player[projectile.owner].meleeCrit)
									{
										flag4 = true;
									}
									if (projectile.ranged && Main.rand.Next(1, 101) <= Main.player[projectile.owner].rangedCrit)
									{
										flag4 = true;
									}
									if (projectile.magic && Main.rand.Next(1, 101) <= Main.player[projectile.owner].magicCrit)
									{
										flag4 = true;
									}
									int num71 = Main.DamageVar((float)projectile.damage);
									projectile.StatusNPC(k);
									Main.npc[k].StrikeNPC(num71, projectile.knockBack, projectile.direction, flag4, false);
									if (Main.netMode != 0)
									{
										if (flag4)
										{
											NetMessage.SendData(28, -1, -1, "", k, (float)num71, projectile.knockBack, (float)projectile.direction, 1);
										}
										else
										{
											NetMessage.SendData(28, -1, -1, "", k, (float)num71, projectile.knockBack, (float)projectile.direction, 0);
										}
									}
									if (projectile.penetrate != 1)
									{
										Main.npc[k].immune[projectile.owner] = 10;
									}
									if (projectile.penetrate > 0)
									{
										projectile.penetrate--;
										if (projectile.penetrate == 0)
										{
											break;
										}
									}
								}
							}
                        }
                        #endregion
                    }
                }
                #region if the player participates in pvp
                if (projectile.damage > 0 && Main.player[Main.myPlayer].hostile)
				{
					for (int l = 0; l < 255; l++)
					{
						if (l != projectile.owner && Main.player[l].active && !Main.player[l].dead && !Main.player[l].immune && Main.player[l].hostile && projectile.playerImmune[l] <= 0 && (Main.player[Main.myPlayer].team == 0 || Main.player[Main.myPlayer].team != Main.player[l].team) && (!projectile.ownerHitCheck || Collision.CanHit(Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height, Main.player[l].position, Main.player[l].width, Main.player[l].height)))
						{
							Rectangle value3 = new Rectangle((int)Main.player[l].position.X, (int)Main.player[l].position.Y, Main.player[l].width, Main.player[l].height);
							if (rectangle.Intersects(value3))
							{
								
								bool flag3 = false;
								if (projectile.melee && Main.rand.Next(1, 101) <= Main.player[projectile.owner].meleeCrit)
								{
									flag3 = true;
								}
								int num8 = Main.DamageVar((float)projectile.damage);
								if (!Main.player[l].immune)
								{
									projectile.StatusPvP(l);
								}
								Main.player[l].Hurt(num8, projectile.direction, true, false, "Death", flag3);
								if (Main.netMode != 0)
								{
									if (flag3)
									{
										NetMessage.SendData(26, -1, -1, "Death", l, (float)projectile.direction, (float)num8, 1f, 1);
									}
									else
									{
										NetMessage.SendData(26, -1, -1, "Death", l, (float)projectile.direction, (float)num8, 1f, 0);
									}
								}
								projectile.playerImmune[l] = 40;
								if (projectile.penetrate > 0)
								{
									projectile.penetrate--;
									if (projectile.penetrate == 0)
									{
										break;
									}
								}
							}
						}
					}
                }
                #endregion
            }
#endregion
#endregion
							sp.Draw(
                            mytex, 
                            new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), 
                            new Rectangle?(new Rectangle(0, (((mytex.Height-2*(FrameNum-1))/FrameNum)+2)*randPart, mytex.Width, (mytex.Height-2*(FrameNum-1))/FrameNum)),
                            color2, 
                            rotation2, 
                            new Vector2((float)mytex.Width * 0.5f, (float)((mytex.Height-2*(FrameNum-1))/FrameNum) * 0.5f),
                            chainScalar,
                            SpriteEffects.None, 
                            0f);
                            FakeVelocity = RotateAboutOrigin(FakeVelocity,Vector2.Zero,((float)(Math.PI*2))/segmants);
					}
    return false;
}

 public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            Vector2 u = point - origin; //point relative to origin  

            if (u == Vector2.Zero)
                return point;

            float a = (float)Math.Atan2(u.Y, u.X); //angle relative to origin  
            a += rotation; //rotate  

            //u is now the new point relative to origin  
            u = u.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            return u + origin;
        } 



/*

public static Vector2[] PhlebasRandom(float Xoffset,float Yoffset,float length,float heightradius,int SplitTimes)
{
    Vector2[] arr = new Vector2[2];
    int range = arr.Length;
    arr[0] = new Vector2(0f,0f);
    arr[1] = new Vector2(length,0f);
    if(SplitTimes <= 0)
        return arr;
    int TimesSplit = 0;
    while(TimesSplit < SplitTimes)
    {
        Vector2[] arr2 = new Vector2[2+(int)Math.Pow(2,TimesSplit)];
        for(int i = 0; i < range; i++)
        {
            //using a new vector to avoids references.
            arr2[i*2] = new Vector2(arr[i].X,arr[i].Y);
        }
        for(int i = 1; i < range-1; i+=2)
        {
            arr2[i] = SplitSegmant(arr2,heightradius/(TimesSplit+1),i-1,i+1);
        }
        arr = new Vector2[arr2.Length];
        range = arr.Length;
        for(int i = 0; i < range; i++)
        {
            arr[i] = new Vector2(arr2[i].X,arr2[i].Y);
        }
        TimesSplit++;
    }
    for (int i = 0; i < range; i++)
    {
        arr[i]+=new Vector2(Xoffset,Yoffset);	 
    }
    return arr;
}
public static Vector2 SplitSegmant(Vector2[] arr,float heightRadius,int startpoint,int endpoint)
{
    float Xavg = (arr[startpoint].X+arr[endpoint].X)/2f;
    float Yavg = (arr[startpoint].Y+arr[endpoint].Y)/2f;
    float scalar = (float)Main.rand.NextDouble()-(float)Main.rand.NextDouble(); //number between -1 and 1
    Yavg += heighRadius*scalar;
    return new Vector2(Xavg,Yavg);
}
*/