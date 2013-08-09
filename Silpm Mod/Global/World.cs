public static int SmallTroll = 0;
//public static int CrazyWingsFlap = 0;
public static bool OresAdded = false;
public static bool CrazyTrolololoFight = false;
public static int tf=0;
public static float qcurVolume;
public static bool Added = false;
public static int WingIndex = 0;
public static bool CrazerKilled = false;
public static int Knights= 0;
public static bool SlimeMother=false;
public static bool teeeeest=true;

public void Save(BinaryWriter writer)
	{
	writer.Write(CrazerKilled);
	writer.Write(OresAdded);
	
	}
public void Load(BinaryReader reader,int version)
	{
	CrazerKilled=reader.ReadBoolean();
	OresAdded=reader.ReadBoolean();
	}
public void Initialize()
{
	//reset
	SmallTroll=0;
	CrazyTrolololoFight=false;
	tf=0;
	qcurVolume=Main.musicVolume;
	Knights=0;
	SlimeMother=false;
	//Wings
	if(Added)
		{
        return;
		}
    Texture2D mytex=Main.goreTexture[Config.goreID["Crazy Wings"]];
    foreach(Texture2D T in Main.wingsTexture)
		{
        if(mytex == T)
			{
            Added = true;
            break;
			}
		}
    if(!Added)
		{
        Added = true;
        Texture2D[] wingsTextureNew = new Texture2D[Main.wingsTexture.Length+1];
        for (int i = 0; i < Main.wingsTexture.Length; i++)
			{
            wingsTextureNew[i]=Main.wingsTexture[i];
			}
        wingsTextureNew[Main.wingsTexture.Length] = mytex;
        Main.wingsTexture = wingsTextureNew;
		WingIndex = Main.wingsTexture.Length-1;
		}
	//
}

public static void UpdateWorld()
{
	//Trolololo Boss Music
	if (CrazyTrolololoFight == true)
		{ tf++;
		if (tf==9660)
			{
			Main.PlaySound(2,-1,-1,SoundHandler.soundID["TrolololoFull"]);
			tf=0;
			}
		
		}
	//Ores
	if ((OresAdded == false) && (CrazerKilled))
		{
		OresAdded=true;
		int amount=0;
		double num6;
		num6 = Main.rockLayer;
		int i2 = -100+Main.maxTilesX-100;
		int j2 = -(int)num6+Main.maxTilesY-150;
		int sum = i2*j2;
		amount = (int)(sum/10000)*3;
	    for(int zz=0;zz<amount;zz++) GenCrazyOre();
		}
	//
}



public static void GenCrazyOre() {
	int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
	double num6;
	num6 = Main.rockLayer;
	int j2 = WorldGen.genRand.Next((int)num6, Main.maxTilesY - 150);
	WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 5), Config.tileDefs.ID["Crazy ore"]);	
}