public static void Effects(Player player)
{
    player.nightVision = true;
    Vector2 PC = player.position+new Vector2(player.width/2,player.height/2);
    Lighting.addLight((int)((PC.X) / 16f), (int)((PC.Y) / 16f), 5, 5, 5);  
}