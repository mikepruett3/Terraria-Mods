int x=0;


public static void UseItemEffect(Player player, Rectangle rectangle) 
{
    Color color = new Color();
    int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 26, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 100, color, 1.9f);
    Main.dust[dust].noGravity = true;
	/*if (Main.rand.Next(200)==1)
		{
		NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Spawned Skeleton",0);
		}
	Old code ;p */


}

public void HoldStyle(Player player)
{
	x++;
	if (player.controlUseTile && player.statMana > 50 && x > 30)
		{
		NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Spawned Skeleton",0);
		player.statMana-=50;
		x=0;
		}
}