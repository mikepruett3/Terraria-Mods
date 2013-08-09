public static int modIndex=0;
public void Initialize() 
{
	modIndex=Config.mods.IndexOf("YYY Visible Accessories");
}
public void NetReceive(int msg, BinaryReader R) 
{
    int PID = (int)R.ReadByte();
    //Console.WriteLine("reached "+PID+" ");
    Player P = Main.player[PID];
    if(msg == 1)
    {
        if(!Main.dedServ)
        { 
            Recover(P,R);
        }
	    else 
        {
            Recover(P,R);
            Send(P);
	    }
    }
    if(msg == 2)
    {
        if(!Main.dedServ)
        { 
            Recover(P,R);
            Send(P);
        }
    }
}

public void PlayerConnected(int PID)
{
    Player P = Main.player[PID];
    Send(P,2);
}

public static void Recover(Player P, BinaryReader R)
{
        int PID = P.whoAmi;
        for(int i = 0; i < ModPlayer.Num_Of_Accs;i++)
        {
            int netID = R.ReadInt32();
            byte StackAmt = R.ReadByte();
            byte Pref = R.ReadByte();
            //Console.WriteLine(""+netID+" "+StackAmt+" "+Pref);
            //if (netID >= Main.maxItemTypes)
            //    ModPlayer.PVA[PID].PVA[i].netDefaults(Config.itemDefs.byName[Config.itemDefs.playerLoad[netID]].netID);
            ModPlayer.PVA[PID].PVA[i].netDefaults(netID);
            ModPlayer.PVA[PID].PVA[i].stack = (int)StackAmt;
            ModPlayer.PVA[PID].PVA[i].Prefix((int)Pref);
            Codable.LoadCustomData(ModPlayer.PVA[PID].PVA[i], R, 5, true);
        }  
}


public static void Send(Player P,int msg = 1) 
{
    int mP = P.whoAmi;

    #region hell
    //byte[][] Arr = new byte[ModPlayer.Num_Of_Accs][];
    //int count = 0;
    //string s = "";
    //for(int i = 0; i < Arr.Length ; i++)
    //{
    //    Arr[i] = ItemNetData(ModPlayer.PVA[mP].PVA[i]);
    //    count+= Arr[i].Length;
    //}
    //foreach(Byte B in Arr[7]) s+=B;
    //Main.NewText(""+s);
    //byte[] Combined = new byte[count+1];
    //Combined[0] = (byte)P.whoAmi;
    //count = 1;
    //for(int i = 0; i < Arr.Length; i++)
    //{   
    //    int Amt = Arr[i].Length;
    //    if (Amt > 0) Buffer.BlockCopy(Arr[i], 0, Combined, 3, Amt);
    //    count+=Amt;
    //}

    //object[] oArr = new object[Combined.Length];
    //s = "";
    //s = ""+Combined.Length+" ";
    //for ( int i = 0; i < oArr.Length; i++) 
    //{
    //    s+=Combined[i];
    //    oArr[i] = Combined[i];
    //}
    //Console.WriteLine(s);
#endregion

    //if(!Main.dedServ) return;
    MemoryStream stream = new MemoryStream();
    BinaryWriter W = new BinaryWriter(stream);
    for(int i = 0; i < ModPlayer.PVA[mP].PVA.Length ; i++)
    {
        Item I = ModPlayer.PVA[mP].PVA[i];
        W.Write((int)I.netID);
        W.Write((byte)I.stack);
        W.Write((byte)I.prefix);
        Codable.SaveCustomData(I, W);
    }
    W.Close();
    byte[] B = new byte[stream.ToArray().Length];
    Buffer.BlockCopy(stream.ToArray(), 0, B, 0, stream.ToArray().Length);

    object[] oArr = new object[B.Length+1];
    string s = "";
    s = ""+B.Length+" ";
    oArr[0] = (byte)mP;
    for ( int i = 1; i < oArr.Length; i++) 
    {
        s+=B[i-1];
        oArr[i] = B[i-1];
    }
    
    NetMessage.SendModData(ModWorld.modIndex, msg, -1, -1, oArr);
    
}

