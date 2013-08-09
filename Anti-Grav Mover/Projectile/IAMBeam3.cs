public const float MAX_SPEED = 250.0f;
public const float MAX_NPC_SPEED = 500.0f;
public const float SPEED_DECAY = 0.97f;
public const float MAX_DIST = 800.0f;
public const float MIN_DIST = 400.0f;
public const float WEAPON_OFFSET_X = -40.0f;
public const float WEAPON_OFFSET_Y = -40.0f;
public const float NPC_SPEED_DECAY = 0.98f;
public const int MODE_NONE = -1, MODE_GROUND = 0, MODE_PLAYER = 1, MODE_NPC = 2;

public Player owner;
public NPC target;
public Player ptarget;
public bool isOwner;
public float oDist;
private float aRot;
private long lastCheck;
private float cx, cy, px, py, ox, oy, dcx, dcy, ncx, ncy, dpx, dpy, cdist, pdist, ang, nx, ny, tox, toy;
private float mouseX, mouseY;
private float halfWeaponHeight, halfWeaponWidth, halfPlayerHeight, halfPlayerWidth;
private int mode;
private int cooldown = 0;
private int damage = 2;
private float pulse = 0.0f;
private bool pulsing = false;

//Init
public void Initialize() {
	owner = Main.player[projectile.owner];
	isOwner = (projectile.owner == Main.myPlayer);
	
	if (isOwner) {
		oDist = 0.0f;
		mode = MODE_NONE;
		float projSpeed = (float)Math.Sqrt(projectile.velocity.X*projectile.velocity.X + projectile.velocity.Y*projectile.velocity.Y);
		if (projSpeed < 8.0f) {
			projSpeed = 8.0f;
		}
		while ((mode == MODE_NONE)&&((oDist+=projSpeed)<MAX_DIST)) {
			mode = hitTest();
			projectile.position.X += projectile.velocity.X;
			projectile.position.Y += projectile.velocity.Y;
		}
		
		if (mode == MODE_NONE) {
			KillAndSend("Hit test found nothing."); return;
		}
		if (oDist > MAX_DIST) {
			KillAndSend("Beyond max range."); return;
		}
		if (oDist < MIN_DIST)
			oDist = MIN_DIST;
			
		SendData();
		SendActivateData();
	}
	aRot = 0.0f;
	projectile.velocity.X = 0.0f;
	projectile.velocity.Y = 0.0f;
	halfWeaponWidth =  23.0f;
	halfWeaponHeight = 11.0f;
	halfPlayerWidth = owner.width / 2.0f;
	halfPlayerHeight = owner.height / 2.0f;
}
private int hitTest() {
	int projX = (int)(projectile.position.X/16);
	int projY = (int)(projectile.position.Y/16);
	if ((projX>0)&&(projY>0)&&(projX<Main.maxTilesX)&&(projY<Main.maxTilesY)) {
		if (Main.netMode != 0) {
			for (int i = 0; i < Main.player.Length; i++) {
				if (i!=projectile.owner) {
					Player n = Main.player[i];
					float tx = projectile.position.X - n.position.X;
					float ty = projectile.position.Y - n.position.Y;
					if ((tx > 0) && (ty > 0) && (tx < n.width) && (ty < n.height) && n.active) {
						ptarget = n;
						return MODE_PLAYER;
					}
				}
			}
		}
		for (int i = 0; i < Main.npc.Length; i++) {
			NPC n = Main.npc[i];
			float tx = projectile.position.X - n.position.X;
			float ty = projectile.position.Y - n.position.Y;
			if ((tx > 0) && (ty > 0) && (tx < n.width) && (ty < n.height) && n.active) {
				target = n;
				return MODE_NPC;
			}
		}
		if (Main.tile[projX, projY]!=null)
			return (Main.tileSolid[(int)Main.tile[projX, projY].type]&&Main.tile[projX, projY].active)?MODE_GROUND:MODE_NONE;
		else
			return MODE_NONE;
	}
	return MODE_GROUND;
}

