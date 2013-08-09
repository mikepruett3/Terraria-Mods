public float OriginX = -1f;
public float OriginY = -1f;
public int randNum = -1;
public Vector2 lastNonZeroVelocity;
public void Initialize()
{
    projectile.timeLeft = 60;
    this.OriginX = projectile.position.X;
    this.OriginY = projectile.position.Y;
}

public void AI()
{
    #region projectile life check
    if (Main.player[projectile.owner].dead)
    {
        projectile.Kill();
        return;
    }
    #endregion
    #region projectile rotation
    Vector2 vector3 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
    float num20 = this.OriginX - vector3.X;
    float num21 = this.OriginY - vector3.Y;
    float num22 = (float)Math.Sqrt((double)(num20 * num20 + num21 * num21));
    projectile.rotation = (float)Math.Atan2((double)num21, (double)num20) - 1.57f;
    #endregion
    if (projectile.ai[0] == 0f)
    {
        #region reached range limit
        if (num22 > 6000f) //range for comeback
        {
            this.OriginX += projectile.velocity.X;
            this.OriginY += projectile.velocity.Y;
            //projectile.ai[0] = 1f;
        }
        #endregion
        #region go aruond
        int num23 = (int)(projectile.position.X / 16f) - 1;
        int num24 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
        int num25 = (int)(projectile.position.Y / 16f) - 1;
        int num26 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
        if (num23 < 0)
        {
            num23 = 0;
        }
        if (num24 > Main.maxTilesX)
        {
            num24 = Main.maxTilesX;
        }
        if (num25 < 0)
        {
            num25 = 0;
        }
        if (num26 > Main.maxTilesY)
        {
            num26 = Main.maxTilesY;
        }
        for (int n = num23; n < num24; n++)
        {
            int num27 = num25;
            while (num27 < num26)
            {
                if (Main.tile[n, num27] == null)
                {
                    Main.tile[n, num27] = new Tile();
                }
                Vector2 vector4;
                vector4.X = (float)(n * 16);
                vector4.Y = (float)(num27 * 16);
                float SizeMultiplier = 0.25f;
                #region catch something
                if (projectile.position.X + (float)projectile.width*SizeMultiplier > vector4.X && projectile.position.X < vector4.X + 16f && projectile.position.Y + (float)projectile.height*SizeMultiplier > vector4.Y && projectile.position.Y < vector4.Y + 16f && Main.tile[n, num27].active && Main.tileSolid[(int)Main.tile[n, num27].type])
                {
                    WorldGen.KillTile(n, num27, true, true, false);
                    Main.PlaySound(0, n * 16, num27 * 16, 1);
                    lastNonZeroVelocity = new Vector2(projectile.velocity.X,projectile.velocity.Y);
                    projectile.velocity.X = 0f;
                    projectile.velocity.Y = 0f;
                    projectile.ai[0] = 2f;
                    projectile.position.X = (float)(n * 16 + 8 - projectile.width / 2);
                    projectile.position.Y = (float)(num27 * 16 + 8 - projectile.height / 2);
                    //projectile.damage = 0;
                    projectile.netUpdate = true;
                    break;
                }
                #endregion
                else
                {
                    num27++;
                }
            }
            if (projectile.ai[0] == 2f)
            {
                return;
            }
        }
        return;
        #endregion
    }
    if (projectile.ai[0] == 1f)
    {
        projectile.Kill();
        return;
        #region fail and reverse
        float num33 = 11f;
        if (num22 < 24f)
        {
            projectile.Kill();
        }
        num22 = num33 / num22;
        num20 *= num22;
        num21 *= num22;
        projectile.velocity.X = num20;
        projectile.velocity.Y = num21;
        return;
        #endregion
    }
    if (projectile.ai[0] == 2f)
    {
        projectile.ai[1]++;
        if(projectile.ai[1] > 300f)
        {
        this.OriginX += lastNonZeroVelocity.X;
        this.OriginY += lastNonZeroVelocity.Y;
        if(Vector2.Distance(new Vector2(OriginX,OriginY),projectile.position) < 30f)
        {
            projectile.Kill();
            return;
        }
        }
        #region detect breakness
        int num34 = (int)(projectile.position.X / 16f) - 1;
        int num35 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
        int num36 = (int)(projectile.position.Y / 16f) - 1;
        int num37 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
        if (num34 < 0)
        {
            num34 = 0;
        }
        if (num35 > Main.maxTilesX)
        {
            num35 = Main.maxTilesX;
        }
        if (num36 < 0)
        {
            num36 = 0;
        }
        if (num37 > Main.maxTilesY)
        {
            num37 = Main.maxTilesY;
        }
        bool flag = true;
        for (int num38 = num34; num38 < num35; num38++)
        {
            for (int num39 = num36; num39 < num37; num39++)
            {
                if (Main.tile[num38, num39] == null)
                {
                    Main.tile[num38, num39] = new Tile();
                }
                Vector2 vector5;
                vector5.X = (float)(num38 * 16);
                vector5.Y = (float)(num39 * 16);
                if (projectile.position.X + (float)(projectile.width / 2) > vector5.X && projectile.position.X + (float)(projectile.width / 2) < vector5.X + 16f && projectile.position.Y + (float)(projectile.height / 2) > vector5.Y && projectile.position.Y + (float)(projectile.height / 2) < vector5.Y + 16f && Main.tile[num38, num39].active && Main.tileSolid[(int)Main.tile[num38, num39].type])
                {
                    flag = false;
                }
            }
        }
        if (flag)
        {
            projectile.ai[0] = 0f;
            float speed = 15f;
            Vector2 Origin  = new Vector2(OriginX,OriginY);
            float distance = Vector2.Distance(projectile.position,Origin);
            float ratio = speed/distance;
            projectile.velocity = (Origin-projectile.position)*-ratio;
        }
        #endregion
    }
}

