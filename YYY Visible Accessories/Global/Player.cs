

public const int Num_Of_Accs = 8;

public class VA
{
    public Item[] PVA;
    public Item[] MakePVA()
    {
        Item[] A = new Item[Num_Of_Accs];
        for(int i = 0; i < A.Length; i++)
        {   
            A[i] = new Item();
        }
        return A;
    }
    public VA()
    {
        PVA = MakePVA();
    }
}

    public static VA[] PVA = MakeVA();
    public static VA[] MakeVA()
    {
        VA[] arr = new VA[Main.player.Length];
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = new VA();
        }
        return arr;
    }

public void Initialize()
{
    PVA = MakeVA();
    Visible_Accs_Menu.IAR = Visible_Accs_Menu.DespairGeneration();
    //PVA[Main.myPlayer].PVA = PVA[Main.myPlayer].MakePVA();
}

public void UpdatePlayer(Player P)  
{
    Sync_My_VA(P);
    VA_Effects(P);
    //if(P.whoAmi == Main.myPlayer && Main.netMode == 1)
    //{
    //    Set_My_VA(P);
    //    Sync_My_VA(P);
    //}
    foreach(Item I in P.armor) RunTotalFix(P,I);
    foreach(Item I in PVA[P.whoAmi].PVA) RunTotalFix(P,I);
}

#region TOtalFIx

public void RunTotalFix(Player P,Item I)
{
        if(I.type < 604) return;
        if (I.prefix == 62)
        {
        	P.statDefense++;
        }
        if (I.prefix == 63)
        {
        	P.statDefense += 2;
        }
        if (I.prefix == 64)
        {
        	P.statDefense += 3;
        }
        if (I.prefix == 65)
        {
        	P.statDefense += 4;
        }
        if (I.prefix == 66)
        {
        	P.statManaMax2 += 20;
        }
        if (I.prefix == 67)
        {
        	P.meleeCrit++;
        	P.rangedCrit++;
        	P.magicCrit++;
        }
        if (I.prefix == 68)
        {
        	P.meleeCrit += 2;
        	P.rangedCrit += 2;
        	P.magicCrit += 2;
        }
        if (I.prefix == 69)
        {
        	P.meleeDamage += 0.01f;
        	P.rangedDamage += 0.01f;
        	P.magicDamage += 0.01f;
        }
        if (I.prefix == 70)
        {
        	P.meleeDamage += 0.02f;
        	P.rangedDamage += 0.02f;
        	P.magicDamage += 0.02f;
        }
        if (I.prefix == 71)
        {
        	P.meleeDamage += 0.03f;
        	P.rangedDamage += 0.03f;
        	P.magicDamage += 0.03f;
        }
        if (I.prefix == 72)
        {
        	P.meleeDamage += 0.04f;
        	P.rangedDamage += 0.04f;
        	P.magicDamage += 0.04f;
        }
        if (I.prefix == 73)
        {
        	P.moveSpeed += 0.01f;
        }
        if (I.prefix == 74)
        {
        	P.moveSpeed += 0.02f;
        }
        if (I.prefix == 75)
        {
        	P.moveSpeed += 0.03f;
        }
        if (I.prefix == 76)
        {
        	P.moveSpeed += 0.04f;
        }
        if (I.prefix == 77)
        {
        	P.meleeSpeed += 0.01f;
        }
        if (I.prefix == 78)
        {
        	P.meleeSpeed += 0.02f;
        }
        if (I.prefix == 79)
        {
        	P.meleeSpeed += 0.03f;
        }
        if (I.prefix == 80)
        {
        	P.meleeSpeed += 0.04f;
        }
}
#endregion

public void Set_My_VA(Player P)
{
    int mP = P.whoAmi;
    PVA[mP].PVA = Visible_Accs_Menu.IAR;
}
public void VA_Effects(Player P)
{   
    int mP = P.whoAmi;
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        if(PVA[mP].PVA[i].type > 0)
        {
            if(PVA[mP].PVA[i].type < 605)
                RunBackStupidly(PVA[mP].PVA[i],P);
            else
                PVA[mP].PVA[i].RunMethod("Effects",P);
        }
    }
}


public bool PreDraw(Player player,SpriteBatch sp) 
{   
    int mP = player.whoAmi;
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        if(PVA[mP].PVA[i].type > 0)
        {
            PVA[mP].PVA[i].RunMethod("Visible_Accessory_PreDraw",player , sp);
        }
    }
    return true;
}

public void PostDraw(Player player,SpriteBatch sp) 
{   
    int mP = player.whoAmi;
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        if(PVA[mP].PVA[i].type>0)
        {
            PVA[mP].PVA[i].RunMethod("Visible_Accessory_PostDraw",player , sp);
        }
    }
}

