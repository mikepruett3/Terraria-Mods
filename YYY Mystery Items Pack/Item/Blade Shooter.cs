public void UseItem(Player player, int playerID)
{
    Player P = player;
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
	if (playerID == Main.myPlayer)
	{

        int Projectile_Index = -1;
        int Projectile_Speed = 10;

        int ROR = 20;
        float x = (float) (PC.X) - 7 + ROR + Main.rand.Next(ROR);
	    player.direction = -1;

	    if ((player.position.X + (player.width/2)) < (Main.mouseX + Main.screenPosition.X))
	    {
	        player.direction = 1;
            x = (float) (PC.X) - 7 - ROR + Main.rand.Next(ROR);
        }

        float y = (float) (player.position.Y + (player.height/2))-Main.rand.Next(-ROR,ROR);

        float VX = ((Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f));
        float VY = ((Main.mouseY + Main.screenPosition.Y) - (player.position.Y + player.height * 0.5f));
        float VT = (float) Math.Sqrt((double) ((VX * VX) + (VY * VY)));
	    VT = Projectile_Speed / VT;
	    VX *= VT;
	    VY *= VT; 

	    Projectile_Index = Projectile.NewProjectile(
    	x,
    	y,
    	VX,
    	VY,
    	"Blade Shot", //Type
    	(int) (item.damage*player.magicDamage), //Damage
    	2.0f,
    	playerID
        );
	    NetMessage.SendData(27, -1, -1, "", Projectile_Index, 0f, 0f, 0f, 0);
	}
}

public void UseStyle(Player player)
{
    float scalez = 0.9f;
    Lighting.addLight((int)((player.itemLocation.X + (float)(player.itemWidth / 2)) / 16f), (int)((player.itemLocation.Y + (float)(player.itemHeight / 2)) / 16f), scalez, scalez, scalez);  
    
    Player P = player;
    Vector2 PC = P.position+new Vector2(P.width/2,P.height/2);
    int Projectile_Speed = 10;
    float VX = ((Main.mouseX + Main.screenPosition.X) - PC.X);
    float VY = ((Main.mouseY + Main.screenPosition.Y) - PC.Y);
    float VT = (float) Math.Sqrt((double) ((VX * VX) + (VY * VY)));
    VT = Projectile_Speed / VT;
    VX *= VT;
    VY *= VT; 
    P.itemLocation.X = PC.X - (float)Main.itemTexture[item.type].Width * 0.5f - (float)(P.direction * 2 * 0.8f);
    P.itemLocation.Y = PC.Y - (float)Main.itemTexture[item.type].Height * 0.5f;
    P.itemRotation = (float)Math.Atan2((double)(VY * (float)P.direction), (double)(VX * (float)P.direction));
}
