public const int BladesMax = 7;

public static int[,] BladeTargetIndex=new int[Main.player.Length,BladesMax];
public static bool[,] BladePlayerOrNPC=new bool[Main.player.Length,BladesMax];

float ProjSpeed = 10f;
float speed = 0f;
int LockPhase = 0;
Vector2 LockTargetPoint;
Vector2 LockTargetStart;

int BladeIndex = -1;
int BladeDamage = 10;

Vector2 BladePosOffset;
float offsetRot=0;

int Phase = 0;

float _A_Pie = (float)Math.PI*2;
float Half_A_Pie = (float)Math.PI;
float Quarter_A_Pie = (float)Math.PI/2;

public void Initialize()
{
    int index = 0;
    foreach (Projectile P in Main.projectile)
	{
		 if(P.active && P.type == projectile.type && P.whoAmI != projectile.whoAmI && P.owner == projectile.owner)
            index++;
	}
    SetBladeInfo(index+1);
}

public void AI()
{
    projectile.timeLeft =3;
    Lighting.addLight((int)((projectile.position.X + (float)(projectile.width / 2)) / 16f), (int)((projectile.position.Y + (float)(projectile.height / 2)) / 16f), 1f, 1f, 1f);  


    Player Ow = Main.player[projectile.owner];
    int OwIndex = projectile.owner;
    Vector2 OwCenter = Ow.position + new Vector2(Ow.width,Ow.height/2);

    Vector2 ProjCenter = projectile.position+new Vector2(projectile.width/2,projectile.height/2);

    #region Phase 0 - Detective Friend
    if(Phase == 0)
    {
    projectile.damage = 0;
    bool Offensive = false;


    BladePlayerOrNPC[OwIndex,BladeIndex] = false; // false is player , true is npc
    BladeTargetIndex[OwIndex,BladeIndex] = -1;
    int[] InvalidArr = new int[BladesMax];
    for(int Index = 0; Index < InvalidArr.Length; InvalidArr[Index] = BladeTargetIndex[OwIndex,Index] ,Index++ );
    int TarNPC = FindClosestNPC(ProjCenter,false,true,InvalidArr,true,ProjCenter);
    int TarPLAYER = FindClosestPlayer(ProjCenter,true,false,true);


    if(TarPLAYER > -1 || TarNPC > -1)
        Offensive = true;

    if(projectile.ai[1] > 0f)
        Offensive = false;
    if(projectile.ai[1] > 0f)
        projectile.ai[1]--;
    if(Offensive)
    {
        Vector2 NPC_Center = Vector2.Zero;
        Vector2 Player_Center = Vector2.Zero;
        if(TarPLAYER > -1 && TarNPC > -1)
        {
            NPC NPCTar = Main.npc[TarNPC];
            Player PlayerTar = Main.player[TarPLAYER];

            NPC_Center = NPCTar.position+new Vector2(NPCTar.width/2,NPCTar.height/2);
            Player_Center = PlayerTar.position+new Vector2(PlayerTar.width/2,PlayerTar.height/2);

            if(Vector2.Distance(NPC_Center,ProjCenter) < Vector2.Distance(Player_Center,ProjCenter))
                BladePlayerOrNPC[OwIndex,BladeIndex] = true;
        }
        else if(TarPLAYER > -1)
        {
            Player PlayerTar = Main.player[TarPLAYER];
            Player_Center = PlayerTar.position+new Vector2(PlayerTar.width/2,PlayerTar.height/2);
            BladePlayerOrNPC[OwIndex,BladeIndex] = true;
        }
        else
        {
            NPC NPCTar = Main.npc[TarNPC];
            NPC_Center = NPCTar.position+new Vector2(NPCTar.width/2,NPCTar.height/2);
        }



        float DistanceThreshold = -1f;
        if(BladePlayerOrNPC[OwIndex,BladeIndex])
            DistanceThreshold = Vector2.Distance(Player_Center,ProjCenter);
        else
            DistanceThreshold = Vector2.Distance(NPC_Center,ProjCenter);

        if(DistanceThreshold < 300f)
        {
            if(BladePlayerOrNPC[OwIndex,BladeIndex])
            {
                BladeTargetIndex[OwIndex,BladeIndex] = TarPLAYER;
                LockPhase = 0;
                LockTargetPoint = Player_Center;
            }
            else
            {
                BladeTargetIndex[OwIndex,BladeIndex] = TarNPC;
                LockPhase = 0;
                LockTargetPoint = NPC_Center;
            }
            projectile.rotation = (float)Math.Atan2(LockTargetPoint.Y  - ProjCenter.Y,LockTargetPoint.X  - ProjCenter.X);
            projectile.rotation -= Quarter_A_Pie;
            LockTargetStart = new Vector2(ProjCenter.X,ProjCenter.Y);
            Phase = 1;
        }
        else Offensive = false;  
    }
    if(!Offensive)                                                //NOT OFFENSIVE!
    {   
        Vector2 RealTarget = OwCenter + BladePosOffset*new Vector2(Ow.direction,Ow.gravDir);
        float RangeToOwner = Vector2.Distance(ProjCenter, RealTarget);
        if (RangeToOwner > 300)
        {
            speed = MathHelper.Clamp(speed + 0.1f, 0, ProjSpeed);
        }

        else if (RangeToOwner <= 300 && RangeToOwner > 0)
        {
            speed = MathHelper.SmoothStep(0,ProjSpeed, RangeToOwner/150);
        }
        else
        {
            speed = 0;
        }
        
	    float Angle = (float)Math.Atan2(RealTarget.Y  - ProjCenter.Y,RealTarget.X  - ProjCenter.X);
        projectile.velocity.X = (float)Math.Cos(Angle) * speed;
        projectile.velocity.Y = (float)Math.Sin(Angle) * speed;
        if(projectile.velocity.X < 0)
            projectile.spriteDirection = -1;
        else
            projectile.spriteDirection = 1;
        float HeightDiff = RealTarget.Y - ProjCenter.Y;
        float WidthDiff = RealTarget.X - ProjCenter.X;

        float theta = (float)Math.Atan((double)HeightDiff / (double)WidthDiff);
        theta+= Quarter_A_Pie;
        if(RangeToOwner < 100f)
        {
        theta = offsetRot*Ow.direction+Half_A_Pie;
        if(Ow.gravDir < 0)
            theta = -theta +Half_A_Pie;
        }
        else if (ProjCenter.X > OwCenter.X)
        {
            theta = MathHelper.WrapAngle(theta + Half_A_Pie);
        }
        float DifferenceMax = 0.2f;
        projectile.rotation += MathHelper.Clamp(MathHelper.WrapAngle(theta - projectile.rotation), -DifferenceMax, DifferenceMax);
    }



    }
    #endregion
    #region Phase 1 - Attack 
    if(Phase == 1)
    {
        projectile.damage = BladeDamage;
        projectile.ai[0]++;
        if(LockPhase == 0)
        {
            if(BladePlayerOrNPC[OwIndex,BladeIndex])
            {
                Player P = Main.player[BladeTargetIndex[OwIndex,BladeIndex]];
                if(!P.dead)
                {
                    LockTargetPoint = P.position+new Vector2(P.width/2,P.height/2);
                }
            }
            else
            {
                NPC N = Main.npc[BladeTargetIndex[OwIndex,BladeIndex]];
                if(N.active)
                {
                    LockTargetPoint = N.position+new Vector2(N.width/2,N.height/2);
                }
            }
            float Rotator = 0.1f;
            float Amt = 0f;
            while(Math.Abs(Amt*Rotator) < Half_A_Pie)
                Amt++;
            float EnderDistance = Vector2.Distance(ProjCenter,LockTargetPoint);
            Vector2 CenterPoint = (LockTargetPoint+LockTargetStart)/2;
            if(EnderDistance < 10f || projectile.ai[0] > Amt)
            {
                projectile.ai[0] = 0f;
                LockTargetPoint+=LockTargetPoint-CenterPoint;
                LockTargetStart = new Vector2(ProjCenter.X,ProjCenter.Y);
                LockPhase = 1;
                return;
            }
            float Angle = (float)Math.Atan2(ProjCenter.Y-CenterPoint.Y,ProjCenter.X-CenterPoint.X);
            projectile.position  = RotateAboutOrigin(ProjCenter,CenterPoint,Rotator);
            projectile.position -= new Vector2(projectile.width/2,projectile.height/2);
            projectile.rotation = Angle+Half_A_Pie;
            projectile.rotation *= (Rotator/Math.Abs(Rotator));
        }
        if(LockPhase == 1)
        {
            float Rotator = -0.15f;
            float Amt = 0f;
            while(Math.Abs(Amt*Rotator) < Half_A_Pie)
                Amt++;
            float EnderDistance = Vector2.Distance(ProjCenter,LockTargetPoint);
            Vector2 CenterPoint = (LockTargetPoint+LockTargetStart)/2;
            if(EnderDistance < 10f || projectile.ai[0] > Amt)
            {
                projectile.ai[0] = 0f;
                bool Re_Set = false;
                
                if(BladePlayerOrNPC[OwIndex,BladeIndex])
                {
                    Player P = Main.player[BladeTargetIndex[OwIndex,BladeIndex]];
                    if(!P.dead)
                    {
                        LockTargetPoint = P.position+new Vector2(P.width/2,P.height/2);
                        Re_Set = true;
                    }
                }
                else
                {
                    NPC  N = Main.npc[BladeTargetIndex[OwIndex,BladeIndex]];
                    if(N.active)
                    {
                        LockTargetPoint = N.position+new Vector2(N.width/2,N.height/2);
                        Re_Set = true;
                    }
                }
                if(!Re_Set)
                    LockTargetPoint+=LockTargetStart-LockTargetPoint;
                LockTargetStart = new Vector2(ProjCenter.X,ProjCenter.Y);
                //Phase = 0;
                LockPhase = 2;
                return;
            }
            float Angle = (float)Math.Atan2(ProjCenter.Y-CenterPoint.Y,ProjCenter.X-CenterPoint.X);
            projectile.position  = RotateAboutOrigin(ProjCenter,CenterPoint,Rotator);
            projectile.position -= new Vector2(projectile.width/2,projectile.height/2);
            projectile.rotation = Angle+Half_A_Pie;
            projectile.rotation *= (Rotator/Math.Abs(Rotator));
        }
        if(LockPhase == 2)
        {
            if(BladePlayerOrNPC[OwIndex,BladeIndex])
            {
                Player P = Main.player[BladeTargetIndex[OwIndex,BladeIndex]];
                if(!P.dead)
                {
                    LockTargetPoint = P.position+new Vector2(P.width/2,P.height/2);
                }
            }
            else
            {
                NPC N = Main.npc[BladeTargetIndex[OwIndex,BladeIndex]];
                if(N.active)
                {
                    LockTargetPoint = N.position+new Vector2(N.width/2,N.height/2);
                }
            }
            float Rotator = -0.2f;
            float Amt = 0f;
            while(Math.Abs(Amt*Rotator) < Half_A_Pie)
                Amt++;
            float EnderDistance = Vector2.Distance(ProjCenter,LockTargetPoint);
            Vector2 CenterPoint = (LockTargetPoint+LockTargetStart)/2;
            if(EnderDistance < 10f || projectile.ai[0] > Amt)
            {
                projectile.ai[0] = 0f;
                LockTargetPoint = OwCenter;
                LockTargetStart = new Vector2(ProjCenter.X,ProjCenter.Y);
                //Phase = 0;
                LockPhase = 3;
                return;
            }
            float Angle = (float)Math.Atan2(ProjCenter.Y-CenterPoint.Y,ProjCenter.X-CenterPoint.X);
            projectile.position  = RotateAboutOrigin(ProjCenter,CenterPoint,Rotator);
            projectile.position -= new Vector2(projectile.width/2,projectile.height/2);
            projectile.rotation = Angle+Half_A_Pie;
            projectile.rotation *= (Rotator/Math.Abs(Rotator));
        }
        if(LockPhase == 3)
        {
            float Rotator = 0.3f;
            float Amt = 0f;
            while(Math.Abs(Amt*Rotator) < Half_A_Pie)
                Amt++;
            float EnderDistance = Vector2.Distance(ProjCenter,LockTargetPoint);
            Vector2 CenterPoint = (LockTargetPoint+LockTargetStart)/2;
            if(EnderDistance < 10f || projectile.ai[0] > Amt)
            {
                projectile.ai[0] = 0f;
                projectile.ai[1] = 120f;
                LockTargetPoint+=CenterPoint-LockTargetPoint;
                LockTargetStart = new Vector2(ProjCenter.X,ProjCenter.Y);
                Phase = 0;
                //LockPhase = 2;
                return;
            }
            float Angle = (float)Math.Atan2(ProjCenter.Y-CenterPoint.Y,ProjCenter.X-CenterPoint.X);
            projectile.position  = RotateAboutOrigin(ProjCenter,CenterPoint,Rotator);
            projectile.position -= new Vector2(projectile.width/2,projectile.height/2);
            projectile.rotation = Angle+Half_A_Pie;
            projectile.rotation *= (Rotator/Math.Abs(Rotator));
        }
    }
    #endregion
}

