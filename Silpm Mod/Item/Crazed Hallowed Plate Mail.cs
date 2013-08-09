public static void Effects(Player player) 
	{
	player.magicCrit += 10;
	player.meleeCrit += 10;
	player.rangedCrit += 10;
	if(player.head == 89) Main.NewText("HAHAHAHA",0,0,0);
	}
	
public static void SetBonus(Player player)
	{
	// Thx Yoraiz0r for help with this \/
	if(player.head == Config.itemDefs.byName["Crazed Hallowed Mask"].headSlot)
		{
		player.setBonus = "+25% Melee and Movement Speed";
		player.meleeSpeed +=0.25f;
		player.moveSpeed *=25;
		}
	if(player.head == Config.itemDefs.byName["Crazed Hallowed Helmet"].headSlot)
		{
		player.setBonus = "25% Chance not to Consume Ammo";
		player.ammoCost75=true;	
		}
	if(player.head == Config.itemDefs.byName["Crazed Hallowed Headgear"].headSlot)
		{
		player.setBonus = "-20% Mana Usage";
		player.manaCost -= 0.20f;
		}
	}
	//Config.itemID["Crazed Hallowed Mask"]
	//ItemDefHandler["Crazed Hallowed Mask"]
	// /\ Close enought :P