public static void Sync_My_VA(Player P)
{
    if(P.whoAmi != Main.myPlayer) return;
   
    //ModWorld.Send(P);
    //if(Main.netMode == 0)
    //{
    PVA[P.whoAmi].PVA = Visible_Accs_Menu.IAR;
    //}
}

#region old
//public static void OldSync_My_VA(Player P)
//{
//    Item I0 = Visible_Accs_Menu.IAR[0];
//    Item I1 = Visible_Accs_Menu.IAR[1];
//    Item I2 = Visible_Accs_Menu.IAR[2];
//    Item I3 = Visible_Accs_Menu.IAR[3];
//    Item I4 = Visible_Accs_Menu.IAR[4];
//    Item I5 = Visible_Accs_Menu.IAR[5];
//    Item I6 = Visible_Accs_Menu.IAR[6];
//    Item I7 = Visible_Accs_Menu.IAR[7];
//    NetMessage.SendModData(ModWorld.modIndex, 1, -1, -1, (byte)P.whoAmi,
//    I0.type,
//    (I0.stack > 0),
//    I1.type,
//    (I1.stack > 0),
//    I2.type,
//    (I2.stack > 0),
//    I3.type,
//    (I3.stack > 0),
//    I4.type,
//    (I4.stack > 0),
//    I5.type,
//    (I5.stack > 0),
//    I6.type,
//    (I6.stack > 0),
//    I7.type,
//    (I7.stack > 0)
//    );
//    if(Main.netMode == 0)
//    {
//        PVA[0,0] = I0.type;
//        PAV[0,0] = I0.stack > 0;
//        PVA[0,1] = I1.type;
//        PAV[0,1] = I1.stack > 0;
//        PVA[0,2] = I2.type;
//        PAV[0,2] = I2.stack > 0;
//        PVA[0,3] = I3.type;
//        PAV[0,3] = I3.stack > 0;
//        PVA[0,4] = I4.type;
//        PAV[0,4] = I4.stack > 0;
//        PVA[0,5] = I5.type;
//        PAV[0,5] = I5.stack > 0;
//        PVA[0,6] = I6.type;
//        PAV[0,6] = I6.stack > 0;
//        PVA[0,7] = I7.type;
//        PAV[0,7] = I7.stack > 0;
//    }
//}
#endregion

#region Code stupidly used
public void RunBackStupidly(Item I,Player P)
{
    if (I.type == 15 && P.accWatch < 1)
    {
    	P.accWatch = 1;
    }
    if (I.type == 16 && P.accWatch < 2)
    {
    	P.accWatch = 2;
    }
    if (I.type == 17 && P.accWatch < 3)
    {
    	P.accWatch = 3;
    }
    if (I.type == 18 && P.accDepthMeter < 1)
    {
    	P.accDepthMeter = 1;
    }
    if (I.type == 53)
    {
    	P.doubleJump = true;
    }
    if (I.type == 54)
    {
        //hermes fail
    }
    if (I.type == 128)
    {
    	P.rocketBoots = 1;
    }
    if (I.type == 156)
    {
    	P.noKnockback = true;
    }
    if (I.type == 158)
    {
    	P.noFallDmg = true;
    }
    if (I.type == 159)
    {
    	P.jumpBoost = true;
    }
    if (I.type == 187)
    {
    	P.accFlipper = true;
    }
    if (I.type == 211)
    {
    	P.meleeSpeed += 0.12f;
    }
    if (I.type == 223)
    {
    	P.manaCost -= 0.06f;
    }
    if (I.type == 285)
    {
    	P.moveSpeed += 0.05f;
    }
    if (I.type == 212)
    {
    	P.moveSpeed += 0.1f;
    }
    if (I.type == 267)
    {
    	P.killGuide = true;
    }
    if (I.type == 193)
    {
    	P.fireWalk = true;
    }
    if (I.type == 485)
    {
    	P.wolfAcc = true;
    }
    if (I.type == 486)
    {
    	P.rulerAcc = true;
    }
    if (I.type == 393)
    {
    	P.accCompass = 1;
    }
    if (I.type == 394)
    {
    	P.accFlipper = true;
    	P.accDivingHelm = true;
    }
    if (I.type == 395)
    {
    	P.accWatch = 3;
    	P.accDepthMeter = 1;
    	P.accCompass = 1;
    }
    if (I.type == 396)
    {
    	P.noFallDmg = true;
    	P.fireWalk = true;
    }
    if (I.type == 397)
    {
    	P.noKnockback = true;
    	P.fireWalk = true;
    }
    if (I.type == 399)
    {
    	P.jumpBoost = true;
    	P.doubleJump = true;
    }
    if (I.type == 405)
    {
    	//hermes fail
    	P.rocketBoots = 2;
    }
    if (I.type == 407)
    {
    	P.blockRange = 1;
    }
    if (I.type == 489)
    {
    	P.magicDamage += 0.15f;
    }
    if (I.type == 490)
    {
    	P.meleeDamage += 0.15f;
    }
    if (I.type == 491)
    {
    	P.rangedDamage += 0.15f;
    }
    if (I.type == 492)
    {
    	P.wings = 1;
    }
    if (I.type == 493)
    {
    	P.wings = 2;
    }
    if (I.type == 497)
    {
    	P.accMerman = true;
    }
    if (I.type == 535)
    {
    	P.pStone = true;
    }
    if (I.type == 536)
    {
    	P.kbGlove = true;
    }
    if (I.type == 532)
    {
    	P.starCloak = true;
    }
    if (I.type == 554)
    {
    	P.longInvince = true;
    }
    if (I.type == 555)
    {
    	P.manaFlower = true;
    	P.manaCost -= 0.08f;
    }
}
#endregion

