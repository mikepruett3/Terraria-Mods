public void UseStyle(Player player)
{
    int rangeOffset = player.itemWidth/3;
    if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666)
    {
        player.itemLocation.X -= rangeOffset * (float)player.direction;
    }
    else
    {
        player.itemLocation.X += rangeOffset * (float)player.direction;
    }
}