//Data updates -- This need to be turned into one function.
//                But for debugging reasons, it's easier to
//                keep it at 3 until I finish the visuals.
private void updatePlayerPoints() {
	cx = getCursorX();
	cy = getCursorY();
	px = ptarget.position.X + halfPlayerWidth;
	py = ptarget.position.Y + halfPlayerHeight;
	ox = owner.position.X + halfPlayerWidth;
	oy = owner.position.Y + halfPlayerHeight;
	
	dcx = cx - ox;
	dcy = cy - oy;
	dpx = px - ox;
	dpy = py - oy;
	
	cdist = (float)Math.Sqrt(dcx * dcx + dcy * dcy);
	pdist = (float)Math.Sqrt(dpx * dpx + dpy * dpy);
	
	ang = (float)Math.Atan2(cy - oy, cx - ox);// + 3.141592654f;
	
	nx = (float)Math.Cos(ang)*oDist + ox;
	ny = (float)Math.Sin(ang)*oDist + oy;
	
	tox = (float)Math.Cos(ang+3.1415f)*WEAPON_OFFSET_X;
	toy = (float)Math.Sin(ang+3.1415f)*WEAPON_OFFSET_Y;
	
	ncx = (dcx/cdist)*pdist;
	ncy = (dcy/cdist)*pdist;
}
private void updatePoints() {
	cx = getCursorX();
	cy = getCursorY();
	px = projectile.position.X;
	py = projectile.position.Y;
	ox = owner.position.X + halfPlayerWidth;
	oy = owner.position.Y + halfPlayerHeight;
	
	dcx = cx - ox;
	dcy = cy - oy;
	dpx = px - ox;
	dpy = py - oy;
	
	cdist = (float)Math.Sqrt(dcx * dcx + dcy * dcy);
	pdist = (float)Math.Sqrt(dpx * dpx + dpy * dpy);
	
	ang = (float)Math.Atan2(cy - oy, cx - ox) + 3.141592654f;
	
	nx = (float)Math.Cos(ang)*oDist + px;
	ny = (float)Math.Sin(ang)*oDist + py;
	
	tox = (float)Math.Cos(ang)*WEAPON_OFFSET_X;
	toy = (float)Math.Sin(ang)*WEAPON_OFFSET_Y;
	
	ncx = (dcx/cdist)*pdist;
	ncy = (dcy/cdist)*pdist;
}
private void updateAltPoints() {
	cx = getCursorX();
	cy = getCursorY();
	px = target.position.X + target.width/2.0f;
	py = target.position.Y + target.height/2.0f;
	ox = owner.position.X + halfPlayerWidth;
	oy = owner.position.Y + halfPlayerHeight;
	
	dcx = cx - ox;
	dcy = cy - oy;
	dpx = px - ox;
	dpy = py - oy;
	
	cdist = (float)Math.Sqrt(dcx * dcx + dcy * dcy);
	pdist = (float)Math.Sqrt(dpx * dpx + dpy * dpy);
	
	ang = (float)Math.Atan2(cy - oy, cx - ox);// + 3.141592654f;
	
	nx = (float)Math.Cos(ang)*oDist + ox;
	ny = (float)Math.Sin(ang)*oDist + oy;
	
	tox = (float)Math.Cos(ang+3.1415f)*WEAPON_OFFSET_X;
	toy = (float)Math.Sin(ang+3.1415f)*WEAPON_OFFSET_Y;
	
	ncx = (dcx/cdist)*pdist;
	ncy = (dcy/cdist)*pdist;
}

//Networking
public void SendData() {
	float x = (float)(Main.mouseX + Main.screenPosition.X);
	float y = (float)(Main.mouseY + Main.screenPosition.Y);
	bool mdown = Main.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
	ModWorld.playerCursor[Main.myPlayer].X = x;
	ModWorld.playerCursor[Main.myPlayer].Y = y;
	ModWorld.playerMouseDown[Main.myPlayer] = mdown;
	NetMessage.SendModData(ModWorld.modIndex, ModWorld.NET_CURSOR_DATA, -1, -1,
							(byte)Main.myPlayer, x, y, mdown);
}
private void setActive(bool isActive) {
	ModWorld.iamActive[projectile.owner] = isActive;
	NetMessage.SendModData(ModWorld.modIndex, isActive?ModWorld.NET_ACTIVATE_IAM:ModWorld.NET_DEACTIVATE_IAM, -1, -1, (byte)Main.myPlayer);
}
public void SendActivateData() {
	switch (mode) {
		case MODE_GROUND:
			NetMessage.SendModData(ModWorld.modIndex, ModWorld.NET_IAM_ACTIVATION_DATA, -1, -1,
						   (byte)Main.myPlayer, oDist, mode, projectile.position.X, projectile.position.Y);
			break;
		case MODE_PLAYER:
		case MODE_NPC:
			NetMessage.SendModData(ModWorld.modIndex, ModWorld.NET_IAM_ACTIVATION_DATA, -1, -1,
						   (byte)Main.myPlayer, oDist, mode, (float)((mode==MODE_PLAYER)?ptarget.whoAmi:target.whoAmI),0.0f);
			break;
	}
	setActive(true);
}


