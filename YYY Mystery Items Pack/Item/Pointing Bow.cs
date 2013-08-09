public static void UseItem(Player player, int playerID)
{
	int CurrentSlot = 0;
	int UsedAmmoSlot = 0;
	
	// get slot in the player's inventory of the ammo to use
	foreach(Item X in player.inventory)
	{
		if(X.ammo == 1 && X.stack > 0)
		{
			UsedAmmoSlot = CurrentSlot;
            break;
		}
		
		CurrentSlot++;
	}
	
	// no ammo found
	if(CurrentSlot == player.inventory.Length)
	{
		return;
	}
	
	float PlayerCentreX = player.position.X + player.width * 0.5f;
	float PlayerCentreY = player.position.Y + player.height * 0.5f;
	
	// create slave and manipulator projectiles
	int Slave = Projectile.NewProjectile(0.0f, 0.0f, 0.0f, 0.0f, player.inventory[UsedAmmoSlot].shoot, 20, 0.0f, Main.myPlayer);
	int Manip = Projectile.NewProjectile(0.0f, 0.0f, 0.0f, 0.0f, "Aim Manipulator", 20, 0.0f, Main.myPlayer);
	
	// set positions
	Main.projectile[Slave].position.X = PlayerCentreX - Main.projectile[Slave].width * 0.5f;
	Main.projectile[Slave].position.Y = PlayerCentreY - Main.projectile[Slave].height * 0.5f;
	Main.projectile[Manip].position.X = PlayerCentreX - Main.projectile[Manip].width * 0.5f;
	Main.projectile[Manip].position.Y = PlayerCentreY - Main.projectile[Manip].height * 0.5f;
	
	// set initial rotation of slave projectile
	// arrows start facing down instead of facing right
	// so rotate it to face the correct way
	Main.projectile[Slave].rotation = (float)Math.Atan2(Main.screenPosition.Y + Main.mouseY - Main.projectile[Manip].position.Y,
														Main.screenPosition.X + Main.mouseX - Main.projectile[Manip].position.X) +
											 (float)(Math.PI/2);
	
	// reduce the stack of the ammo used
	player.inventory[UsedAmmoSlot].stack--;
	
	// for use in the projectile cs
	Main.projectile[Manip].ai[0] = (float)Slave;
	Main.projectile[Manip].ai[1] = (float)Main.projectile[Slave].aiStyle;
	
	// arrows fall due to gravity, this mucks up our control
	// so set the aiStyle to give us full control
	// will be reset once the manipulator projectile dies
	Main.projectile[Slave].aiStyle = -1;
}