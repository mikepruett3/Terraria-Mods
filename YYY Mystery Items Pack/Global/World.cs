public static BoosterDust[] BoosterDustsArray = new BoosterDust[50];
public void PreDrawInterface(SpriteBatch sp)
{
    foreach(BoosterDust B in BoosterDustsArray)
    {
        if(B != null && B.alive)
            B.Update();
        if(B.alive)
            B.Draw(sp);
    }
}

public void Initialize()
{
	modIndex=Config.mods.IndexOf("YYY Mystery Items Pack");
    for(int i = 0; i < ModWorld.BoosterDustsArray.Length;i++)
    {
        ModWorld.BoosterDustsArray[i] = new BoosterDust();
    }
}

public class BoosterDust
{
    public Texture2D Tex;
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    public bool alive = false;
    public int timeLeft;
    public int Frame;
    public int FrameSizeX;
    public int FrameSizeY;
    public float Scale;
    public float Rotation;
    public int FrameCounter;

    public virtual void SetBoosterDust(string TheTexName, Vector2 Pos , Vector2 Vel , Vector2 Accel, int TheTimeLeft = -180,float rotation= 0f, int TheFrame = 1, int TheFrameSizeX = -1, int TheFrameSizeY = -1, float TheScale = 1f)
    {
        Tex = Main.goreTexture[Config.goreID[""+TheTexName]];
        Position = Pos;
        Velocity = Vel;
        Acceleration = Accel;
        timeLeft = TheTimeLeft;
        Rotation = rotation;
        Frame = TheFrame;
        FrameSizeX = TheFrameSizeX;
        FrameSizeY = TheFrameSizeY;
        Scale = TheScale;
        if(FrameSizeX == -1)
            FrameSizeX = Tex.Width;
        if(FrameSizeY == -1)
            FrameSizeY = Tex.Height;
    }


    public static int NewBoosterDust(string TN, Vector2 Pos,Vector2 V,Vector2 A, int TL = 180,float RT = 0f, int TF = 1, int TFX = -1, int TFY = -1, float TS = 1f)
    {   
        int index =-1;
        for(int i = 0; i < ModWorld.BoosterDustsArray.Length;i++)
        {
            BoosterDust Booster = ModWorld.BoosterDustsArray[i];
			if(Booster == null)
            {
                Booster = new BoosterDust();
            }
            if(!Booster.alive)
            {
                index = i;
                break;
            }
        }
        if(index == -1)
            return -1;
        ModWorld.BoosterDustsArray[index] = new BoosterDust();
        ModWorld.BoosterDustsArray[index].alive = true;
        ModWorld.BoosterDustsArray[index].SetBoosterDust(TN,Pos,V,A,TL,RT,TF,TFX,TFY,TS);
        return index;
    }


    public virtual void Update()
    {
        DoUpdate();
        KillConditions();
    }


    public virtual void DoUpdate()
    {
        float A_Pie = (float)(Math.PI);
        Velocity += Acceleration;
        Rotation = (float)Math.Atan2((double)Velocity.Y, (double)Velocity.X) + A_Pie/2;
        timeLeft--;
        FrameCounter++;
        if(FrameCounter >= 5)
        {
        FrameCounter = 0;
        Frame++;
        }
        if(Frame*FrameSizeY >= Tex.Height)
            Frame = 1;
    }


    public virtual void KillConditions()
    {
        if (timeLeft < 1)
            Kill();
    }


    public virtual void Kill()
    {
        alive = false;
    }


    public virtual void Draw(SpriteBatch sp)
    {

        float rotation = Rotation;

        float PosX = (float)(Position.X - Main.screenPosition.X);
        float PosY = (float)(Position.Y - Main.screenPosition.Y);
        Vector2 ScreenPositioning = new Vector2(PosX,PosY);

        int frameIndex = Frame-1;
        int frameX = 0;
        int frameY = (FrameSizeY+2)*frameIndex;

        Vector2 Center = new Vector2((float)FrameSizeX * 0.5f, (float)(FrameSizeY * 0.5f));
        
        Vector2 ForTheColors = Position+Center;
        ForTheColors = ForTheColors/16f;
        Color color = Lighting.GetColor((int)ForTheColors.X, (int)ForTheColors.Y);

        sp.Draw(
        Tex, 
        ScreenPositioning,
        new Rectangle(frameX, frameY, FrameSizeX, FrameSizeY),
        color, 
        rotation, 
        Center,
        Scale,
        SpriteEffects.None, 
        0f);







    //    float ballrot = 0f;
    //    Player drawPlayer = Main.player[Main.myPlayer];

    //    float thisx = (float)((int)(drawPlayer.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2)));
    //    float thisy = (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2)));
    //        SpriteEffects effects = SpriteEffects.None;
    //        if (drawPlayer.direction == -1)
    //        {
    //            effects = SpriteEffects.FlipHorizontally;
    //        }
    //    float ballrotoffset = 0f;
    //    if(drawPlayer.velocity.Y != Vector2.Zero.Y)
    //    {
    //        if(drawPlayer.velocity.X != 0f)
    //        ballrotoffset+= 0.05f*drawPlayer.velocity.X;
    //        else
    //        ballrotoffset += 0.25f*drawPlayer.direction;
    //    }
    //    else if (drawPlayer.velocity.X < 0f)
    //        ballrotoffset -= 0.2f;
    //    else if ( drawPlayer.velocity.X > 0f)
    //        ballrotoffset += 0.2f;
    //    if(drawPlayer.hitTile > 0)
    //    {
    //        if(drawPlayer.velocity.X != 0f)
    //        ballrotoffset+= 0.05f*drawPlayer.velocity.X;
    //        else
    //        ballrotoffset += 0.25f*drawPlayer.direction;
    //    }
    //    ballrot+=ballrotoffset;
    ////    spriteBatch.Draw(Main.teamTexture,thispos, new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[1], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
    //    Color brightColor = (drawPlayer.shirtColor.R+drawPlayer.shirtColor.G+drawPlayer.shirtColor.B >= drawPlayer.underShirtColor.R+drawPlayer.underShirtColor.G+drawPlayer.underShirtColor.B)?drawPlayer.shirtColor:drawPlayer.underShirtColor;
    //    Color darkColor = (drawPlayer.shirtColor.R+drawPlayer.shirtColor.G+drawPlayer.shirtColor.B < drawPlayer.underShirtColor.R+drawPlayer.underShirtColor.G+drawPlayer.underShirtColor.B)?drawPlayer.shirtColor:drawPlayer.underShirtColor;
    //    sp.Draw(Tex, thispos+ballDims/2, new Rectangle?(new Rectangle(0,((int)ballDims.Y+2)*timez,(int)ballDims.X, (int)ballDims.Y)), darkColor,ballrot,ballDims/2, 1.14f, effects, 0f);
        
    }
}


public static int modIndex=0;
public void NetReceive(int msg, BinaryReader reader) 
{
		int playerID = (int) reader.ReadByte();
		
	if(msg%10 == 1) 
    {
        
        msg /= 10;
        int ProjIndex = msg % 1000;
        msg /= 1000;
        int ArmyIndex = msg%10;
        msg /= 10;
        int WeaponType = msg % 10;
        
            if(WeaponType == 2)
                Main.projectile[ProjIndex].RunMethod("SetSpearInfo",ArmyIndex);
            if(WeaponType == 7)
                Main.projectile[ProjIndex].RunMethod("SetBladeInfo",ArmyIndex);
			NetMessage.SendModData(modIndex, msg, -1, -1, (byte)playerID);
	}
}