public bool PreDraw(SpriteBatch sp)
{
    Texture2D mytex=Main.goreTexture[Config.goreID["Blade Beam Phase 1"]];
    float chainScalar = 1f;
    int chainTransper = 0;
    int FrameNum=1;
    if(this.randNum == -1 || (int)(Main.time) % 4 == 0)
        this.randNum = Main.rand.Next(FrameNum);
    int randPart = this.randNum-1;
					Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num5 = this.OriginX - vector2.X;
					float num6 = this.OriginY - vector2.Y;
					float rotation2 = (float)Math.Atan2((double)num6, (double)num5) - 1.57f;
					bool flag2 = true;
					while (flag2)
					{
                       // chainScalar -= 0f;
                        chainTransper+= 5;
                        randPart++;
                        if(randPart % FrameNum == 0)
                            randPart = 0;
						float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
						if (num7 < 25f)
						{
							flag2 = false;
						}
						else
						{
#region laser light
                            float red  = 0.3f;
                            float green  = 0.2f;
                            float blue  = 0.2f;
                            Lighting.addLight((int)((vector2.X + (float)(projectile.width / 2)) / 16f), (int)((vector2.Y + (float)(projectile.height / 2)) / 16f), red, green, blue);
#endregion
#region laserdust

            if (Main.rand.Next(5) == 0)
            {
                Color newColor;
                Vector2 dustpos = projectile.position;
                int projwidth = projectile.width;
                int projheight = projectile.height;
                int dusttype = 60; // this controls a dust type
                float randspeedx = 0f;
                float randspeedy = 0f;
                int dustalpha = 150;
                float dustscale = 3f;
                newColor = default(Color);
                int zz = Dust.NewDust(vector2-new Vector2(projectile.width/2f,projectile.height/2f), projectile.width, projectile.height, dusttype, randspeedx, randspeedy, dustalpha, newColor, dustscale);
                Main.dust[zz].noGravity = true;
            }
#endregion

							num7 = (float)((mytex.Height-2*(FrameNum-1))/FrameNum) * 0.5f / num7;
							num5 *= num7;
							num6 *= num7;
							vector2.X += num5;
							vector2.Y += num6;
							num5 = this.OriginX - vector2.X;
							num6 = this.OriginY - vector2.Y;
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
                            #region damaging


Rectangle rectangle = new Rectangle((int)vector2.X, (int)vector2.Y, projectile.width, projectile.height);
#region damage time
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
						}
					}
    return true;
}