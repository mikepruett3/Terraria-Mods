public void Kill()
	{
	int proj = Config.projDefs.byName["Fire Staff Bomb Spirit"].type;
	int dmg = 145+Main.rand.Next(10);
	
	Main.PlaySound(2, (int)this.projectile.position.X, (int)this.projectile.position.Y, 14);
	for (int ir=0; ir<20; ir++)
		{
		Color color = default(Color);
		int dust = Dust.NewDust(new Vector2(this.projectile.position.X, this.projectile.position.Y), this.projectile.width, this.projectile.height, 31, 0f, 0f, 100, color, 1.5f);
		}
	
	
	
	//Projectiles

		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 0f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 0.3f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, -0.3f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 0.7f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, -0.7f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 1.4f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, -1.4f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 2.7f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, -2.7f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, 3.1f, -4f, proj, dmg, 0, Main.myPlayer);
		Projectile.NewProjectile(this.projectile.position.X+(this.projectile.width/2), this.projectile.position.Y, -3.1f, -4f, proj, dmg, 0, Main.myPlayer);
		
		
	
	this.projectile.active = false;
	}