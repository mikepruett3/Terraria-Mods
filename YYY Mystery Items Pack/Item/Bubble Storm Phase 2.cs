

public void UseStyle(Player player)
{
Vector2 vector = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
                        
                        float num27 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
                        float num28 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;

player.itemLocation.X = player.position.X + (float)player.width * 0.5f - (float)Main.itemTexture[player.inventory[player.selectedItem].type].Width * 0.5f - (float)(player.direction * 2);
player.itemLocation.Y = player.position.Y + (float)player.height * 0.5f - (float)Main.itemTexture[player.inventory[player.selectedItem].type].Height * 0.5f;
player.itemRotation = (float)Math.Atan2((double)(num28 * (float)player.direction), (double)(num27 * (float)player.direction));             

}
