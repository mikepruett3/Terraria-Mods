public void SetStyle(Player player, Item item)
{
        player.itemRotation += 0.25f*player.direction*player.gravDir;
        if(player.itemRotation<0)
            player.itemRotation+=(float)(Math.PI*2);
		float p = (float)Math.PI;
		double xa = ((double)Math.Abs(player.itemRotation));
		double xd = Math.Cos(xa);
		double yd = Math.Sin(xa);
		float cx=-item.width/2 * player.direction; 
		float cy=item.height/2 * player.gravDir;
		float newx= (float)(cx*xd - cy*yd);
		float newy=(float)(cx*yd + cy*xd);
		player.itemLocation.X = player.position.X + (float)player.width * 0.5f + (8f * (float)player.direction) + newx;
		player.itemLocation.Y = player.position.Y + 20f + newy;
}


public void SetFrame(Player player, Item item)
{
			player.bodyFrame.Width = 40;
			player.bodyFrame.Height = 56;
			player.bodyFrame.X = 0;
			player.bodyFrame.Y = player.bodyFrame.Height * 3;
}


public Rectangle UpdateHitBox(Player player, Item item, Rectangle rectangle)
{
 int x=(int)(player.position.X + (float)player.width * 0.5f - item.width/2f + (8f * (float)player.direction));
 int y= (int)(player.position.Y + 20f - item.height/2);
 rectangle = new Rectangle(x, y, item.width, item.height);
 return rectangle;
}