
public void Effects(Player player) 
	{
	Projectile.NewProjectile(player.position.X-150,player.position.Y-950, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X-100,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X-50,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X+50,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X-100,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	Projectile.NewProjectile(player.position.X-150,player.position.Y-550, 2, Main.rand.Next(10)+5, Config.projDefs.byName["Ice Staff Spirit"].type, Main.rand.Next(15)+6, 0, Main.myPlayer);
	}