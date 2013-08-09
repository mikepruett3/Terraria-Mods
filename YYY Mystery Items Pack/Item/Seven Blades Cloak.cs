public static void Effects(Player player)
{
    if(player.whoAmi == Main.myPlayer)
    {
        bool shouldspawn = false;
        int index = 0;
        foreach (Projectile P in Main.projectile)
        {
            if (P.active && P.owner == Main.myPlayer && P.type == Config.projDefs.byName["Seventh Cloak Blade"].type)
            {
                index++;
            }
        }

        if(index < 7)
        {
            shouldspawn = true;
        }
        if(index == 0 || shouldspawn)
        {
            int a = Projectile.NewProjectile(player.position.X+player.width/2,player.position.Y+player.height/2,0,0,"Seventh Cloak Blade",5,0,Main.myPlayer);
            //Main.projectile[a].RunMethod("SetBladeInfo",index+1);

            //if (player.whoAmi == Main.myPlayer)
            //{
            //    if(Main.dedServ) return;
            //    if (Main.gamePaused) return;
            //    int ArmyIndex = index+1;
            //    int ProjIndex = a;
            //    int WeaponType = 7;
            //    int num = 1;
            //    num+=ProjIndex*10;
            //    num+=ArmyIndex*10000;
            //    num+=WeaponType*100000;
            //    NetMessage.SendModData(ModWorld.modIndex, num, -1, -1, (byte)player.whoAmi);
            //    //do stuff
            //}
        }
    }
}

public void HoldStyle(Player player)
{
    if(player.whoAmi == Main.myPlayer)
    {
        bool HasMeEquipped = false;
        foreach ( Item I in Main.player[Main.myPlayer].armor )
        {
            if (I.type == this.item.type)
            {
                HasMeEquipped = true;
            }
        }
        if (!HasMeEquipped)
        {
            foreach (Projectile P in Main.projectile)
            {
                if (P.active && P.owner == Main.myPlayer && P.type == Config.projDefs.byName["Seventh Cloak Blade"].type)
                {
                    P.Kill();
                }
            }
        }
    }
}