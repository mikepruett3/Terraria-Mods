int x=0;

public static void UseItemEffect(Player player, Rectangle rectangle) 
	{
    Color color = new Color();
    int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 33, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 100, color, 1.9f);
    Main.dust[dust].noGravity = true;
	}

public void HoldStyle(Player player)
	{
	x++;
	if (player.controlUseTile && player.statMana >= 100 && x > 60)
		{
		x=0;
		Main.PlaySound(2,-1,-1,30);
		player.statMana-=100;
		player.AddBuff("Ice Rain", 600, false);
		}
	}