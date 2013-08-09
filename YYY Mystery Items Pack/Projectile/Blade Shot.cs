
public void AI()
{
    Projectile P = projectile;
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
    float Light_Scaler = 0.2f;
    if(P.ai[0] == 0)
    {
        P.ai[0] = P.damage;
    }
    P.damage = (int)P.ai[0];
    foreach(Projectile P2 in Main.projectile)
    {
        if(P2.active && P2.type == P.type && P2.owner == P.owner)
        {
            Vector2 PC2 = P2.position+new Vector2(P2.width/2,P2.height/2);
            if(Vector2.Distance(PC,PC2) < 100f)
            {
                Light_Scaler+= 0.14f;
                P.damage += 4;
            }
        }
    }
    if(Light_Scaler > 1.3f)
    {
        int dusttype = 43;
        float dustscale = 2f;
        int num41 = Dust.NewDust(new Vector2(P.position.X + P.velocity.X, P.position.Y + P.velocity.Y), P.width, P.height, dusttype, P.velocity.X, P.velocity.Y, 100, new Color(255,255,255,255), dustscale * P.scale);
        Main.dust[num41].noGravity = true;
    }

    Lighting.addLight(
    (int)((PC.X) / 16f), 
    (int)((PC.Y) / 16f), 
    Light_Scaler, 
    Light_Scaler, 
    Light_Scaler
    );  

    P.rotation = (float)Math.Atan2((double)P.velocity.Y, (double)P.velocity.X) + (float)(Math.PI/2);
}




public float SMDfloat(object var1,object var2)
{
#region returns float of distance
    float dist = 0;
    Vector2[] A1 = new Vector2[2];
    object varz = var1;
    for (int i = 0; i < 2; i++)
    {
    if (i == 0)
        varz = var1;
    if (i == 1)
        varz = var2;
    if (varz is Player)
    {
        Player pl = (Player)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }
    if (varz is Projectile)
    {
        Projectile pl = (Projectile)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }
    if (varz is NPC)
    {
        NPC pl = (NPC)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }	 
    }
    
    return Vector2.Distance(A1[0],A1[1]);
#endregion
}



public Vector2 SMDV2(object var1,object var2)
{
#region returns vector2 of the distance
    float dist = 0;
    Vector2[] A1 = new Vector2[2];
    object varz = var1;
    for (int i = 0; i < 2; i++)
    {
    if (i == 0)
        varz = var1;
    if (i == 1)
        varz = var2;
    if (varz is Player)
    {
        Player pl = (Player)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }
    if (varz is Projectile)
    {
        Projectile pl = (Projectile)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }
    if (varz is NPC)
    {
        NPC pl = (NPC)varz;
        A1[i] = new Vector2(pl.position.X+(pl.width/2),pl.position.Y+(pl.height/2));
    }	 
    }
    
    return A1[0]-A1[1];
#endregion
}