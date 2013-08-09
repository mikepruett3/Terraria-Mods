

public void AI()
{
    Projectile P = projectile;
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
    P.rotation = (float)Math.Atan2((double)P.velocity.Y, (double)P.velocity.X) + (float)(Math.PI/2);
    if (P.ai[0] == 0f)
    {
        P.alpha -= 50;
        if (P.alpha <= 0)
        {
            P.alpha = 0;
            P.ai[0] = 1f;
            if (P.ai[1] == 0f)
            {
                P.ai[1] += 1f;
                P.position += P.velocity * 1f;
            }
            if (P.type == 7 && Main.myPlayer == P.owner)
            {
                int Projectile_Type = P.type;
                if (P.ai[1] >= 15) // change for max parts
                {
                    Projectile_Type = Config.projDefs.byName["Highthorn2"].type;
                }
                else
                    Projectile_Type = Config.projDefs.byName["Highthorn1"].type;
                if((int)(P.ai[1])%3==0)
                {
                    Vector2 MemoryUnit = new Vector2(P.velocity.X,P.velocity.Y);

                    float RotatoryAngle = (float)((Math.PI*1)/8f)*(float)Main.rand.NextDouble();
                    
                    #region Projectile 1
                    
                    P.velocity=RotateAboutOrigin(MemoryUnit,RotatoryAngle);

                    int Projectile_Index = Projectile.NewProjectile(
                    PC.X + P.velocity.X , 
                    PC.Y + P.velocity.Y, 
                    P.velocity.X, 
                    P.velocity.Y, 
                    Projectile_Type, 
                    P.damage, 
                    P.knockBack, 
                    P.owner
                    );
                    Projectile Inheritor = Main.projectile[Projectile_Index];
                    Inheritor.damage = P.damage;
                    Inheritor.ai[1] = P.ai[1] + 1f;
                    NetMessage.SendData(27, -1, -1, "", Projectile_Index, 0f, 0f, 0f, 0);

                    #endregion

                    #region Projectile 2

                    RotatoryAngle = (float)((Math.PI*1)/8f)*(float)Main.rand.NextDouble();

                    P.velocity=RotateAboutOrigin(MemoryUnit,-RotatoryAngle);

                    Projectile_Index = Projectile.NewProjectile(
                    PC.X + P.velocity.X , 
                    PC.Y + P.velocity.Y, 
                    P.velocity.X, 
                    P.velocity.Y, 
                    Projectile_Type, 
                    P.damage, 
                    P.knockBack, 
                    P.owner
                    );
                    Inheritor = Main.projectile[Projectile_Index];
                    Inheritor.damage = P.damage;
                    Inheritor.ai[1] = P.ai[1] + 1f;
                    NetMessage.SendData(27, -1, -1, "", Projectile_Index, 0f, 0f, 0f, 0);

                    #endregion
                }
                else
                {
                    int Projectile_Index = Projectile.NewProjectile(
                    PC.X + P.velocity.X , 
                    PC.Y + P.velocity.Y, 
                    P.velocity.X, 
                    P.velocity.Y, 
                    Projectile_Type, 
                    P.damage, 
                    P.knockBack, 
                    P.owner
                    );
                    Projectile Inheritor = Main.projectile[Projectile_Index];
                    Inheritor.damage = P.damage;
                    Inheritor.ai[1] = P.ai[1] + 1f;
                    NetMessage.SendData(27, -1, -1, "", Projectile_Index, 0f, 0f, 0f, 0);
                }
                return;
            }
        }
    }
    else
    {
        if (P.alpha < 170 && P.alpha + 5 >= 170)
        {
            for (int j = 0; j < 3; j++)
            {
                Dust.NewDust(P.position, P.width, P.height, 18, P.velocity.X * 0.025f, P.velocity.Y * 0.025f, 170, default(Color), 1.2f);
            }
            Dust.NewDust(P.position, P.width, P.height, 14, 0f, 0f, 170, default(Color), 1.1f);
        }
        P.alpha += 5;
        if (P.alpha >= 255)
        {
            P.Kill();
            return;
        }
    }            
}  


#region Rotate by Angles


public Vector2 RotateByRightAngle(Vector2 vector)
{
    return new Vector2(vector.Y, -vector.X);
}


public Vector2 RotateByLeftAngle(Vector2 vector)
{
    return new Vector2(-vector.Y, vector.X);
}


#endregion


#region Rotate About Origin


        public Vector2 RotateAboutOrigin(Vector2 point, float rotation)
        {
            if(rotation < 0)
                rotation+=(float)(Math.PI*4);
            Vector2 u = point; //point relative to origin  

            if (u == Vector2.Zero)
                return point;

            float a = (float)Math.Atan2(u.Y, u.X); //angle relative to origin  
            a += rotation; //rotate  

            //u is now the new point relative to origin  
            u = u.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            return u;
        } 


#endregion