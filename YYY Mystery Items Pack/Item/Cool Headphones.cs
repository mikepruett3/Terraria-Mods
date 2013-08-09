public static void Effects(Player player)
{
    if(player.whoAmi == Main.myPlayer)
    {
        bool ShouldSpawn = false;
        foreach (Projectile P in Main.projectile)
        {
            if (P.active && P.type == Config.projDefs.byName["Light Puck"].type)
            {
                ShouldSpawn = true;
                break;
            }
        }
        if (!ShouldSpawn)
        {
            Projectile.NewProjectile(player.position.X,player.position.Y,0,0,"Light Puck",25,0,Main.myPlayer);
        }
    }
}

public void HoldStyle(Player player)
{
    if(player.whoAmi == Main.myPlayer)
    {
        bool SpareLife = false;
        foreach ( Item I in Main.player[Main.myPlayer].armor )
        {
            if (I.type == item.type)
            {
                SpareLife = true;
            }
        }
        if (!SpareLife)
        {
            foreach (Projectile P in Main.projectile)
            {
                if (P.active && P.type == Config.projDefs.byName["Light Puck"].type)
                {
                    P.Kill();
                }
            }
        }
    }
}