public void Save(BinaryWriter W) 
{
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        Item In = Visible_Accs_Menu.IAR[i];
        //if (In.name == null)
        //{
        //    In.name = "";
        //}
        W.Write((int)In.netID);

        Item In2 = new Item();
        In2.SetDefaults(In.type);

        W.Write(In2.name);
        W.Write((byte)In.stack);
        W.Write((byte)In.prefix);
        Codable.SaveCustomData(In, W);
    }
}

public void Load(BinaryReader R, int V) 
{
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        Item In = Visible_Accs_Menu.IAR[i];
        int netID = R.ReadInt32();
        
        In.SetDefaults(R.ReadString());
        //if (netID >= Main.maxItemTypes)
        //    In.netDefaults(Config.itemDefs.byName[Config.itemDefs.playerLoad[netID]].netID);

        //In.netDefaults(netID);
        In.stack = R.ReadByte();
        In.Prefix((int)R.ReadByte());
        Codable.LoadCustomData(In, R, 5, true);
    }
}

public void FrameEffect(Player player)
{
    int mP = player.whoAmi;
    for(int i = 0; i < Num_Of_Accs; i++)
    {
        if(PVA[mP].PVA[i].type>0)
        {
            PVA[mP].PVA[i].RunMethod("Visible_Accessory_FrameEffect",player);
        }
    }
}
#region Interface

public class Visible_Accs_Menu : Interfaceable 
{

    public static Item[] IAR = DespairGeneration();

    public static Item[] DespairGeneration()
    {
        Item[] AR = new Item[ModPlayer.Num_Of_Accs];
        for(int i = 0; i < Num_Of_Accs; i++)
        {
            AR[i] = new Item();
        }
        return AR;
    }

    public static Dictionary<string,int> Attach = new Dictionary<string,int>();

    public static void Create() 
    {
        Config.tileInterface = new InterfaceObj(new Visible_Accs_Menu(), ModPlayer.Num_Of_Accs, ModPlayer.Num_Of_Accs);
        Config.tileInterface.name = "Yoraiz0r Visible Accessories";

        int SX = 100;
        int SY = 300;

        int OA = 55;
        int TO = -20;

        string TXT = "";
        int IO=-1;

        Attach = new Dictionary<string,int>();

        ++IO;
        TXT = "Head";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Neck";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Wrists";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Arms";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Feet";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Legs";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Waist";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        ++IO;
        TXT = "Back";
        Attach.Add(TXT,IO);

        Config.tileInterface.AddText(TXT, SX+IO*OA, SY+TO, false, 0.9f,Color.White);
        Config.tileInterface.AddItemSlot(SX+IO*OA,SY);

        Attach.Add("All",-1);
    }

    public void ButtonClicked(int button) 
    {
        Item[] itemSlots = Config.tileInterface.itemSlots;
    }

    public bool CanPlaceSlot(int slot, Item mouseItem) 
    {
        if(mouseItem==null || mouseItem.type < 1 || mouseItem.stack < 1)
            return true;
        if(mouseItem.RunMethod("Visible_Acc"))
        {
            string[] A = (string[])Codable.customMethodReturn;
            foreach(string B in A)
            if (Attach.ContainsKey(B))
            {
                int value = Attach[B];
                if(slot == value || value == -1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void PlaceSlot(int slot) 
    {    
        PVA[Main.myPlayer].PVA = Visible_Accs_Menu.IAR;
        ModWorld.Send(Main.player[Main.myPlayer]);
    }

    public bool DropSlot(int slot) 
    {
        return false;
    }

    public static void OurCustomUpdateFeature()
    {
        Color[] buttonColors = Config.tileInterface.buttonColor;
        bool[] ButtonIsClickable = Config.tileInterface.buttonClickable;
        string[] ButtonTexts = Config.tileInterface.buttonText;

        Item[] itemSlots = Config.tileInterface.itemSlots;
    }
}

#endregion