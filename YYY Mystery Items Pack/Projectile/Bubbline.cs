

int Ticker = 0;

public void AI()
{

    #region Aesthetics
    if (Main.myPlayer == projectile.owner)
    {
        if (Main.player[projectile.owner].channel)
        {
            float num119 = 0f;
            if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shoot == projectile.type)
            {
                num119 = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed * projectile.scale;
            }
            Vector2 vector14 = new Vector2(Main.player[projectile.owner].position.X + (float)Main.player[projectile.owner].width * 0.5f, Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height * 0.5f);
            float num120 = (float)Main.mouseX + Main.screenPosition.X - vector14.X;
            float num121 = (float)Main.mouseY + Main.screenPosition.Y - vector14.Y;
            float num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
            num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
            num122 = num119 / num122;
            num120 *= num122;
            num121 *= num122;
            if (num120 != projectile.velocity.X || num121 != projectile.velocity.Y)
            {
                projectile.netUpdate = true;
            }
            projectile.velocity.X = num120;
            projectile.velocity.Y = num121;
        }
        else
        {
            projectile.Kill();
        }
    }
    if (projectile.velocity.X > 0f)
    {
        Main.player[projectile.owner].direction = 1;
        projectile.spriteDirection = 1;
    }
    else
    {
        if (projectile.velocity.X < 0f)
        {
            Main.player[projectile.owner].direction = -1;
            projectile.spriteDirection = 1;
        }
    }
    projectile.spriteDirection = projectile.direction;
    Main.player[projectile.owner].direction = projectile.direction;
    Main.player[projectile.owner].heldProj = projectile.whoAmI;
    Main.player[projectile.owner].itemTime = 2;
    Main.player[projectile.owner].itemAnimation = 2;
    projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
    projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
    projectile.rotation = (float)(Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) );
    if (Main.player[projectile.owner].direction == 1)
    {
        Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
    }
    else
    {
        Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
    }
    if (projectile.spriteDirection == -1)
    {
        projectile.rotation -= (float)Math.PI; //half pie
    }
    #endregion

    #region Spawning bubbles
    Projectile P = projectile;
    if(Ticker <= 0)
    {
        Ticker = 15;
        Player PO = Main.player[P.owner];
        Vector2 Dist = new Vector2(0,-40f);
        Dist = RotateAboutOrigin(Dist,Vector2.Zero,(PO.itemRotation)+(float)(Math.PI/2)*PO.direction);
        int Index = Projectile.NewProjectile(Dist.X+PO.position.X+PO.width/2,Dist.Y+PO.position.Y+PO.height/2,0,0,"Bubbline Light Bubble",P.damage,0,P.owner);
        Main.projectile[Index].ai[1] = P.whoAmI;
        NetMessage.SendData(27, -1, -1, "", Index, 0f, 0f, 0f, 0);
    }
    else
        Ticker--;

    #endregion
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