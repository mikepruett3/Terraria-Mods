public static void Effects(Player player)
{
	player.rocketBoots = 2;
    player.rocketTime = 2;
    player.gravControl = true;
    player.jumpBoost = true;
    player.noFallDmg = true;
    player.waterWalk = true;
    player.doubleJump = true;
    player.moveSpeed += 5f;
    if(player.whoAmi == Main.myPlayer)
    {
        if (player.controlLeft && player.velocity.X > -10)
            player.velocity.X -= 0.2f;
        if (player.controlRight && player.velocity.X < 10)
            player.velocity.X += 0.2f;
    }
}
