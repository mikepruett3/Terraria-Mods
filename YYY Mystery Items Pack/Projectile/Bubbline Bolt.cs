
public void AI()
{
    float red = 1f;
    float green = 0.1f;
    float blue = 0.1f;
    Projectile P = projectile;
    Lighting.addLight((int)((P.position.X + (float)(P.width / 2)) / 16f), (int)((P.position.Y + (float)(P.height / 2)) / 16f), red, green, blue);                
    if (P.alpha > 0)
    {
        P.alpha -= 15;
    }
    if (P.alpha < 0)
    {
        P.alpha = 0;
    }
    P.rotation = (float)Math.Atan2((double)P.velocity.Y, (double)P.velocity.X) + 1.57f;
}
public void Initialize()
{
    Projectile P = projectile;
    Player PO = Main.player[P.owner];
    Vector2 Dist = new Vector2(0,-16f);
    P.velocity = RotateAboutOrigin(Dist,Vector2.Zero,(PO.itemRotation)+(float)(Math.PI/2)*PO.direction);    
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