public static void Effects(Player player)
{
	player.lavaImmune = true;
	player.breath = player.breathMax;
    if(player.wet)
        player.gills = true;
    player.breathCD=0;
    player.fireWalk = true;
}