#region Rotate

public float Rotate(Vector2 MyPos , Vector2 TargetPos , float MyRot)
{
	float Time = 1.0f / Main.frameRate;
	float Angle = (float)Math.Atan2(TargetPos.Y  - MyPos.Y,TargetPos.X  - MyPos.X);
	float DiffRot = MyRot - Quarter_A_Pie - Angle;
	
	if(DiffRot > Half_A_Pie)
	{
		DiffRot -= _A_Pie;
	}
	else if(DiffRot < -Half_A_Pie)
	{
		DiffRot += _A_Pie;
	}
	
	if(DiffRot > Time * _A_Pie)
	{
		if(DiffRot > 0.0f)
		{
			MyRot -= Time * _A_Pie;
		}
		else
		{
			MyRot += Time * _A_Pie;
		}
	}
	else
	{
		MyRot = Angle + Quarter_A_Pie;
	}
    return MyRot;
}

#endregion


#region Find NPC

public int FindClosestNPC(Vector2 CenterLocation,bool HasToBeFriendly ,bool HastoNotBeFriendly,int[] InvalidTars,bool SightVector,Vector2 Sight)
{
    float num = -1f;
    int target = -1;
    foreach(NPC N in Main.npc)
    {
        if(N.active &&
         N.damage > 0 &&
         !N.dontTakeDamage &&
         (!SightVector || (SightVector && Collision.CanHit(Sight, 8,8,N.position,N.width,N.height))) &&
        (!HastoNotBeFriendly || (HastoNotBeFriendly && !N.friendly)) &&
        (!HasToBeFriendly || (HasToBeFriendly && N.friendly))
        )
        {
            bool FoundAnInvalid = false;
            foreach(int z in InvalidTars)
                if(z == N.whoAmI)
                    FoundAnInvalid = true;
            if(num == -1f || Vector2.Distance(CenterLocation,N.position+new Vector2(N.width/2,N.height/2)) < num)
            {
                if(!FoundAnInvalid)
                {
                    num = Vector2.Distance(CenterLocation,N.position+new Vector2(N.width/2,N.height/2));
                    target = N.whoAmI;
                }
            }
        }
    }
    return target;
}

