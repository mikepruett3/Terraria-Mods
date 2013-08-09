int x=0;


public static void UseItemEffect(Player player, Rectangle rectangle) 
	{
    Color color = new Color();
    int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 3, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 100, color, 1.9f);
    Main.dust[dust].noGravity = true;
	}

public void HoldStyle(Player player)
	{
	x++;
	if (player.controlUseTile && player.statMana > 40 && x > 60)
		{
		player.statLife+=75;
		Main.PlaySound(2,-1,-1,29);
		player.statMana-=60;
		x=0;
		}
	}