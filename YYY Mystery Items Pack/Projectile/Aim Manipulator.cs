// first time run
bool Initialised = false;
int Timer = 120;
// slave projectile ID
int Slave = 0;

// direction to travel
Microsoft.Xna.Framework.Vector2 Velocity;

// speed to travel
float Speed = 7.0f;

float _A_Pie = (float)(Math.PI*2);

float Half_A_Pie = (float)Math.PI;

float Quarter_A_Pie = (float)(Math.PI/2);

// first time run
public void Initialise()
{
	Initialised = true;
	
	Slave = (int)projectile.ai[0];
	
	projectile.tileCollide = Main.projectile[Slave].tileCollide;

    projectile.timeLeft = Timer;
}

public void AI()
{
	// do once
	if(!Initialised)
	{
		Initialise();
	}
	
	// do terraria's base projectile ai
	projectile.AI(true);
	
	if(!Main.projectile[Slave].active)
	{
		Kill();
		return;
	}
	
	SlaveAI();
	
	Rotate();
	
	// keep to a simple value
	if(Main.projectile[Slave].rotation > _A_Pie)
	{
		Main.projectile[Slave].rotation -= _A_Pie;
	}
	else if(Main.projectile[Slave].rotation < 0.0f)
	{
		Main.projectile[Slave].rotation += _A_Pie;
	}
	
	// move in direction the projectile is facing
	Velocity.X = (float)Math.Cos(Main.projectile[Slave].rotation - Quarter_A_Pie) * Speed;
	Velocity.Y = (float)Math.Sin(Main.projectile[Slave].rotation - Quarter_A_Pie) * Speed;
	
	projectile.position += Velocity;
	
	Main.projectile[Slave].position = projectile.position;
}

public void Kill()
{
	// reset the projectile's ai style and set continuing velocity
	projectile.active = false;
	Main.projectile[Slave].aiStyle = (int)projectile.ai[1];
	Main.projectile[Slave].velocity = Velocity;
}

// To kill, or not to kill
public void SlaveAI()
{
	if(Main.projectile[Slave].tileCollide)
	{
		int ColX = (int)(projectile.position.X + projectile.width * 0.5f) / 16;
		int ColY = (int)(projectile.position.Y + projectile.height * 0.5f) / 16;
		
		if (Main.tile[ColX, ColY] != null)
		{
			if (Main.tile[ColX, ColY].active && Main.tileSolid[Main.tile[ColX, ColY].type] &&
				!Main.tileSolidTop[Main.tile[ColX, ColY].type])
			{
				Main.projectile[Slave].Kill();
				Kill();
			}
		}
	}
	
	if(!Main.projectile[Slave].ignoreWater)
	{
		if(Main.projectile[Slave].wet)
		{
			Main.projectile[Slave].Kill();
			Kill();
		}
	}
	
	if(Main.projectile[Slave].penetrate > 1)
	{
		Collision.HitTiles(Main.projectile[Slave].position, Velocity,
						   Main.projectile[Slave].width, Main.projectile[Slave].height);
		
		Main.PlaySound(2, (int)Main.projectile[Slave].position.X, (int)Main.projectile[Slave].position.Y, 10);
		Main.projectile[Slave].penetrate--;
	}
	// else
	// {
		// Main.projectile[Slave].Kill();
		// Kill();
	// }
	
	if (Main.projectile[Slave].timeLeft <= 0)
	{
		Main.projectile[Slave].Kill();
		Kill();
	}
	
	if (Main.player[projectile.owner].dead)
	{
		Main.projectile[Slave].Kill();
		Kill();
	}
	
	if (projectile.lavaWet)
	{
		Main.projectile[Slave].Kill();
		Kill();
	}
}

// rotates towards the mouse, turning in the shortest direction
public void Rotate()
{
	float Time = 1.0f / Main.frameRate;
	
	// angle from projectile to mouse
	float Angle = (float)Math.Atan2(Main.screenPosition.Y + Main.mouseY - projectile.position.Y,
									Main.screenPosition.X + Main.mouseX - projectile.position.X);
	
	// arrows start facing down, instead of facing right
	float DiffRot = Main.projectile[Slave].rotation - Quarter_A_Pie - Angle;
	
	// get value between -PI and PI
	if(DiffRot > Half_A_Pie)
	{
		DiffRot -= _A_Pie;
	}
	else if(DiffRot < -Half_A_Pie)
	{
		DiffRot += _A_Pie;
	}
	
	// one full _A_Pie turned per second
	if(DiffRot > Time * _A_Pie)
	{
		if(DiffRot > 0.0f)
		{
			// Anti-Clockwise
			Main.projectile[Slave].rotation -= Time * _A_Pie;
		}
		else
		{
			// Clockwise
			Main.projectile[Slave].rotation += Time * _A_Pie;
		}
	}
	else
	{
		// close enough to intended rotation
		Main.projectile[Slave].rotation = Angle + Quarter_A_Pie;
	}
}