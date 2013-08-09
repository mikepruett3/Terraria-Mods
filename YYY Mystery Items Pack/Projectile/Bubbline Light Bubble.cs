
public void AI() 
{
    Projectile P = projectile;
    P.timeLeft--;
    if (Main.myPlayer == P.owner)
    {
        if (Main.player[P.owner].channel)
        { 
            Projectile P_Target = Main.projectile[(int)P.ai[1]];
            Player PO = Main.player[P.owner];
            Vector2 Dist = new Vector2(0,-40f);
            Dist = RotateAboutOrigin(Dist,Vector2.Zero,(PO.itemRotation)+(float)(Math.PI/2)*PO.direction);
            Vector2 P_Target_Center = new Vector2(Dist.X+PO.position.X+PO.width/2,Dist.Y+PO.position.Y+PO.height/2);
            
            P_Target_Center += Dist;
            float Speed = 12f;
            Vector2 P_Center = new Vector2(P.position.X + (float)P.width * 0.5f, P.position.Y + (float)P.height * 0.5f);
            float DistX = (float)P_Target_Center.X - P_Center.X;
            float DistY = (float)P_Target_Center.Y - P_Center.Y;
            float DistTotal = (float)Math.Sqrt((double)(DistX * DistX + DistY * DistY));
            DistTotal = (float)Math.Sqrt((double)(DistX * DistX + DistY * DistY));
            if (DistTotal > Speed)
            {
                DistTotal = Speed / DistTotal;
                DistX *= DistTotal;
                DistY *= DistTotal;
            }
            P.velocity.X -= 3f*((P.position.X - (P_Target_Center.X))/1000);
            P.velocity.Y -= 3f*((P.position.Y - (P_Target_Center.Y))/1000);
            int MassDisteX = (int)(DistX * 1000f);
            int MassVeleX = (int)(P.velocity.X * 1000f);
            int MassDisteY = (int)(DistY * 1000f);
            int MassVeleY = (int)(P.velocity.Y * 1000f);
            if (MassDisteX != MassVeleX || MassDisteY != MassVeleY)
            {
                P.netUpdate = true;
            }
        }
        else
        {
            int Index = Projectile.NewProjectile(P.position.X+P.width/2,P.position.Y+P.height/2,0,0,"Bubbline Bolt",P.damage,0,P.owner);
            NetMessage.SendData(27, -1, -1, "", Index, 0f, 0f, 0f, 0);
            P.active = false;
        }
    }
    int limit = 5;
    if(P.velocity.X > limit) P.velocity.X = limit;
    if(P.velocity.X < -limit) P.velocity.X = -limit;
    if(P.velocity.Y > limit) P.velocity.Y = limit;
    if(P.velocity.Y < -limit) P.velocity.Y = -limit;
}

public void Initialize()
{
    Projectile P = projectile;
    int multiplier = 0;
    while(Vector2.Distance(P.velocity,Vector2.Zero) < 3)
    {
    multiplier = Main.rand.Next(2);
    if(multiplier == 0)
        multiplier = -1;
    P.velocity.X = Main.rand.Next(0,5);
    P.velocity.X *= multiplier;
    multiplier = Main.rand.Next(2);
    if(multiplier == 0)
        multiplier = -1;
    P.velocity.Y = Main.rand.Next(0,5);
    P.velocity.Y *= multiplier;
    }
    P.timeLeft = 300;
}

public void Kill()
{
    Projectile P = projectile;
    int Index = Projectile.NewProjectile(P.position.X+P.width/2,P.position.Y+P.height/2,0,0,"Bubbline Bolt",P.damage,0,P.owner);
    NetMessage.SendData(27, -1, -1, "", Index, 0f, 0f, 0f, 0);
    projectile.active = false;
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