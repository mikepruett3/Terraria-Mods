public void UseItem(Player player, int playerID)
{
	if (playerID == Main.myPlayer)
	{
                        Item MyCheckedItem = new Item();
                        bool FoundUsableAmmo = false;
                        bool FoundAmmoInAmmoSlot = false;
                        if (!FoundAmmoInAmmoSlot)
                        {
                            for (int PassingIndex = 0; PassingIndex < 44; PassingIndex++)
                            {
                                if (player.inventory[PassingIndex].type != item.type && player.inventory[PassingIndex].stack > 0)
                                {
                                    MyCheckedItem = player.inventory[PassingIndex];
                                    FoundUsableAmmo = true;
                                    break;
                                }
                            }
                        }
                        if (FoundUsableAmmo)
                        {
                            bool DoNotConsumeAmmo = false;
                            if (player.ammoCost80 && Main.rand.Next(5) == 0)
                            {
                                DoNotConsumeAmmo = true;
                            }
                            if (player.ammoCost75 && Main.rand.Next(4) == 0)
                            {
                                DoNotConsumeAmmo = true;
                            }
                            if (!DoNotConsumeAmmo)
                            {
                                   MyCheckedItem.stack--;
                            }
    }
    if(FoundUsableAmmo)
    {
	    int MyProjectileIndex = -1;
        int MyProjectileSpeed = 10;
        float VX = ((Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f));
        float VY = ((Main.mouseY + Main.screenPosition.Y) - (player.position.Y + player.height * 0.5f));
        float distance = (float) Math.Sqrt((double) ((VX * VX) + (VY * VY)));
	    distance = MyProjectileSpeed / distance;
	    VX *= distance;
	    VY *= distance; 
        Vector2 Center = player.position+new Vector2(player.width/2,player.height/2)+new Vector2(player.direction*7f,0);
	    MyProjectileIndex = Projectile.NewProjectile(
    	Center.X,
    	Center.Y,
    	VX,
    	VY,
    	"Litteral Blade Shot", 
    	(int) (item.damage*player.magicDamage),
    	2.0f,
    	playerID
        );
        Main.projectile[MyProjectileIndex].RunMethod("BeLitteral",MyCheckedItem.type,MyCheckedItem.prefix);
        if (MyCheckedItem.stack <= 0)
        {
            MyCheckedItem.active = false;
            MyCheckedItem.name = "";
            MyCheckedItem.type = 0;
        }
	    NetMessage.SendData(27, -1, -1, "", MyProjectileIndex, 0f, 0f, 0f, 0);
	    }    
    }             
}



public void UseStyle(Player player)
{
    #region usestyle
    float scalez = 0.9f;
    Lighting.addLight((int)((player.itemLocation.X + (float)(player.itemWidth / 2)) / 16f), (int)((player.itemLocation.Y + (float)(player.itemHeight / 2)) / 16f), scalez, scalez, scalez);  

    int MyProjectileSpeed = 10;
    float VX = ((Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f));
    float VY = ((Main.mouseY + Main.screenPosition.Y) - (player.position.Y + player.height * 0.5f));
    float distance = (float) Math.Sqrt((double) ((VX * VX) + (VY * VY)));
	distance = MyProjectileSpeed / distance;
	VX *= distance;
	VY *= distance; 
	                                    player.itemLocation.X = player.position.X + (float)player.width * 0.5f - (float)Main.itemTexture[item.type].Width * 0.5f - (float)(player.direction * 2 * 0.8f);
                                        player.itemLocation.Y = player.position.Y + (float)player.height * 0.5f - (float)Main.itemTexture[item.type].Height * 0.5f;
                                        player.itemRotation = (float)Math.Atan2((double)(VY * (float)player.direction), (double)(VX * (float)player.direction));
#endregion        
}