public static byte[] ItemNetData(Item In)
{

    MemoryStream stream = new MemoryStream();
    BinaryWriter W = new BinaryWriter(stream);
    if (In.name == null)
    {
        In.name = "";
    }
    W.Write((int)In.netID);
    W.Write((byte)In.stack);
    W.Write((byte)In.prefix);
    Codable.SaveCustomData(In, W);
    W.Close();
    byte[] B = new byte[stream.ToArray().Length];
    Buffer.BlockCopy(stream.ToArray(), 0, B, 0, stream.ToArray().Length);
    return B;

    //byte[] NetID;
    //if (I.name == null || I.name == "")
    //{
    //    NetID = BitConverter.GetBytes(0);
    //}
    //else
    //{
    //    NetID = BitConverter.GetBytes(I.netID);
    //}
    
    //MemoryStream stream = new MemoryStream();
    //BinaryWriter writer = new BinaryWriter(stream);
    //Codable.SaveCustomData(I, writer, true);
    //writer.Close();
    //byte[] total = new byte[stream.ToArray().Length+NetID.Length+2];
    //int count = 0;
    //if (NetID.Length > 0) Buffer.BlockCopy(NetID, 0, total, 0, NetID.Length);
    //count+=NetID.Length;
    //total[count] = (byte)I.stack;
    //count++;
    //total[count] = (byte)I.prefix;
    //count++;
    //if (stream.ToArray().Length > 0) Buffer.BlockCopy(stream.ToArray(), 0, total, count, stream.ToArray().Length);
    //return total;
}



public static bool SHOW_ACCMENU = false;
public static Texture2D ACC_PIC=Main.goreTexture[Config.goreID["Visible Accessories Menu Button"]];

public static void PreDrawInterface(SpriteBatch sp) 
{
    if (Main.playerInventory)
    {
        Color white = new Color(150, 150, 150, 150);
        Main.inventoryScale = 0.85f;
        int XO = 448;
        int YO = 210;
        #region draw button textures
                int toggler = 1;
                Color colz = Color.White;
                int SX = 82;
                int SY = Main.screenHeight-112;
                if (SHOW_ACCMENU)
                {
                    toggler = 0;
                    if(Config.tileInterface!=null && Config.tileInterface.name == "Yoraiz0r Visible Accessories")
                    {
	                    Config.tileInterface.SetLocation(new Vector2((float)(Main.player[Main.myPlayer].position.X/16f),(float)(Main.player[Main.myPlayer].position.Y/16f)));
                    }
                    else
                    {
                    SHOW_ACCMENU = false;
                    toggler = 1 ;
                    }
                }
                if(toggler == 1)
                {
                    colz = Color.Gray;
                }
                sp.Draw(ACC_PIC, new Vector2((float)SX, (float)SY), new Rectangle?(new Rectangle(0, 0, ACC_PIC.Width, ACC_PIC.Height)), colz, 0f, default(Vector2), 0.9f, SpriteEffects.None, 0f);
                #endregion
        #region button logic
                if (Main.mouseX > SX && (float)Main.mouseX < (float)SX + (float)ACC_PIC.Width * 0.9f 
                    && Main.mouseY > SY && (float)Main.mouseY < (float)SY + (float)ACC_PIC.Height * 0.9f
                )
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Main.PlaySound(12, -1, -1, 1);
                        if (!SHOW_ACCMENU)
                        {
                            SHOW_ACCMENU = true;
                            ModPlayer.Visible_Accs_Menu.Create();
                            Config.tileInterface.itemSlots = ModPlayer.Visible_Accs_Menu.IAR;
                            Config.tileInterface.SetLocation(new Vector2((float)(Main.player[Main.myPlayer].position.X/16f),(float)(Main.player[Main.myPlayer].position.Y/16f)));
              
                        }
                        else
                        {
                            SHOW_ACCMENU = false;
                            Config.tileInterface=null;
                        }
                    }
                }
#endregion
    }
}
    //MemoryStream stream = new MemoryStream();
    //BinaryWriter W = new BinaryWriter(stream);
    //if (In.name == null)
    //{
    //    In.name = "";
    //}
    //W.Write(In.netID);
    //W.Write(In.stack);
    //W.Write(In.prefix);
    //Codable.SaveCustomData(In, W);
    //W.Close();
    //byte[] B = new byte[stream.ToArray().Length];
    //Buffer.BlockCopy(stream.ToArray(), 0, B, 0, stream.ToArray().Length);
    //return B;