public void UseTile(Player player, int x, int y)
{
    while(Main.tile[x,y].frameX>0) x--;
    while(Main.tile[x,y].frameY>0) y--;

    Nest_Interface.Create();
    Config.tileInterface.SetLocation(new Vector2((float)x,(float)y));

    Main.playerInventory = true;
}


public class Nest_Interface : Interfaceable
	{
	const int Number_Of_Buttons = 1;
    const int Number_Of_Slots = 1;
	
	public static void Create()
		{
		Config.tileInterface = new InterfaceObj(new Nest_Interface(), Number_Of_Slots, Number_Of_Buttons);
		Config.tileInterface.AddText("Nest", 400, 270, true, 0.9f,Color.Yellow);
		Config.tileInterface.AddItemSlot(400,290);
		NestUpdate();
		}
	public static void NestUpdate()
		{
		
		}
		
	public void ButtonClicked(int button)
		{
        NestUpdate();
        Item[] itemSlots = Config.tileInterface.itemSlots;
        if(itemSlots[0].stack > 19 && itemSlots[0].type == Config.itemDefs.byName["Ho-Oh Egg"].type)
			{
			itemSlots[0].stack -= 20;
			//NPC.NewNPC(tile.frameX,tile.frameY,"Big Bird",0);	
			Main.NewText("Big Bird comming for eggs! ;O ");
			SpawnBigBird(Main.player[Main.myPlayer]);
			}
		}
		
	public bool CanPlaceSlot(int slot, Item mouseItem)
		{
        NestUpdate();
        return true;
		}

    public void PlaceSlot(int slot)
		{
        NestUpdate();
		}

    public bool DropSlot(int slot)
		{
        Item[] itemSlots = Config.tileInterface.itemSlots;
        if(itemSlots[slot].stack > 1) return true;
        return false;
		}
	public void SpawnBigBird(Player player)
		{
		//NPC.SpawnOnPlayer(player,"Big Bird");
		NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Big Bird",0);
		}
	}