//Drawing:
private float getScale(float x) {
	return -((x-1.0f)*(x-1.0f))+ 1.25f;
}
private void drawBit(SpriteBatch s, float x, float y, float ang = 0.0f, float scale = 1.0f, float fade = 1.0f) {
	float ts = scale - 0.25f;
	ts = 1.0f - ts;
	ts /= 2.0f;
	ts += 0.5f;
	Texture2D t = Main.projectileTexture[projectile.type];
	s.Draw(t, new Vector2(x - Main.screenPosition.X , y -Main.screenPosition.Y), 
							 new Rectangle?(new Rectangle(0, 0, t.Width, t.Height)),
							 new Color((int)(150.0f*ts), 0, 0), ang, new Vector2(t.Width >> 1, t.Height >> 1) , scale, SpriteEffects.None, 0f);
}
private float getScale2(float x, float d) {
	//-((x-0.5)*4)^2+1.5
	return (float)Math.Max(0.0f, -1.0f*((x-d)*(x-d)*20.0f) + 1.0f);
}
public void PostDraw(SpriteBatch s) {
	aRot -= 0.05f;
	float cap = 100.0f;
	
	float dx, dy, prec, iprec;

	for (float i = 0.0f; i < cap; i+= 1.0f) {
		prec = i/cap;
		iprec = 1.0f - prec;
		
		dx = (dpx*prec)*prec + (ncx*prec + tox)*iprec + ox;
		dy = (dpy*prec)*prec + (ncy*prec + toy)*iprec + oy;
		drawBit(s, dx, dy, aRot + prec*8.0f, getScale(prec*2.0f) + ((pulsing)?getScale2(prec,pulse):0.0f), (float)Math.Sin((aRot*3.0f+prec*4.0f))/4.0f + 0.75f);
	}
}