#endregion


#region Find Player

public int FindClosestPlayer(Vector2 CenterLocation,bool HasToBeHostile,bool HasToLikeMyOwner,bool HasToNotLikeMyOwner)
{
    float num = -1f;
    int target = -1;
    foreach(Player P in Main.player)
    {
        if(P.active && 
        !P.dead &&
        (!HasToBeHostile || (HasToBeHostile && P.hostile)) &&
        (!HasToNotLikeMyOwner || (HasToNotLikeMyOwner && P.team != Main.player[projectile.owner].team)) &&
        (!HasToLikeMyOwner || (HasToLikeMyOwner && P.team == Main.player[projectile.owner].team))
        )
        {
            if(num == -1f || Vector2.Distance(CenterLocation,P.position+new Vector2(P.width/2,P.height/2)) < num)
            {
                num = Vector2.Distance(CenterLocation,P.position+new Vector2(P.width/2,P.height/2));
                target = P.whoAmi;
            }
        }
    }
    return target;
}

#endregion


#region Set Blade Info

public void SetBladeInfo(int BladeIndexer)
{
    
    float Degree = -Half_A_Pie/180f;
    float Rotator = BladeIndexer*Degree*-10;
    Vector2 Offseteer = new Vector2(0,50f);
    Offseteer=RotateAboutOrigin(Offseteer,Vector2.Zero,Rotator);
    Offseteer+=new Vector2(-10,-30);
    offsetRot = Rotator;
    BladePosOffset = new Vector2(Offseteer.X,Offseteer.Y);
    BladeIndex = BladeIndexer-1;
}

#endregion


#region Rotate About Origin

 public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
{
    Vector2 u = point - origin;

    if (u == Vector2.Zero)
        return point;

    float a = (float)Math.Atan2(u.Y, u.X);
    a += rotation;

    u = u.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
    return u + origin;
} 

#endregion


#region Damage NPC


public void DamageNPC(NPC npc, ref int damage, ref float knockback) 
{
    damage = 0;
    while(Main.CalculateDamage(damage, npc.defense) < 10)
        damage++;
}

#endregion


