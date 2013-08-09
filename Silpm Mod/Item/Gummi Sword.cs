public static void UseItemEffect(Player player, Rectangle rectangle) 
{
    Color color = new Color();
    int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 57, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 100, color, 1.9f);
    Main.dust[dust].noGravity = true;
}