//AI Tick:
public void AI() {
	projectile.velocity.X = 0.0f;
	projectile.velocity.Y = 0.0f;
	if (cooldown > 0)
		cooldown--;
	if (!ModWorld.iamActive[projectile.owner])
		return;
	if (ModWorld.iamData[projectile.owner]!=default(Vector4)) {
		oDist = ModWorld.iamData[projectile.owner].X;
		mode = (int)ModWorld.iamData[projectile.owner].Y;
		if (mode == MODE_PLAYER)
			ptarget = Main.player[(int)ModWorld.iamData[projectile.owner].Z];
		else if (mode == MODE_NPC)
			target = Main.npc[(int)ModWorld.iamData[projectile.owner].Z];
		else {
			projectile.position.X = ModWorld.iamData[projectile.owner].Z;
			projectile.position.Y = ModWorld.iamData[projectile.owner].W;
		}
			
		ModWorld.iamData[projectile.owner] = default(Vector4);
	}
	
	if (Main.netMode!=2) {
		owner.itemAnimation = 5;
		owner.itemTime = 10;
	}
	if (isOwner&&(Main.netMode!=2))
		SendData();
	float speed = 0f;
	switch (mode) {
		case MODE_NPC:
			if (isOwner && getRightMouseDown() && (!pulsing) && (!target.friendly)) {
				pulsing = true;
				pulse = 0.0f;
				NetMessage.SendModData(ModWorld.modIndex, ModWorld.NET_IAM_PULSE, -1, -1,(byte)Main.myPlayer);
			} else if (ModWorld.pulsing[projectile.owner]&&!isOwner) {
				if (pulsing)
					PulseComplete();
				ModWorld.pulsing[projectile.owner] = false;
				pulsing = true;
				pulse = 0.0f;
			}
			if (target.active == false)
				KillAndSend("Target dead");
			updateAltPoints();
			target.velocity.X += (nx - px)/300.0f;
			target.velocity.Y += (ny - py)/300.0f;
			
			speed = (float) Math.Sqrt( target.velocity.X * target.velocity.X + target.velocity.Y * target.velocity.Y);
			if (speed > MAX_NPC_SPEED) {
				target.velocity.X = (target.velocity.X/speed)*MAX_NPC_SPEED;
				target.velocity.Y = (target.velocity.Y/speed)*MAX_NPC_SPEED;
			}
			target.velocity.X *= NPC_SPEED_DECAY;
			target.velocity.Y *= NPC_SPEED_DECAY;
			break;
		case MODE_PLAYER:
			 if (ModWorld.pulsing[projectile.owner]&&!isOwner) {
				if (pulsing)
					PulseComplete();
				ModWorld.pulsing[projectile.owner] = false;
				pulsing = true;
				pulse = 0.0f;
			}
			if (Main.netMode != 2) {
				if (isOwner && getRightMouseDown() && (!pulsing)) {
					pulsing = true;
					pulse = 0.0f;
					NetMessage.SendModData(ModWorld.modIndex, ModWorld.NET_IAM_PULSE, -1, -1,(byte)Main.myPlayer);
				}
				updatePlayerPoints();
				ptarget.velocity.X += (nx - px)/300.0f;
				ptarget.velocity.Y += (ny - py)/300.0f;
				
				speed = (float) Math.Sqrt( ptarget.velocity.X * ptarget.velocity.X + ptarget.velocity.Y * ptarget.velocity.Y);
				if (speed > MAX_SPEED) {
					ptarget.velocity.X = (ptarget.velocity.X/speed)*MAX_SPEED;
					ptarget.velocity.Y = (ptarget.velocity.Y/speed)*MAX_SPEED;
				}
				ptarget.velocity.X *= SPEED_DECAY;
				ptarget.velocity.Y *= SPEED_DECAY;
			}
			break;
		case MODE_GROUND:
			if (Main.netMode != 2) {
				updatePoints();
				owner.velocity.X += (nx - ox)/400.0f;
				owner.velocity.Y += (ny - oy)/400.0f;
				
				speed = (float) Math.Sqrt( owner.velocity.X * owner.velocity.X + owner.velocity.Y * owner.velocity.Y);
				if (speed > MAX_SPEED) {
					owner.velocity.X = (owner.velocity.X/speed)*MAX_SPEED;
					owner.velocity.Y = (owner.velocity.Y/speed)*MAX_SPEED;
				}
				owner.velocity.X *= SPEED_DECAY;
				owner.velocity.Y *= SPEED_DECAY;
			}
			break;
	}
	if (pulsing) {
		pulse += 0.04f;
		if (pulse >= 1.0f) {
			PulseComplete();
		}
	}
	if (!getMouseDown())
		Kill();
}
private void PulseComplete() {
	pulsing = false;
	damage = Math.Min(damage << 1,100000028);
	switch (mode) {
		case MODE_NPC:
			target.StrikeNPC(damage, 0.0f, 1);
			break;
		case MODE_PLAYER:
			if (owner.hostile && ptarget.hostile && (owner.team != ptarget.team)) {
				ptarget.Hurt(damage, 0, true, false, "was destroyed by a W.A.M.");
			} else {
				damage = Math.Min(damage,100000028);
				if (ptarget != Main.player[Main.myPlayer])
					ptarget.HealEffect(damage);
				ptarget.statLife += damage;
			}
		break;
	}
}

//Getters:
private bool getRightMouseDown() {
	return Main.mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
}
private bool getMouseDown() {
	if (Main.netMode == 0)
		return Main.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
	else
		return ModWorld.playerMouseDown[projectile.owner];
}
private float getCursorX() {
	if (Main.netMode == 0)
		return (float)Main.mouseX + Main.screenPosition.X;
	else
		return ModWorld.playerCursor[projectile.owner].X;
	
}
private float getCursorY() {
	if (Main.netMode == 0)
		return (float)Main.mouseY + Main.screenPosition.Y;
	else
		return ModWorld.playerCursor[projectile.owner].Y;
}

//Kill code:
public void KillAndSend(String s) {
	Console.WriteLine(s);
	Kill();
}
public void Kill() {
	if (isOwner)
		setActive(false);
	projectile.active = false;
	//ModWorld.iamActive[projectile.owner] = false;
}