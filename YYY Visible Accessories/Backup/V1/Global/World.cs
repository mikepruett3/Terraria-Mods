public static int modIndex=0;
public void Initialize() 
{
	modIndex=Config.mods.IndexOf("YYY Visible Accessories");
}
public void NetReceive(int msg, BinaryReader reader) 
{
    if(!Main.dedServ)
    {
        int playerID = (int)reader.ReadByte();
        for(int i = 0; i < ModPlayer.Num_Of_Accs;i++)
        {
            int ItemID = reader.ReadInt32();
            byte Exists = reader.ReadByte();
    
            ModPlayer.PVA[playerID,i] = ItemID;
            ModPlayer.PAV[playerID,i] = Exists==(byte)1;
        }   
    }
	else 
    {
        int playerID = (int)reader.ReadByte();

        //int C = 0;
        //object[] Arr = new object[((reader.BaseStream.Length-1)/5)*2];
        //while(C < Arr.Length)
        //{
        //    Arr[C++] = reader.ReadInt32();
        //    Arr[C++] = reader.ReadBoolean();
        //}
        
        //NetMessage.SendModData(ModWorld.modIndex, 1, -1, -1, (byte)playerID,Arr);


        Item I0 = ModPlayer.Visible_Accs_Menu.IAR[0];
        Item I1 = ModPlayer.Visible_Accs_Menu.IAR[1];
        Item I2 = ModPlayer.Visible_Accs_Menu.IAR[2];
        Item I3 = ModPlayer.Visible_Accs_Menu.IAR[3];
        Item I4 = ModPlayer.Visible_Accs_Menu.IAR[4];
        Item I5 = ModPlayer.Visible_Accs_Menu.IAR[5];
        Item I6 = ModPlayer.Visible_Accs_Menu.IAR[6];
        Item I7 = ModPlayer.Visible_Accs_Menu.IAR[7];
        NetMessage.SendModData(ModWorld.modIndex, 1, -1, -1, (byte)playerID,
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean(),
        reader.ReadInt32(),
        reader.ReadBoolean()
        );
	}
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