
public void UseItem(Player player, int playerID) 
{

    if (playerID == Main.myPlayer)
	{
	Item MyCheckedItem = new Item();
	bool FoundUsableAmmo = false;
	bool FoundAmmoInAmmoSlot = false;
	for (int PassingIndex = 44; PassingIndex < 48; PassingIndex++)
	{
	    if (player.inventory[PassingIndex].ammo == item.useAmmo && player.inventory[PassingIndex].stack > 0)
	    {
	        MyCheckedItem = player.inventory[PassingIndex];
	        FoundUsableAmmo = true;
	        FoundAmmoInAmmoSlot = true;
	        break;
	    }
	}
	if (!FoundAmmoInAmmoSlot)
	{
	    for (int PasserIndex = 0; PasserIndex < 44; PasserIndex++)
	    {
	        if (player.inventory[PasserIndex].ammo == item.useAmmo && player.inventory[PasserIndex].stack > 0)
	        {
	            MyCheckedItem = player.inventory[PasserIndex];
	            FoundUsableAmmo = true;
	            break;
	        }
	    }
	}
	if (FoundUsableAmmo)
	{
	    bool DoNotConsumeAmmo = false;
	    if (player.ammoCost80 && Main.rand.Next(5) == 0)
	    {
	        DoNotConsumeAmmo = true;
	    }
	    if (player.ammoCost75 && Main.rand.Next(4) == 0)
	    {
	        DoNotConsumeAmmo = true;
	    }
	    if (!DoNotConsumeAmmo)
	    {
	            MyCheckedItem.stack--;
	    }
    }
    if(FoundUsableAmmo)
    {
	
	if ((player.direction == -1) && ((Main.mouseX + Main.screenPosition.X) > (player.position.X + player.width * 0.5f)))
	{
		player.direction = 1;
	}
	if ((player.direction == 1) && ((Main.mouseX + Main.screenPosition.X) < (player.position.X + player.width * 0.5f)))
	{
		player.direction = -1;
	}	

	if (player.direction == 1) 
    {
        player.itemRotation = (float) Math.Atan2((Main.mouseY + Main.screenPosition.Y)-(player.position.Y + player.height * 0.5f), (Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f));	
    }
    else 
    {
        player.itemRotation = (float) Math.Atan2((player.position.Y + player.height * 0.5f)-(Main.mouseY + Main.screenPosition.Y),(player.position.X + player.width * 0.5f)-(Main.mouseX + Main.screenPosition.X));
    }
    int ProjectileType = item.shoot; //this is the projectile your weapon shoots right now
    if(MyCheckedItem.shoot > 0) //if there's a special bullet projectile for your ammo
        ProjectileType = MyCheckedItem.shoot; //this will get the special projectile of the ammo.
    
    for (int AmountOfShots = 0; AmountOfShots < 3; AmountOfShots++) // 3is the bullets that come out , for example
    {

    int spread = 50;           
    float speedX = ((Main.mouseX + Main.screenPosition.X) - (player.position.X + player.width * 0.5f))+Main.rand.Next(-spread, spread);
    float speedY = ((Main.mouseY + Main.screenPosition.Y) - (player.position.Y + player.height * 0.5f))+Main.rand.Next(-spread, spread);
		 
    float ProjectileSpeed = 23f;
    float VelocitySize = (float) Math.Sqrt((double) ((speedX * speedX) + (speedY * speedY)));
	VelocitySize = ProjectileSpeed / VelocitySize;

	speedX *= VelocitySize;
	speedY *= VelocitySize;

	int MyProjectileIndex = Projectile.NewProjectile(
	(float) player.position.X + (player.width * 0.5f),
	(float) player.position.Y + (player.height * 0.5f),
	(float) speedX,
	(float) speedY,
	ProjectileType,
	(int) (item.damage*player.rangedDamage),
	player.inventory[player.selectedItem].knockBack,
	playerID
	);

    NetMessage.SendData(27, -1, -1, "", MyProjectileIndex, 0f, 0f, 0f, 0);

    }
    if (MyCheckedItem.stack <= 0) //lose ammo here
    {
        MyCheckedItem.active = false;
        MyCheckedItem.name = "";
        MyCheckedItem.type = 0;
    }
}
}
}