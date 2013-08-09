public void UseItem(Player player, int playerID)
{
	if (playerID == Main.myPlayer)
	{

        #region if it needs ammo

        Item MyCheckedItem = new Item();
        bool FoundUsableAmmo = false;
        bool FoundAmmoInAmmoSlot = false;
        for (int PassingIndex = 44; PassingIndex < 48; PassingIndex++)
        {
            if (player.inventory[PassingIndex].ammo == item.useAmmo && player.inventory[PassingIndex].stack > 0)
            {
                MyCheckedItem = player.inventory[PassingIndex];
                FoundUsableAmmo = true;
                FoundAmmoInAmmoSlot = true;
                break;
            }
        }
        if (!FoundAmmoInAmmoSlot)
        {
            for (int num24 = 0; num24 < 44; num24++)
            {
                if (player.inventory[num24].ammo == item.useAmmo && player.inventory[num24].stack > 0)
                {
                    MyCheckedItem = player.inventory[num24];
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

        #endregion

        if(FoundUsableAmmo)
        {

        #region regular

	        int Projectile_Index = -1;
            int Projectile_Speed = 10;
            int ROR = 5;
            Vector2 IC = player.itemLocation+new Vector2(Main.itemTexture[item.type].Width/2,Main.itemTexture[item.type].Height/2);
            float x = (float) (IC.X)-7-ROR+Main.rand.Next(ROR);
	        player.direction = -1;
	        if ((IC.X) < (Main.mouseX + Main.screenPosition.X))
	        {
                player.direction = 1;
                x = (float) (IC.X)-7+ROR+Main.rand.Next(ROR);
            }
            float y = (float) (IC.Y)-Main.rand.Next(-ROR,ROR);

            float VX = ((Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f));
            float VY = ((Main.mouseY + Main.screenPosition.Y) - (player.position.Y + player.height * 0.5f));
            float VT = (float) Math.Sqrt((double) ((VX * VX) + (VY * VY)));
	        VT = Projectile_Speed / VT;
	        VX *= VT;
	        VY *= VT; 

	        Projectile_Index = Projectile.NewProjectile(
    	    (int)IC.X,
    	    (int)IC.Y,
    	    VX,
    	    VY,
    	    "Blade Shot", //Type
    	    (int) (item.damage*player.rangedDamage), //Damage
    	    2.0f,
    	    playerID
            );
	        NetMessage.SendData(27, -1, -1, "", Projectile_Index, 0f, 0f, 0f, 0);

            if (MyCheckedItem.stack <= 0)
            {
                MyCheckedItem.active = false;
                MyCheckedItem.name = "";
                MyCheckedItem.type = 0;
            }
            #endregion

	    }    
    }             
}



public void UseStyle(Player P)
{

    #region usestyle

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

    #endregion        

}