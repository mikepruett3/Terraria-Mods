

public void AI()
{
    Projectile P = projectile;
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
    Player Ow = Main.player[P.owner];
    Vector2 OwC = Ow.position+new Vector2(Ow.width/2,Ow.height/2);
    
    P.penetrate = 100;
    P.timeLeft = 3;
    P.rotation += 0.1f;
    float Scaler = P.velocity.Length();
    P.scale = MathHelper.Lerp(P.scale,Math.Max(Scaler / 7 , 1),0.05f);
    P.damage = (int)((P.scale-1f)*20f);
    P.alpha = 250-(int)(100*P.scale);

    Lighting.addLight(
    (int)((PC.X) / 16f), 
    (int)((PC.Y) / 16f), 
    0.3f*P.scale, 
    0.3f*P.scale, 
    0.3f*P.scale
    );           
    
    if(!Ow.active || Ow.dead)
    {
        P.Kill();
        return;
    }
    if (Vector2.Distance(PC,OwC) > 1500f)
    {
        P.position=OwC;
        return;
    }

    float Projectile_Speed = 12f;
    float Dist_X = (float)Main.mouseX + Main.screenPosition.X - PC.X;
    float Dist_Y = (float)Main.mouseY + Main.screenPosition.Y - PC.Y;
    float Dist_Total = (float)Math.Sqrt((double)(Dist_X * Dist_X + Dist_Y * Dist_Y));
    if(Main.myPlayer == P.owner)
    {
        if (Dist_Total > Projectile_Speed)
        {
            Dist_Total = Projectile_Speed / Dist_Total;
            Dist_X *= Dist_Total;
            Dist_Y *= Dist_Total;
        }
        int Extreme_Dist_X1 = (int)(Dist_X * 1000f);
        int Extreme_Dist_X2 = (int)(P.velocity.X * 1000f);
        int Extreme_Dist_Y1 = (int)(Dist_Y * 1000f);
        int Extreme_Dist_Y2 = (int)(P.velocity.Y * 1000f);
        if (Extreme_Dist_X1 != Extreme_Dist_X2 || Extreme_Dist_Y1 != Extreme_Dist_Y2)
        {
            P.netUpdate = true;
        }
        P.velocity.X = Dist_X;
        P.velocity.Y = Dist_Y;
    }
}
