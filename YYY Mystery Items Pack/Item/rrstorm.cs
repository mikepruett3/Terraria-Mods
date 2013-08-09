public void UseItem(Player player, int playerID)
{
	Projectile.NewProjectile(
    	(float) (Main.mouseX + Main.screenPosition.X)-100+Main.rand.Next(200),
    	(float) (Main.mouseY + Main.screenPosition.Y)-1200.0f,
    	(float) (-40+Main.rand.Next(80))/10,
    	35.9f,
    	"Blade Beam Phase 2", //Type
    	(int) (item.damage*player.magicDamage), //Damage
    	2.0f,
    	playerID
    	);
}